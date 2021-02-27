using OpenTracker.Models.AutoTracking.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This class contains logic for the SNES connector using System.Net.WebSockets to connect to
    /// (Q)USB2SNES.
    /// </summary>
    public class NewSNESConnector : ISNESConnector
    {
        private readonly IAutoTrackerLogService _logService;
        private readonly IRequestType.Factory _requestFactory;

        private const string AppName = "OpenTracker";

        private ClientWebSocket _socket = new ClientWebSocket();
        private string? _uriString;
        private string? _device;

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

        public delegate NewSNESConnector Factory();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logService">
        /// The autotracker log service.
        /// </param>
        /// <param name="requestFactory">
        /// An Autofac factory for creating requests.
        /// </param>
        public NewSNESConnector(
            IAutoTrackerLogService logService, IRequestType.Factory requestFactory)
        {
            _logService = logService;
            _requestFactory = requestFactory;
        }
        
        /// <summary>
        /// Raises the StatusChanged event.
        /// </summary>
        private void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, Status);
        }

        /// <summary>
        /// Sends a message to the web socket.
        /// </summary>
        /// <param name="request">
        /// The payload of the request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        private async Task<bool> Send(IRequestType request, CancellationToken cancellationToken)
        {
            var sendData = SerializeAndPackage(request);

            try
            {
                await _socket.SendAsync(
                    sendData, WebSocketMessageType.Text, true, cancellationToken);
            }
            catch (Exception ex)
            {
                _logService.Log(LogLevel.Debug, ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Converts the specified request into a format that can be sent via web socket.
        /// </summary>
        /// <param name="request">
        /// The request to be serialized.
        /// </param>
        /// <returns>
        /// An array segment of bytes representing the message to be sent.
        /// </returns>
        private static ArraySegment<byte> SerializeAndPackage(IRequestType request)
        {
            var serialized = JsonConvert.SerializeObject(request);
            var message = Encoding.UTF8.GetBytes(serialized);
            
            return new ArraySegment<byte>(message);
        }

        /// <summary>
        /// Receives and aggregates packets from the web socket into a byte array.
        /// </summary>
        /// <param name="cancellationToken">
        /// A cancellation token.
        /// </param>
        /// <returns>
        /// A byte array representing the message.
        /// </returns>
        private async Task<byte[]> ReceiveMessageAsArray(CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            WebSocketReceiveResult? webSocketResult = null;
            var buffer = new ArraySegment<byte>(new byte[8192]);
            while (webSocketResult == null || !webSocketResult.EndOfMessage)
            {
                webSocketResult = await _socket.ReceiveAsync(buffer, cancellationToken);
                await memoryStream.WriteAsync(buffer.Array, buffer.Offset, webSocketResult.Count, cancellationToken);
            }

            return memoryStream.ToArray();
        }

        /// <summary>
        /// Receives and aggregates packets from the web socket into a message string.
        /// </summary>
        /// <param name="cancellationToken">
        /// A cancellation token.
        /// </param>
        /// <returns>
        /// A string representing the message.
        /// </returns>
        private async Task<string?> ReceiveMessageAsString(CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            WebSocketReceiveResult? webSocketResult = null;
            var buffer = new ArraySegment<byte>(new byte[8192]);
            while (webSocketResult == null || !webSocketResult.EndOfMessage)
            {
                webSocketResult = await _socket.ReceiveAsync(buffer, cancellationToken);
                await memoryStream.WriteAsync(buffer.Array, buffer.Offset, webSocketResult.Count, cancellationToken);
            }

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        /// <summary>
        /// Converts the message string from JSON into an enumerable of strings.
        /// </summary>
        /// <param name="message">
        /// A string representing the message.
        /// </param>
        /// <returns>
        /// An enumerable of strings from the message.
        /// </returns>
        private static IEnumerable<string>? ConvertMessage(string message)
        {
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(message);

            if (deserialized is null)
            {
                return null;
            }

            if (!deserialized.TryGetValue("Results", out var results))
            {
                return null;
            }

            return results;
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
        /// <param name="timeOutInMs">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// An enumerator of the resulting strings of the response.
        /// </returns>
        private async Task<IEnumerable<string>?> GetJsonResults(
            string requestName, IRequestType request, int timeOutInMs)
        {
            using var cancellationTokenSource = new CancellationTokenSource(timeOutInMs);
            var receiveTask = ReceiveMessageAsString(cancellationTokenSource.Token);
            
            _logService.Log(LogLevel.Info, $"Request {requestName} sending.");
            if (!await Send(request, cancellationTokenSource.Token))
            {
                _logService.Log(LogLevel.Error, $"Request {requestName} failed to send.");
                Status = ConnectionStatus.Error;
                return null;
            }
            
            _logService.Log(LogLevel.Info, $"Request {requestName} sent.");
            var webSocketResult = await receiveTask;

            if (!(webSocketResult is null))
            {
                _logService.Log(LogLevel.Info, $"Request {requestName} completed successfully.");
                return ConvertMessage(webSocketResult);
            }
            
            _logService.Log(LogLevel.Error, $"Request {requestName} received no data.");
            return null;
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
        private async Task<bool> SendOnly(string requestName, IRequestType request)
        {
            _logService.Log(LogLevel.Info, $"Request {requestName} is being sent.");

            using var cancellationTokenSource = new CancellationTokenSource();
            
            if (!await Send(request, cancellationTokenSource.Token))
            {
                _logService.Log(LogLevel.Error, $"Request {requestName} failed to send.");
                Status = ConnectionStatus.Error;
                return false;
            }

            _logService.Log(LogLevel.Info, $"Request {requestName} has been sent successfully.");
            return true;
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
        private async Task<IEnumerable<string>?> GetDeviceInfo(int timeOutInMs = 4096)
        {
            return await GetJsonResults(
                "get device info", _requestFactory(OpcodeType.Info.ToString()), timeOutInMs);
        }
        
        /// <summary>
        /// Sets the URI.
        /// </summary>
        /// <param name="uriString">
        /// A string representing the URI.
        /// </param>
        public void SetUri(string uriString)
        {
            _uriString = uriString;
        }

        /// <summary>
        /// Connects to the USB2SNES web socket.
        /// </summary>
        /// <param name="timeOutInMs">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        public async Task<bool> Connect(int timeOutInMs = 4096)
        {
            if (_uriString is null)
            {
                _logService.Log(LogLevel.Error, "Connection attempted without a URI.");
                Status = ConnectionStatus.Error;
                return false;
            }

            _logService.Log(LogLevel.Info, "Attempting to connect to USB2SNES websocket at " +
                $"{_uriString}.");
            Status = ConnectionStatus.Connecting;

            using var cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await _socket.ConnectAsync(new Uri(_uriString), cancellationTokenSource.Token);
            }
            catch (WebSocketException)
            {
                _logService.Log(LogLevel.Error, "Failed to connect to USB2SNES websocket at " +
                    $"{_uriString}.");
                Status = ConnectionStatus.Error;
                return false;
            }
            
            _logService.Log(LogLevel.Info, "Successfully connected to USB2SNES websocket at " +
                $"{_uriString}.");
            Status = ConnectionStatus.SelectDevice;
            return true;
        }

        /// <summary>
        /// Disconnects from the web socket and unsets the web socket property.
        /// </summary>
        public async Task Disconnect()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _socket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure, "Manual Close",
                    cancellationTokenSource.Token);
            }
            finally
            {
                Status = ConnectionStatus.NotConnected;
                _device = null;
                
                _socket.Dispose();
                _socket = new ClientWebSocket();
            }
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
            return await GetJsonResults(
                "get device list", _requestFactory(OpcodeType.DeviceList.ToString()),
                timeOutInMs);
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
        /// Attaches to the selected device.
        /// </summary>
        /// <param name="timeOutInMs">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        public async Task<bool> AttachDeviceIfNeeded(int timeOutInMs = 4096)
        {
            if (Status == ConnectionStatus.Connected)
            {
                _logService.Log(
                    LogLevel.Debug, "Already attached to device, skipping attachment attempt.");
                return true;
            }

            if (_device is null)
            {
                _logService.Log(LogLevel.Error, "Device is not selected.");
                return false;
            }

            _logService.Log(LogLevel.Info, $"Attempting to attach to device {_device}.");
            Status = ConnectionStatus.Attaching;

            if (!await SendOnly("attach", _requestFactory(
                OpcodeType.Attach.ToString(), operands: new List<string>(1) { _device })))
            {
                _logService.Log(LogLevel.Error, $"Device {_device} could not be attached.");
                Status = ConnectionStatus.Error;
                return false;
            }

            if (!await SendOnly("register name", _requestFactory(
                OpcodeType.Name.ToString(), operands: new List<string>(1) { AppName })))
            {
                _logService.Log(LogLevel.Error, "Could not register app name with connection.");
                Status = ConnectionStatus.Error;
                return false;
            }

            if (await GetDeviceInfo(timeOutInMs) is null)
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
        /// Returns the values of a contiguous set of bytes of SNES memory.
        /// </summary>
        /// <param name="address">
        ///     A 64-bit unsigned integer representing the starting memory address to be read.
        /// </param>
        /// <param name="bytesToRead">
        ///     A 32-bit signed integer representing the number of bytes to read.
        /// </param>
        /// <param name="timeOutInMs">
        ///     A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        public async Task<byte[]?> Read(ulong address, int bytesToRead = 1, int timeOutInMs = 4096)
        {
            if (!await AttachDeviceIfNeeded(timeOutInMs) || Status != ConnectionStatus.Connected)
            {
                _logService.Log(
                    LogLevel.Warn, "Attempted to read data without web socket in connected state.");
                return null;
            }

            using var cancellationTokenSource = new CancellationTokenSource(timeOutInMs);
            var receiveTask = ReceiveMessageAsArray(cancellationTokenSource.Token);
            
            _logService.Log(LogLevel.Info, $"Reading {bytesToRead} byte(s) from {address:X}.");
            
            if (!await Send(_requestFactory(
                OpcodeType.GetAddress.ToString(),
                operands: new List<string>(2)
                {
                    AddressTranslator.TranslateAddress((uint) address, TranslationMode.Read)
                        .ToString("X"),
                    bytesToRead.ToString("X")
                }), cancellationTokenSource.Token))
            {
                _logService.Log(LogLevel.Error, $"Failed to send read request for {address:X}.");
                Status = ConnectionStatus.Error;
                return null;
            }

            var webSocketResult = await receiveTask;

            if (webSocketResult is null)
            {
                _logService.Log(LogLevel.Error, $"Failed to receive data from {address:X}.");
                Status = ConnectionStatus.Error;
                return null;
            }

            if (webSocketResult.Length != bytesToRead)
            {
                _logService.Log(
                    LogLevel.Error, $"Received {webSocketResult.Length} byte(s) instead of {bytesToRead}.");
                Status = ConnectionStatus.Error;
                return null;
            }
            
            _logService.Log(
                LogLevel.Info, $"Received {webSocketResult.Length} byte(s) from {address:X}.");
            return webSocketResult;
        }
    }
}
