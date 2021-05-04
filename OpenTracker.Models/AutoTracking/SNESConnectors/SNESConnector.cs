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
        
        private readonly IRequest.Factory _requestFactory;
        private readonly IWebSocketWrapper.Factory _webSocketFactory;

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

        private IWebSocketWrapper? _socket;
        private IWebSocketWrapper? Socket
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
        ///     The <see cref="IAutoTrackerLogService"/>.
        /// </param>
        /// <param name="requestFactory">
        ///     An Autofac factory for creating new <see cref="IRequest"/> objects.
        /// </param>
        /// <param name="webSocketFactory">
        ///     An Autofac factory for creating new <see cref="IWebSocketWrapper"/> objects.
        /// </param>
        public SNESConnector(
            IAutoTrackerLogService logService, IRequest.Factory requestFactory,
            IWebSocketWrapper.Factory webSocketFactory)
        {
            _logService = logService;
            
            _requestFactory = requestFactory;
            _webSocketFactory = webSocketFactory;
        }

        public void SetUri(string uriString)
        {
            _uri = uriString;
            _logService.Log(LogLevel.Debug, $"URI set to {_uri}.");
        }

        public async Task<bool> ConnectAsync(int timeOutInMs = 4096)
        {
            return await Task<bool>.Factory.StartNew(() => Connect(timeOutInMs));
        }

        /// <summary>
        /// Connects to the USB2SNES web socket.
        /// </summary>
        /// <param name="timeOutInMs">
        ///     A <see cref="int"/> representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> representing whether the method is successful.
        /// </returns>
        private bool Connect(int timeOutInMs)
        {
            if (!ValidateURI())
            {
                return false;
            }

            Socket = _webSocketFactory(_uri!);
            _logService.Log(
                LogLevel.Info,
                $"Attempting to connect to USB2SNES websocket at {Socket.Url.OriginalString}.");
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
                _logService.Log(
                    LogLevel.Info,
                    $"Successfully connected to USB2SNES websocket at {Socket.Url.OriginalString}.");
                Status = ConnectionStatus.SelectDevice;
                return result;
            }

            _logService.Log(
                LogLevel.Error,
                $"Failed to connect to USB2SNES websocket at {Socket.Url.OriginalString}.");
            Status = ConnectionStatus.Error;

            return result;
        }

        /// <summary>
        /// Returns whether the URI is valid.
        /// </summary>
        /// <returns>
        ///     A <see cref="bool"/> representing whether the URI is valid.
        /// </returns>
        private bool ValidateURI()
        {
            if (_uri is not null)
            {
                _logService.Log(LogLevel.Debug, "Successfully tested URI for nullability.");
                return true;
            }

            _logService.Log(
                LogLevel.Info, "Unable to connect to USB2SNES websocket. The URI is empty.");
            Status = ConnectionStatus.Error;
            return false;
        }

        public async Task DisconnectAsync()
        {
            await Task.Factory.StartNew(Disconnect);
        }

        /// <summary>
        /// Disconnects from the web socket and unsets the web socket property.
        /// </summary>
        private void Disconnect()
        {
            Socket?.Close();
            Socket = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Status = ConnectionStatus.NotConnected;
        }

        public async Task<IEnumerable<string>?> GetDevicesAsync(int timeOutInMs = 4096)
        {
            return await Task<IEnumerable<string>?>.Factory.StartNew(() => GetDevices(timeOutInMs));
        }

        /// <summary>
        /// Returns the devices able to be selected.
        /// </summary>
        /// <param name="timeOutInMs">
        ///     A <see cref="int"/> representing the time in milliseconds before timeout.
        /// </param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}"/> of <see cref="string"/> representing the devices able to be selected.
        /// </returns>
        private IEnumerable<string>? GetDevices(int timeOutInMs)
        {
            if (!ConnectIfNeeded(timeOutInMs))
            {
                _logService.Log(LogLevel.Error, "Unable to get devices. Could not connect to websocket.");
                Status = ConnectionStatus.Error;
                return null;
            }

            return GetJsonResults(
                "get device list", _requestFactory(OpcodeType.DeviceList.ToString()), timeOutInMs);
        }

        /// <summary>
        /// Connects to the USB2SNES web socket, if not already connected.
        /// </summary>
        /// <param name="timeOutInMs">
        ///     A <see cref="int"/> representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> representing whether the method is successful.
        /// </returns>
        private bool ConnectIfNeeded(int timeOutInMs = 4096)
        {
            if (Connected)
            {
                _logService.Log(
                    LogLevel.Debug, "Already connected to USB2SNES websocket, skipping connection attempt.");
                return true;
            }

            if (Socket is not null)
            {
                _logService.Log(LogLevel.Debug, "Attempting to restart WebSocket class.");
                Disconnect();
                _logService.Log(LogLevel.Debug, "Existing WebSocket class restarted.");
            }

            return Connect(timeOutInMs);
        }

        /// <summary>
        /// Sends a request and receives a Json encoded response.
        /// </summary>
        /// <param name="requestName">
        ///     A <see cref="string"/> representing the type of request for logging purposes.
        /// </param>
        /// <param name="request">
        ///     The <see cref="IRequest"/> payload.
        /// </param>
        /// <param name="timeOutInMs">
        ///     A <see cref="int"/> representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}"/> of the resulting <see cref="string"/> of the response.
        /// </returns>
        private IEnumerable<string>? GetJsonResults(
            string requestName, IRequest request, int timeOutInMs = 4096)
        {
            string[]? results = null;

            lock (_transmitLock)
            {
                using ManualResetEvent readEvent = new(false);

                // ReSharper disable once AccessToDisposedClosure
                _messageHandler = e =>
                {
                    results = ConvertResponseResultsToArray(requestName, e, readEvent);
                };

                _logService.Log(LogLevel.Debug, $"Request {requestName} sending.");

                if (!Send(request))
                {
                    _logService.Log(LogLevel.Error, $"Request {requestName} failed to send.");
                    Status = ConnectionStatus.Error;
                    return null;
                }

                _logService.Log(LogLevel.Debug, $"Request {requestName} sent.");

                if (!readEvent.WaitOne(timeOutInMs))
                {
                    _logService.Log(LogLevel.Error, $"Request {requestName} timed out waiting for response.");
                    Status = ConnectionStatus.Error;

                    _messageHandler = null;
                    return null;
                }
            }

            _messageHandler = null;
            _logService.Log(LogLevel.Debug, $"Request {requestName} successful.");
            return results;
        }

        /// <summary>
        /// Sends a message to the web socket.
        /// </summary>
        /// <param name="request">
        ///     The <see cref="IRequest"/> payload.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> representing whether the method is successful.
        /// </returns>
        private bool Send(IRequest request)
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
        /// Returns the Results value of the JSON response to the specified request.
        /// </summary>
        /// <param name="requestName">
        ///     A <see cref="string"/> representing the name of the request for logging purposes.
        /// </param>
        /// <param name="e">
        ///     The <see cref="MessageEventArgs"/> containing the message data.
        /// </param>
        /// <param name="readEvent">
        ///     The <see cref="EventWaitHandle"/> used for timeout.
        /// </param>
        /// <returns>
        ///     A nullable array of <see cref="string"/> representing the Results value of the response.
        /// </returns>
        private string[]? ConvertResponseResultsToArray(
            string requestName, MessageEventArgs e, EventWaitHandle readEvent)
        {
            _logService.Log(LogLevel.Info, $"Request {requestName} response received.");

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>?>(e.Data);

            if (dictionary is null)
            {
                _logService.Log(
                    LogLevel.Error, $"Request {requestName} received invalid response. " +
                        "Not able to convert response to JSON.");
                return null;
            }

            if (!dictionary.TryGetValue("Results", out var deserialized))
            {
                _logService.Log(
                    LogLevel.Error, $"Request {requestName} received invalid response. " +
                        @"JSON response does not contain a ""Results"" key.");
                return null;
            }

            _logService.Log(LogLevel.Debug, $"Request {requestName} successfully deserialized.");
            // ReSharper disable once AccessToDisposedClosure
            readEvent.Set();
            return deserialized;
        }

        public async Task SetDeviceAsync(string device)
        {
            _device = device;

            await AttachDeviceIfNeededAsync();
        }

        public async Task<byte[]?> ReadMemoryAsync(ulong address, int bytesToRead = 1, int timeOutInMs = 4096)
        {
            return await Task<byte[]?>.Factory.StartNew(() => ReadMemory(address, bytesToRead, timeOutInMs));
        }

        /// <summary>
        /// Returns the byte values of the specified memory addresses.
        /// </summary>
        /// <param name="address">
        ///     A <see cref="ulong"/> representing the starting memory address.
        /// </param>
        /// <param name="bytesToRead">
        ///     A <see cref="int"/> representing the number of addresses to read.
        /// </param>
        /// <param name="timeOutInMs">
        ///     A <see cref="int"/> representing the time in milliseconds before timeout.
        /// </param>
        /// <returns>
        ///     An array of <see cref="byte"/> representing the values of the memory addresses.
        /// </returns>
        private byte[]? ReadMemory(ulong address, int bytesToRead, int timeOutInMs)
        {
            var buffer = new byte[bytesToRead];
            
            if (!AttachDeviceIfNeeded(timeOutInMs))
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
                // ReSharper disable once AccessToDisposedClosure
                _messageHandler = (e) => { PopulateBufferArrayWithResponseData(e, buffer, readEvent); };

                _logService.Log(LogLevel.Info, $"Reading {bytesToRead} byte(s) from {address:X}.");
                Send(_requestFactory(
                    OpcodeType.GetAddress.ToString(),
                    operands: new List<string>(2)
                    {
                        AddressTranslator.TranslateAddress((uint) address, TranslationMode.Read)
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
        }

        private void PopulateBufferArrayWithResponseData(MessageEventArgs e, byte[] buffer, ManualResetEvent readEvent)
        {
            if (!e.IsBinary || e.RawData == null)
            {
                _logService.Log(
                    LogLevel.Error, "Did not receive expected binary response from request to read memory.");
                Status = ConnectionStatus.Error;
                return;
            }

            if (e.RawData.Length != buffer.Length)
            {
                _logService.Log(
                    LogLevel.Error,
                    $"Expected to received {buffer.Length} bytes, but received {e.RawData.Length} instead.");
                Status = ConnectionStatus.Error;
                return;
            }

            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i] = e.RawData[i];
            }

            // ReSharper disable once AccessToDisposedClosure
            readEvent.Set();
        }

        /// <summary>
        /// Attaches to the selected device asynchronously, if not already attached.
        /// </summary>
        /// <param name="timeOutInMs">
        ///     A <see cref="int"/> representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> representing whether the method is successful.
        /// </returns>
        private async Task AttachDeviceIfNeededAsync(int timeOutInMs = 4096)
        {
            await Task<bool>.Factory.StartNew(() => AttachDeviceIfNeeded(timeOutInMs));
        }

        /// <summary>
        /// Attaches to the selected device, if not already attached.
        /// </summary>
        /// <param name="timeOutInMs">
        ///     A <see cref="int"/> representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> representing whether the method is successful.
        /// </returns>
        private bool AttachDeviceIfNeeded(int timeOutInMs)
        {
            if (Status != ConnectionStatus.Connected)
            {
                return ConnectIfNeeded(timeOutInMs) && AttachDevice(timeOutInMs);
            }

            _logService.Log(
                LogLevel.Debug, "Already attached to device, skipping attachment attempt.");
            return true;
        }

        /// <summary>
        /// Attaches to the selected device.
        /// </summary>
        /// <param name="timeOutInMs">
        ///     A <see cref="int"/> representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        ///     A <see cref="byte"/> representing whether the method is successful.
        /// </returns>
        private bool AttachDevice(int timeOutInMs = 4096)
        {
            if (_device is null)
            {
                _logService.Log(LogLevel.Error, $"Cannot attach to device. Device is not defined.");
                Status = ConnectionStatus.Error;
                return false;
            }

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

            if (GetDeviceInfo(timeOutInMs) is null)
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
        /// Returns the device info of the attached device.
        /// </summary>
        /// <param name="timeOutInMs">
        ///     A <see cref="int"/> representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}"/> of <see cref="string"/> representing the device info.
        /// </returns>
        private IEnumerable<string>? GetDeviceInfo(int timeOutInMs = 4096)
        {
            return GetJsonResults(
                "get device info", _requestFactory(OpcodeType.Info.ToString()), timeOutInMs);
        }

        /// <summary>
        /// Sends a request without a response.
        /// </summary>
        /// <param name="requestName">
        ///     A <see cref="string"/> representing the type of request for logging purposes.
        /// </param>
        /// <param name="request">
        ///     The <see cref="IRequest"/> payload.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> representing whether the method is successfully.
        /// </returns>
        private bool SendOnly(string requestName, IRequest request)
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
        /// Raises the StatusChanged event.
        /// </summary>
        private void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, Status);
        }

        /// <summary>
        /// Subscribes to the <see cref="IWebSocketWrapper.OnMessage"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event was sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="MessageEventArgs"/>.
        /// </param>
        private void HandleMessage(object? sender, MessageEventArgs e)
        {
            _messageHandler?.Invoke(e);
        }
    }
}
