using OpenTracker.Models.AutoTracking.Logging;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This class contains logic for the SNES connector using System.Net.WebSockets to connect to
    /// (Q)USB2SNES.
    /// </summary>
    public class NewSNESConnector : ISNESConnector
    {
        private readonly IAutoTrackerLogService _logService;

        private readonly ClientWebSocket _socket =
            new ClientWebSocket();
        private string? _uriString;

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
        /// <param name="timeOutInMS">
        /// A 32-bit integer representing the timeout in milliseconds.
        /// </param>
        /// <returns>
        /// A boolean representing whether the method is successful.
        /// </returns>
        public async Task<bool> Connect(int timeOutInMS = 4096)
        {
            if (_uriString is null)
            {
                _logService.Log(LogLevel.Error, "Connection attempted without a URI.");
                Status = ConnectionStatus.Error;
                return false;
            }

            var cancellationTokenSource = new CancellationTokenSource(timeOutInMS);

            _logService.Log(LogLevel.Info, "Attempting to connect to USB2SNES websocket at " +
                $"{_uriString}.");
            Status = ConnectionStatus.Connecting;
            await _socket.ConnectAsync(new Uri(_uriString), cancellationTokenSource.Token);

            cancellationTokenSource.Dispose();

            if (_socket.State == WebSocketState.Open)
            {
                _logService.Log(LogLevel.Info, "Successfully connected to USB2SNES websocket at " +
                    $"{_uriString}.");
                Status = ConnectionStatus.SelectDevice;
                return true;
            }

            _logService.Log(LogLevel.Error, "Failed to connect to USB2SNES websocket at " +
                $"{_uriString}.");
            Status = ConnectionStatus.Error;
            return false;
        }

        public async Task<bool> AttachDeviceIfNeeded(int timeOutInMS = 4096)
        {
            return await Task<bool>.Factory.StartNew(() => false);
        }

        public void Disconnect()
        {
        }

        public async Task<IEnumerable<string>?> GetDevices(int timeOutInMS = 4096)
        {
            return await Task<IEnumerable<string>?>.Factory.StartNew(() => null);
        }

        public async Task<bool> Read(ulong address, byte[] buffer, int timeOutInMS = 4096)
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
