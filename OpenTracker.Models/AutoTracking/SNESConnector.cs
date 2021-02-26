using Newtonsoft.Json;
using OpenTracker.Models.AutoTracking.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the class for the SNES connector that uses (Q)USB2SNES websocket.
    /// </summary>
    public class SNESConnector : ISNESConnector
    {
        private readonly IAutoTrackerLogService _logService;
        private readonly IRequestType.Factory _requestFactory;

        private readonly static string _appName = "OpenTracker";
        private readonly object _transmitLock = new object();

        private Action<MessageEventArgs>? _messageHandler;
        private string? _uri;
        private string? _device;

        private bool Connected =>
            Socket != null && Socket.IsAlive;

        public event PropertyChangedEventHandler? PropertyChanged;

        private WebSocket? _socket;
        private WebSocket? Socket
        {
            get => _socket;
            set
            {
                if (_socket != value)
                {
                    if (_socket != null)
                    {
                        _socket.OnMessage -= HandleMessage;
                    }

                    _socket = value;

                    if (_socket != null)
                    {
                        _socket.OnMessage += HandleMessage;
                    }
                }
            }
        }

        private ConnectionStatus _status;
        public ConnectionStatus Status
        {
            get => _status;
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logService">
        /// The log service for the SNES connector.
        /// </param>
        /// <param name="requestFactory">
        /// An Autofac factory for creating requests.
        /// </param>
        public SNESConnector(
            IAutoTrackerLogService logService, IRequestType.Factory requestFactory)
        {
            _logService = logService;
            _requestFactory = requestFactory;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Subscribes to the OnMessage event on the WebSocket class and 
        /// invokes the current message handler.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the OnMessage event.
        /// </param>
        private void HandleMessage(object? sender, MessageEventArgs e)
        {
            _messageHandler?.Invoke(e);
        }

        /// <summary>
        /// Sends a message to the web socket.
        /// </summary>
        /// <param name="request">
        /// The payload of the request.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        private bool Send(IRequestType request)
        {
            var sendTask = Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    Socket?.Send(JsonConvert.SerializeObject(request));
                }
                catch (InvalidOperationException)
                {
                    return false;
                }

                return true;
            });

            sendTask.Wait();

            return sendTask.Result;
        }

        /// <summary>
        /// Sends a request and receives a Json encoded response.
        /// </summary>
        /// <param name="requestName">
        /// A string representing the type of request for logging purposes.
        /// </param>
        /// <param name="request">
        /// The payload of the request.
        /// </param>
        /// <param name="ignoreErrors">
        /// A boolean representing whether to log and change the status on error.
        /// </param>
        /// <param name="timeOutInMS">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// An enumerator of the resulting strings of the response.
        /// </returns>
        private IEnumerable<string>? GetJsonResults(
            string requestName, IRequestType request, bool ignoreErrors = false,
            int timeOutInMS = 4096)
        {
            string[]? results = null;

            lock (_transmitLock)
            {
                using ManualResetEvent readEvent = new ManualResetEvent(false);

                _messageHandler = (e) =>
                {
                    _logService.Log(LogLevel.Info, $"Request {requestName} response received.");

                    if (JsonConvert.DeserializeObject<Dictionary<string, string[]>>(
                        e.Data) is Dictionary<string, string[]> dictionary &&
                        dictionary.TryGetValue("Results", out string[]? deserialized))
                    {
                        _logService.Log(
                            LogLevel.Debug, $"Request {requestName} successfully deserialized.");
                        results = deserialized;
                        readEvent.Set();
                    }
                };

                _logService.Log(LogLevel.Info, $"Request {requestName} sending.");

                if (!Send(request))
                {
                    _logService.Log(LogLevel.Info, $"Request {requestName} failed to send.");
                    return null;
                }

                _logService.Log(LogLevel.Info, $"Request {requestName} sent.");

                if (!readEvent.WaitOne(timeOutInMS))
                {
                    if (!ignoreErrors)
                    {
                        _logService.Log(LogLevel.Error, $"Request {requestName} failed.");
                        Status = ConnectionStatus.Error;
                    }

                    _messageHandler = null;
                    return null;
                }
            }

            _messageHandler = null;
            _logService.Log(LogLevel.Info, $"Request {requestName} successful.");
            return results;
        }

        /// <summary>
        /// Sends a request without a response.
        /// </summary>
        /// <param name="requestName">
        /// A string representing the type of request for logging purposes.
        /// </param>
        /// <param name="request">
        /// The payload of the request.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successfully.
        /// </returns>
        private bool SendOnly(string requestName, IRequestType request)
        {
            _logService.Log(LogLevel.Info, $"Request {requestName} is being sent.");

            lock (_transmitLock)
            {
                if (!Send(request))
                {
                    _logService.Log(LogLevel.Info, $"Request {requestName} failed to send.");
                    return false;
                }
            }

            _logService.Log(LogLevel.Info, $"Request {requestName} has been sent successfully.");
            return true;
        }

        /// <summary>
        /// Connects to the USB2SNES web socket, if not already connected.
        /// </summary>
        /// <param name="timeOutInMS">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        private bool ConnectIfNeeded(int timeOutInMS = 4096)
        {
            if (Connected)
            {
                _logService.Log(LogLevel.Debug, "Already connected to USB2SNES websocket, " +
                    "skipping connection attempt.");
                return true;
            }

            if (Socket != null)
            {
                _logService.Log(LogLevel.Debug, "Attempting to restart WebSocket class.");
                Disconnect();
                _logService.Log(LogLevel.Debug, "Existing WebSocket class restarted.");
            }

            return Connect(timeOutInMS);
        }

        /// <summary>
        /// Returns the device info of the attached device.
        /// </summary>
        /// <param name="timeOutInMS">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// An enumerator of the device info strings.
        /// </returns>
        private IEnumerable<string>? GetDeviceInfo(int timeOutInMS = 4096)
        {
            return GetJsonResults(
                "get device info", _requestFactory(OpcodeType.Info.ToString()), true, timeOutInMS);
        }

        /// <summary>
        /// Attaches to the selected device.
        /// </summary>
        /// <param name="timeOutInMS">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        private bool AttachDevice(int timeOutInMS = 4096)
        {
            _ = _device ?? throw new NullReferenceException();

            _logService.Log(LogLevel.Info, $"Attempting to attach to device {_device}.");
            Status = ConnectionStatus.Attaching;

            if (!SendOnly("attach", _requestFactory(
                OpcodeType.Attach.ToString(), operands: new List<string>(1) { _device })))
            {
                _logService.Log(LogLevel.Error, $"Device {_device} could not be attached.");
                Status = ConnectionStatus.Error;
                return false;
            }

            if (!SendOnly("register name", _requestFactory(
                OpcodeType.Name.ToString(), operands: new List<string>(1) { _appName })))
            {
                _logService.Log(LogLevel.Error, "Could not register app name with connection.");
                Status = ConnectionStatus.Error;
                return false;
            }

            if (GetDeviceInfo(timeOutInMS) == null)
            {
                _logService.Log(LogLevel.Error, $"Device {_device} could not be attached.");
                Status = ConnectionStatus.Error;
                return false;
            }

            _logService.Log(LogLevel.Info, $"Device {_device} is successfully attached.");
            Status = ConnectionStatus.Connected;

            return true;
        }

        /// <summary>
        /// Sets the URI.
        /// </summary>
        /// <param name="uriString">
        /// A string representing the URI.
        /// </param>
        public void SetUri(string uriString)
        {
            _uri = uriString;
        }

        /// <summary>
        /// Connects to the USB2SNES web socket.
        /// </summary>
        /// <param name="timeOutInMS">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        public bool Connect(int timeOutInMS = 4096)
        {
            Socket = new WebSocket(_uri);
            _logService.Log(LogLevel.Info, "Attempting to connect to USB2SNES websocket at " +
                $"{Socket.Url.OriginalString}.");
            Status = ConnectionStatus.Connecting;

            using var openEvent = new ManualResetEvent(false);

            void onOpen(object? sender, EventArgs e)
            {
                openEvent.Set();
            }

            Socket.OnOpen += onOpen;
            Socket.Connect();
            bool result = openEvent.WaitOne(timeOutInMS);
            Socket.OnOpen -= onOpen;

            if (result)
            {
                _logService.Log(LogLevel.Info, "Successfully connected to USB2SNES websocket at " +
                    $"{Socket.Url.OriginalString}.");
                Status = ConnectionStatus.SelectDevice;
            }
            else
            {
                _logService.Log(LogLevel.Error, "Failed to connect to USB2SNES websocket at " +
                    $"{Socket.Url.OriginalString}.");
                Status = ConnectionStatus.Error;
            }

            return result;
        }

        /// <summary>
        /// Disconnects from the web socket and unsets the web socket property.
        /// </summary>
        public void Disconnect()
        {
            Socket?.Close();
            Socket = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Status = ConnectionStatus.NotConnected;
            _device = null;
        }

        /// <summary>
        /// Returns the list of devices to which can be attached.
        /// </summary>
        /// <param name="timeOutInMS">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// An enumerator of the device list strings.
        /// </returns>
        public async Task<IEnumerable<string>?> GetDevices(int timeOutInMS = 4096)
        {
            return await Task<IEnumerable<string>?>.Factory.StartNew(() =>
            {
                if (!ConnectIfNeeded(timeOutInMS))
                {
                    return null;
                }

                return GetJsonResults(
                    "get device list", _requestFactory(OpcodeType.DeviceList.ToString()), false,
                    timeOutInMS);
            });
        }

        /// <summary>
        /// Sets the device to be connected to.
        /// </summary>
        /// <param name="device">
        /// A string representing the device.
        /// </param>
        public void SetDevice(string device)
        {
            _device = device;

            AttachDeviceIfNeeded();
        }

        /// <summary>
        /// Attaches to the selected device, if not already attached.
        /// </summary>
        /// <param name="timeOutInMS">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        public bool AttachDeviceIfNeeded(int timeOutInMS = 4096)
        {
            if (Status == ConnectionStatus.Connected)
            {
                _logService.Log(
                    LogLevel.Debug, "Already attached to device, skipping attachment attempt.");
                return true;
            }

            if (!ConnectIfNeeded(timeOutInMS))
            {
                return false;
            }

            return AttachDevice(timeOutInMS);
        }

        /// <summary>
        /// Returns the value of a byte of SNES memory.
        /// </summary>
        /// <param name="address">
        /// A 64-bit unsigned integer representing the memory address to be read.
        /// </param>
        /// <param name="value">
        /// An 8-bit unsigned integer representing the value of the byte of SNES memory.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        public bool Read(ulong address, out byte value)
        {
            var buffer = new byte[1];

            if (!Read(address, buffer))
            {
                value = 0;
                return false;
            }

            value = buffer[0];
            return true;
        }

        /// <summary>
        /// Returns the values of a contiguous set of bytes of SNES memory.
        /// </summary>
        /// <param name="address">
        /// A 64-bit unsigned integer representing the starting memory address to be read.
        /// </param>
        /// <param name="buffer">
        /// An array of bytes to be filled with SNES memory data.
        /// </param>
        /// <param name="timeOutInMS">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        public bool Read(ulong address, byte[] buffer, int timeOutInMS = 4096)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (!AttachDeviceIfNeeded(timeOutInMS))
            {
                return false;
            }

            if (Status != ConnectionStatus.Connected)
            {
                return false;
            }

            using ManualResetEvent readEvent = new ManualResetEvent(false);

            lock (_transmitLock)
            {
                _messageHandler = (e) =>
                {
                    if (!e.IsBinary || e.RawData == null)
                    {
                        return;
                    }

                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = e.RawData[Math.Min(i, e.RawData.Length - 1)];
                    }

                    readEvent.Set();
                };

                _logService.Log(LogLevel.Info, $"Reading {buffer.Length} byte(s) from {address:X}.");
                Send(_requestFactory(
                    OpcodeType.GetAddress.ToString(),
                    operands: new List<string>(2)
                    {
                        AddressTranslator.TranslateAddress((uint)address, TranslationMode.Read)
                            .ToString("X", CultureInfo.InvariantCulture),
                        buffer.Length.ToString("X", CultureInfo.InvariantCulture)
                    }));

                if (!readEvent.WaitOne(timeOutInMS))
                {
                    _logService.Log(
                        LogLevel.Error, $"Failed to read {buffer.Length} byte(s) from {address:X}.");
                    Status = ConnectionStatus.Error;
                    return false;
                }
            }

            _logService.Log(LogLevel.Info, $"Read {buffer.Length} byte(s) from {address:X} successfully.");
            return true;
        }
    }
}
