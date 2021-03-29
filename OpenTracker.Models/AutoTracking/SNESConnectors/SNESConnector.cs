using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenTracker.Models.AutoTracking.Logging;
using WebSocketSharp;
using LogLevel = OpenTracker.Models.AutoTracking.Logging.LogLevel;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This class contains logic for the SNES connector using WebSocketSharp to connect to (Q)USB2SNES.
    /// </summary>
    public class SNESConnector : ISNESConnector
    {
        private readonly IAutoTrackerLogService _logService;
        private readonly IRequestType.Factory _requestFactory;

        private const string AppName = "OpenTracker";
        private readonly object _transmitLock = new();

        private Action<MessageEventArgs>? _messageHandler;
        private string? _uri;
        private string? _device;

        private bool Connected => Socket is not null && Socket.IsAlive;

        public event EventHandler<ConnectionStatus>? StatusChanged;

        private ConnectionStatus _status;
        private ConnectionStatus Status
        {
            get => _status;
            set
            {
                if (_status == value)
                {
                    return;
                }
                
                _status = value;
                OnStatusChanged();
            }
        }

        private WebSocket? _socket;
        private WebSocket? Socket
        {
            get => _socket;
            set
            {
                if (_socket == value)
                {
                    return;
                }
                
                if (_socket is not null)
                {
                    _socket.OnMessage -= HandleMessage;
                }

                _socket = value;

                if (_socket is not null)
                {
                    _socket.OnMessage += HandleMessage;
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
        public SNESConnector(IAutoTrackerLogService logService, IRequestType.Factory requestFactory)
        {
            _logService = logService;
            _requestFactory = requestFactory;
        }

        public void SetUri(string uriString)
        {
            _uri = uriString;
        }

        public async Task<bool> Connect(int timeOutInMs = 4096)
        {
            return await Task<bool>.Factory.StartNew(() =>
            {
                Socket = new WebSocket(_uri);
                _logService.Log(LogLevel.Info, "Attempting to connect to USB2SNES websocket at " +
                                               $"{Socket.Url.OriginalString}.");
                Status = ConnectionStatus.Connecting;

                using var openEvent = new ManualResetEvent(false);

                void OnOpen(object? sender, EventArgs e)
                {
                    // ReSharper disable once AccessToDisposedClosure
                    openEvent.Set();
                }

                Socket.OnOpen += OnOpen;
                Socket.Connect();
                var result = openEvent.WaitOne(timeOutInMs);
                Socket.OnOpen -= OnOpen;

                if (result)
                {
                    _logService.Log(LogLevel.Info, "Successfully connected to USB2SNES websocket at " +
                        $"{Socket.Url.OriginalString}.");
                    Status = ConnectionStatus.SelectDevice;
                    return result;
                }

                _logService.Log(LogLevel.Error, "Failed to connect to USB2SNES websocket at " +
                    $"{Socket.Url.OriginalString}.");
                Status = ConnectionStatus.Error;
                
                return result;
            });
        }

        public async Task Disconnect()
        {
            await Task.Factory.StartNew(() =>
            {
                Socket?.Close();
                Socket = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Status = ConnectionStatus.NotConnected;
            });
        }

        /// <summary>
        /// Raises the StatusChanged event.
        /// </summary>
        private void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, Status);
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
        /// Returns the list of devices to which can be attached.
        /// </summary>
        /// <param name="timeOutInMs">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// An enumerator of the device list strings.
        /// </returns>
        public async Task<IEnumerable<string>?> GetDevices(int timeOutInMs = 4096)
        {
            return await Task<IEnumerable<string>?>.Factory.StartNew(() =>
            {
                if (!ConnectIfNeeded(timeOutInMs))
                {
                    return null;
                }

                return GetJsonResults(
                    "get device list", _requestFactory(OpcodeType.DeviceList.ToString()), false,
                    timeOutInMs);
            });
        }

        /// <summary>
        /// Sets the device to be connected to.
        /// </summary>
        /// <param name="device">
        ///     A string representing the device.
        /// </param>
        public async Task SetDevice(string device)
        {
            _device = device;

            await AttachDeviceIfNeeded();
        }

        /// <summary>
        /// Attaches to the selected device, if not already attached.
        /// </summary>
        /// <param name="timeOutInMs">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        public async Task<bool> AttachDeviceIfNeeded(int timeOutInMs = 4096)
        {
            return await Task<bool>.Factory.StartNew(() =>
            {
                if (Status != ConnectionStatus.Connected)
                {
                    return ConnectIfNeeded(timeOutInMs) && AttachDevice(timeOutInMs);
                }
                
                _logService.Log(
                    LogLevel.Debug, "Already attached to device, skipping attachment attempt.");
                
                return true;

            });
        }

        /// <summary>
        /// Returns the values of a contiguous set of bytes of SNES memory.
        /// </summary>
        /// <param name="address">
        ///     A 64-bit unsigned integer representing the starting memory address to be read.
        /// </param>
        /// <param name="bytesToRead">
        ///     A 32-bit signed integer representing the number of bytes to read.
        /// </param>
        /// <param name="timeOutInMs">
        ///     A 32-bit signed integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        public async Task<byte[]?> Read(ulong address, int bytesToRead = 1, int timeOutInMs = 4096)
        {
            return await Task<byte[]?>.Factory.StartNew(() =>
            {
                var buffer = new byte[bytesToRead];
                var attachTask = AttachDeviceIfNeeded(timeOutInMs);
                attachTask.Wait();

                if (!attachTask.Result)
                {
                    return null;
                }

                if (Status != ConnectionStatus.Connected)
                {
                    return null;
                }

                using ManualResetEvent readEvent = new(false);

                lock (_transmitLock)
                {
                    _messageHandler = (e) =>
                    {
                        if (!e.IsBinary || e.RawData == null)
                        {
                            return;
                        }

                        for (var i = 0; i < buffer.Length; i++)
                        {
                            buffer[i] = e.RawData[Math.Min(i, e.RawData.Length - 1)];
                        }

                        // ReSharper disable once AccessToDisposedClosure
                        readEvent.Set();
                    };

                    _logService.Log(LogLevel.Info, $"Reading {bytesToRead} byte(s) from {address:X}.");
                    Send(_requestFactory(
                        OpcodeType.GetAddress.ToString(),
                        operands: new List<string>(2)
                        {
                            AddressTranslator.TranslateAddress((uint)address, TranslationMode.Read)
                            .ToString("X", CultureInfo.InvariantCulture),
                            bytesToRead.ToString("X", CultureInfo.InvariantCulture)
                        }));

                    if (!readEvent.WaitOne(timeOutInMs))
                    {
                        _logService.Log(
                            LogLevel.Error, $"Failed to read {buffer.Length} byte(s) from {address:X}.");
                        Status = ConnectionStatus.Error;
                        return null;
                    }
                }

                _logService.Log(LogLevel.Info, $"Read {buffer.Length} byte(s) from {address:X} successfully.");
                return buffer;
            });
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
        /// <param name="timeOutInMs">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// An enumerator of the resulting strings of the response.
        /// </returns>
        private IEnumerable<string>? GetJsonResults(
            string requestName, IRequestType request, bool ignoreErrors = false, int timeOutInMs = 4096)
        {
            string[]? results = null;

            lock (_transmitLock)
            {
                using ManualResetEvent readEvent = new(false);

                _messageHandler = (e) =>
                {
                    _logService.Log(LogLevel.Info, $"Request {requestName} response received.");

                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>?>(e.Data);

                    if (dictionary is null)
                    {
                        return;
                    }

                    if (!dictionary.TryGetValue("Results", out var deserialized))
                    {
                        return;
                    }
                    
                    _logService.Log(LogLevel.Debug, $"Request {requestName} successfully deserialized.");
                    results = deserialized;
                    // ReSharper disable once AccessToDisposedClosure
                    readEvent.Set();
                };

                _logService.Log(LogLevel.Info, $"Request {requestName} sending.");

                if (!Send(request))
                {
                    _logService.Log(LogLevel.Error, $"Request {requestName} failed to send.");
                    return null;
                }

                _logService.Log(LogLevel.Info, $"Request {requestName} sent.");

                if (!readEvent.WaitOne(timeOutInMs))
                {
                    if (!ignoreErrors)
                    {
                        _logService.Log(LogLevel.Error, $"Request {requestName} failed.");
                        StatusChanged?.Invoke(this, ConnectionStatus.Error);
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
        /// <param name="timeOutInMs">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        private bool ConnectIfNeeded(int timeOutInMs = 4096)
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
                var disconnectTask = Disconnect();
                disconnectTask.Wait();
                _logService.Log(LogLevel.Debug, "Existing WebSocket class restarted.");
            }

            var connectTask = Connect(timeOutInMs);
            connectTask.Wait();

            return connectTask.Result;
        }

        /// <summary>
        /// Returns the device info of the attached device.
        /// </summary>
        /// <param name="timeOutInMs">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// An enumerator of the device info strings.
        /// </returns>
        private IEnumerable<string>? GetDeviceInfo(int timeOutInMs = 4096)
        {
            return GetJsonResults(
                "get device info", _requestFactory(OpcodeType.Info.ToString()), true, timeOutInMs);
        }

        /// <summary>
        /// Attaches to the selected device.
        /// </summary>
        /// <param name="timeOutInMs">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        private bool AttachDevice(int timeOutInMs = 4096)
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
                OpcodeType.Name.ToString(), operands: new List<string>(1) { AppName })))
            {
                _logService.Log(LogLevel.Error, "Could not register app name with connection.");
                Status = ConnectionStatus.Error;
                return false;
            }

            if (GetDeviceInfo(timeOutInMs) == null)
            {
                _logService.Log(LogLevel.Error, $"Device {_device} could not be attached.");
                Status = ConnectionStatus.Error;
                return false;
            }

            _logService.Log(LogLevel.Info, $"Device {_device} is successfully attached.");
            Status = ConnectionStatus.Connected;

            return true;
        }
    }
}
