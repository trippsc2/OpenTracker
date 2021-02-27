using OpenTracker.Models.AutoTracking.Logging;
using System;
using System.Collections.Generic;
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
                if (_status != value)
                {
                    _status = value;
                    OnStatusChanged();
                }
            }
        }

        public delegate NewSNESConnector Factory();

        public NewSNESConnector(IAutoTrackerLogService logService)
        {
            _logService = logService;
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
        /// Raises the StatusChanged event.
        /// </summary>
        private void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, Status);
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

            using var cancellationTokenSource = new CancellationTokenSource(timeOutInMs);

            _logService.Log(LogLevel.Info, "Attempting to connect to USB2SNES websocket at " +
                $"{_uriString}.");
            Status = ConnectionStatus.Connecting;

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

        public async Task<bool> AttachDeviceIfNeeded(int timeOutInMs = 4096)
        {
            return await Task<bool>.Factory.StartNew(() => false);
        }

        /// <summary>
        /// Disconnects from the web socket and unsets the web socket property.
        /// </summary>
        public async Task Disconnect()
        {
            using var cancellationTokenSource = new CancellationTokenSource(4096);

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

        public async Task<IEnumerable<string>?> GetDevices(int timeOutInMs = 4096)
        {
            try
            {
                using var cancellationTokenSource = new CancellationTokenSource(timeOutInMs);
                var sendData =
                    SerializeAndPackage(_requestFactory(OpcodeType.DeviceList.ToString()));

                var receivedData = new ArraySegment<byte>();
                var receiveTask = _socket.ReceiveAsync(receivedData, cancellationTokenSource.Token);
                await _socket.SendAsync(
                    sendData, WebSocketMessageType.Text, true, cancellationTokenSource.Token);
                var webSocketResult = await receiveTask;
            }
            catch (Exception ex)
            {
                
            }

            return await Task<IEnumerable<string>?>.Factory.StartNew(() => null);
        }

        public async Task<bool> Read(ulong address, byte[] buffer, int timeOutInMs = 4096)
        {
            return await Task<bool>.Factory.StartNew(() => false);
        }

        public async Task<byte?> Read(ulong address)
        {
            return await Task<byte?>.Factory.StartNew(() => null);
        }

        public void SetDevice(string device)
        {
        }
    }
}
