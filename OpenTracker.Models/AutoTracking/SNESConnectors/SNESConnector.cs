using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using ReactiveUI;
using WebSocketSharp;
using LogLevel = OpenTracker.Models.Logging.LogLevel;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    public class SNESConnector : ReactiveObject, ISNESConnector
    {
        private readonly IAutoTrackerLogger _logger;

        private readonly IWebSocketWrapper.Factory _webSocketFactory;

        private string? _uri;

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
        
        private ConnectionStatus _status;
        public ConnectionStatus Status
        {
            get => _status;
            private set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">
        ///     The <see cref="IAutoTrackerLogger"/>.
        /// </param>
        /// <param name="webSocketFactory">
        ///     An Autofac factory for creating new <see cref="IWebSocketWrapper"/> objects.
        /// </param>
        public SNESConnector(IAutoTrackerLogger logger, IWebSocketWrapper.Factory webSocketFactory)
        {
            _logger = logger;
            _webSocketFactory = webSocketFactory;
        }

        public void SetURI(string uriString)
        {
            _uri = uriString;
            _logger.Log(LogLevel.Info, $"URI set to \'{_uri}\'.");
        }

        public async Task ConnectAsync()
        {
            try
            {
                await Task.Factory.StartNew(Connect);
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case AggregateException aggregateException:
                        foreach (var innerException in aggregateException.InnerExceptions)
                        {
                            HandleException(innerException);
                        }
                        break;
                    default:
                        HandleException(exception);
                        break;
                }
            }
        }

        private void Connect()
        {
            if (Socket is not null)
            {
                throw new Exception("The WebSocket client already exists and cannot be created.");
            }
            
            CreateWebSocket();

            using var openEvent = new ManualResetEvent(false);
            ConnectToWebSocket(openEvent);
        }

        /// <summary>
        /// Create the <see cref="IWebSocketWrapper"/> object and log it for debugging purposes.
        /// </summary>
        private void CreateWebSocket()
        {
            _logger.Log(LogLevel.Debug, $"Attempting to create WebSocket with URI \'{_uri}\'");
            Socket = _webSocketFactory(_uri!);
            _logger.Log(LogLevel.Debug, $"WebSocket successfully created with URI \'{_uri}\'");
        }

        /// <summary>
        /// Connects to the websocket at the specified URI.
        /// </summary>
        /// <param name="openEvent">
        ///     A <see cref="ManualResetEvent"/> that waits for the <see cref="WebSocket.OnOpen"/> event or a 5 second
        ///     timeout.
        /// </param>
        /// <exception cref="Exception">
        ///     Thrown if the connection to the websocket times out.
        /// </exception>
        private void ConnectToWebSocket(ManualResetEvent openEvent)
        {
            void OnOpen(object? sender, EventArgs e)
            {
                openEvent.Set();
            }

            _logger.Log(
                LogLevel.Debug,
                $"Attempting to connect to USB2SNES websocket at {Socket!.Url.OriginalString}.");
            Status = ConnectionStatus.Connecting;
            Socket!.OnOpen += OnOpen;
            Socket.Connect();
            var result = openEvent.WaitOne(5000);
            Socket!.OnOpen -= OnOpen;

            if (!result)
            {
                throw new Exception(
                    $"Failed to connect to USB2SNES websocket at \'{Socket.Url.OriginalString}\'.");
            }
            
            _logger.Log(
                LogLevel.Info,
                $"Successfully connected to USB2SNES websocket at \'{Socket.Url.OriginalString}\'.");
            Status = ConnectionStatus.SelectDevice;
        }

        public async Task DisconnectAsync()
        {
            await Task.Factory.StartNew(Disconnect);
        }

        /// <summary>
        /// Disconnects from the websocket and disposes of the <see cref="IWebSocketWrapper"/> object.
        /// </summary>
        private void Disconnect()
        {
            Socket?.Close();
            Socket?.Dispose();
            Socket = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Status = ConnectionStatus.NotConnected;
        }

        public Task<IEnumerable<string>?> GetDevicesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task SetDeviceAsync(string device)
        {
            throw new System.NotImplementedException();
        }

        public Task<byte[]?> ReadMemoryAsync(ulong address, int bytesToRead = 1)
        {
            throw new System.NotImplementedException();
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
        }

        /// <summary>
        /// Logs the exception and sets the <see cref="Status"/> property to <see cref="ConnectionStatus.Error"/>.
        /// </summary>
        /// <param name="exception">
        ///     The <see cref="Exception"/> to be handled.
        /// </param>
        private void HandleException(Exception exception)
        {
            _logger.Log(LogLevel.Error, exception.Message);
            Debug.WriteLine(exception.Message);
            Status = ConnectionStatus.Error;
        }
    }
}