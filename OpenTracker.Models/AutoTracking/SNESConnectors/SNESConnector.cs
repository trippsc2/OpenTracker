using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using OpenTracker.Models.AutoTracking.SNESConnectors.Requests;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using ReactiveUI;
using WebSocketSharp;
using LogLevel = OpenTracker.Models.Logging.LogLevel;

namespace OpenTracker.Models.AutoTracking.SNESConnectors
{
    /// <summary>
    /// This class contains logic for the SNES connector to (Q)USB2SNES.
    /// </summary>
    public class SNESConnector : ReactiveObject, ISNESConnector
    {
        private readonly IAutoTrackerLogger _logger;

        private readonly IWebSocketWrapper.Factory _webSocketFactory;
        private readonly IGetDevicesRequest.Factory _getDevicesFactory;
        private readonly IAttachDeviceRequest.Factory _attachDeviceFactory;
        private readonly IRegisterNameRequest.Factory _registerNameFactory;
        private readonly IGetDeviceInfoRequest.Factory _getDeviceInfoFactory;
        private readonly IReadMemoryRequest.Factory _readMemoryFactory;
        private readonly IMessageEventArgsWrapper.Factory _messageEventArgsWrapperFactory;

        private readonly object _transmitLock = new();

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
                    _socket.OnClose -= TraceWebSocketClose;
                    _socket.OnError -= TraceWebSocketError;
                    _socket.OnMessage -= TraceWebSocketMessage;
                    _socket.OnOpen -= TraceWebSocketOpen;
                }

                _socket = value;

                if (_socket is null)
                {
                    return;
                }
                
                _socket.OnClose += TraceWebSocketClose;
                _socket.OnError += TraceWebSocketError;
                _socket.OnMessage += TraceWebSocketMessage;
                _socket.OnOpen += TraceWebSocketOpen;
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
        /// <param name="getDevicesFactory">
        ///     An Autofac factory for creating new <see cref="IGetDevicesRequest"/> objects.
        /// </param>
        /// <param name="attachDeviceFactory">
        ///     An Autofac factory for creating new <see cref="IAttachDeviceRequest"/> objects.
        /// </param>
        /// <param name="registerNameFactory">
        ///     An Autofac factory for creating new <see cref="IRegisterNameRequest"/> objects.
        /// </param>
        /// <param name="getDeviceInfoFactory">
        ///     An Autofac factory for creating new <see cref="IGetDeviceInfoRequest"/> objects.
        /// </param>
        /// <param name="readMemoryFactory">
        ///     An Autofac factory for creating new <see cref="IReadMemoryRequest"/> objects.
        /// </param>
        /// <param name="messageEventArgsWrapperFactory">
        ///     An Autofac factory for creating new <see cref="IMessageEventArgsWrapper"/> objects.
        /// </param>
        public SNESConnector(
            IAutoTrackerLogger logger, IWebSocketWrapper.Factory webSocketFactory,
            IGetDevicesRequest.Factory getDevicesFactory, IAttachDeviceRequest.Factory attachDeviceFactory,
            IRegisterNameRequest.Factory registerNameFactory, IGetDeviceInfoRequest.Factory getDeviceInfoFactory,
            IReadMemoryRequest.Factory readMemoryFactory,
            IMessageEventArgsWrapper.Factory messageEventArgsWrapperFactory)
        {
            _logger = logger;
            
            _webSocketFactory = webSocketFactory;
            _getDevicesFactory = getDevicesFactory;
            _attachDeviceFactory = attachDeviceFactory;
            _registerNameFactory = registerNameFactory;
            _getDeviceInfoFactory = getDeviceInfoFactory;
            _readMemoryFactory = readMemoryFactory;
            _messageEventArgsWrapperFactory = messageEventArgsWrapperFactory;
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
                HandleException(exception);
            }
        }

        /// <summary>
        /// Connects to the USB2SNES web socket.
        /// </summary>
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
            void OpenHandler(object? sender, EventArgs e)
            {
                openEvent.Set();
            }

            _logger.Log(
                LogLevel.Debug,
                $"Attempting to connect to USB2SNES websocket at {Socket!.Url.OriginalString}.");
            Status = ConnectionStatus.Connecting;
            Socket!.OnOpen += OpenHandler;
            Socket.Connect();
            var result = openEvent.WaitOne(5000);
            Socket!.OnOpen -= OpenHandler;

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
            _logger.Log(LogLevel.Debug, "Attempting to disconnect and dispose of WebSocket.");
            Socket?.Close();
            _logger.Log(LogLevel.Debug, "Disconnected from websocket server.");
            Socket?.Dispose();
            _logger.Log(LogLevel.Debug, "Disposed WebSocket class.");
            Socket = null;
            _logger.Log(LogLevel.Debug, "Unset the Socket property.");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Status = ConnectionStatus.NotConnected;
            _logger.Log(LogLevel.Info, "Successfully disconnected websocket.");
        }

        public async Task<IEnumerable<string>?> GetDevicesAsync()
        {
            try
            {
                return await Task<IEnumerable<string>?>.Factory.StartNew(() =>
                    HandleRequest(_getDevicesFactory()));
            }
            catch (Exception exception)
            {
                HandleException(exception);
            }

            return null;
        }

        public async Task AttachDeviceAsync(string device)
        {
            try
            {
                await Task.Factory.StartNew(() => { AttachDevice(device); });
            }
            catch (Exception exception)
            {
                HandleException(exception);
            }
        }

        /// <summary>
        /// Sets the device to the specified device.
        /// </summary>
        /// <param name="device">
        ///     A <see cref="string"/> representing the device.
        /// </param>
        private void AttachDevice(string device)
        {
            Status = ConnectionStatus.Attaching;
            HandleRequest(_attachDeviceFactory(device));

            if (Status != ConnectionStatus.Error)
            {
                Status = ConnectionStatus.Connected;
            }
            
            HandleRequest(_registerNameFactory());
            var deviceInfo = HandleRequest(_getDeviceInfoFactory());

            if (deviceInfo is null)
            {
                throw new Exception("Failed to connect to device.");
            }
        }

        public async Task<byte[]?> ReadMemoryAsync(ulong address, int bytesToRead = 1)
        {
            try
            {
                return await Task<byte[]?>.Factory.StartNew(() =>
                    HandleRequest(_readMemoryFactory(address, bytesToRead)));
            }
            catch (Exception exception)
            {
                HandleException(exception);
            }

            return default;
        }

        /// <summary>
        /// Handles requests to the USB2SNES websocket.
        /// </summary>
        /// <param name="request">
        ///     The <see cref="IRequest{T}"/> to be handled.
        /// </param>
        /// <typeparam name="T">
        ///     The type of data to be returned by the request.
        /// </typeparam>
        /// <returns>
        ///     The data resulting from the request.
        /// </returns>
        /// <exception cref="Exception">
        ///     Thrown if the USB2SNES websocket is not in the required status.
        /// </exception>
        private T? HandleRequest<T>(IRequest<T> request)
        {
            lock (_transmitLock)
            {
                if (!ValidateRequestStatus(request))
                {
                    throw new Exception(
                        $"The request expects a status of {request.StatusRequired} and current is {Status}");
                }

                using var sendEvent = new ManualResetEvent(false);
                var result = SendRequestAndReceiveResult(request, sendEvent);

                return result;
            }
        }

        /// <summary>
        /// Validates the <see cref="Status"/> property is appropriate for the specified <see cref="IRequest{T}"/>
        /// </summary>
        /// <param name="request">
        ///     The <see cref="IRequest{T}"/> to be validated.
        /// </param>
        /// <typeparam name="T">
        ///     The type of data to be returned by the request.
        /// </typeparam>
        /// <returns>
        ///     A <see cref="bool"/> representing whether the <see cref="Status"/> is valid.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when the <see cref="IRequest{T}"/> requires an unexpected <see cref="ConnectionStatus"/>.
        /// </exception>
        private bool ValidateRequestStatus<T>(IRequest<T> request)
        {
            return request.StatusRequired switch
            {
                ConnectionStatus.SelectDevice => Status is ConnectionStatus.SelectDevice or ConnectionStatus.Connected,
                ConnectionStatus.Attaching => Status == ConnectionStatus.Attaching,
                ConnectionStatus.Connected => Status == ConnectionStatus.Connected,
                _ => throw new ArgumentOutOfRangeException(nameof(request))
            };
        }

        /// <summary>
        /// Sends the websocket request to USB2SNES and returns the processed result.
        /// </summary>
        /// <param name="request">
        ///     The <see cref="IRequest{T}"/> representing the request to be sent.
        /// </param>
        /// <param name="sendEvent">
        ///     A <see cref="ManualResetEvent"/> that waits for data to be received or a 2 second timeout.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the request response result.
        /// </typeparam>
        /// <returns>
        ///     The data returned from the request.
        /// </returns>
        /// <exception cref="Exception">
        ///     Thrown if the request fails to receive data.
        /// </exception>
        private T? SendRequestAndReceiveResult<T>(IRequest<T> request, ManualResetEvent sendEvent)
        {
            T? results = default;
            
            void HandleMessage(object? sender, MessageEventArgs e)
            {
                try
                {
                    results = request.ProcessResponseAndReturnResults(_messageEventArgsWrapperFactory(e), sendEvent);
                }
                catch (Exception exception)
                {
                    HandleException(exception);
                }
                
                _logger.Log(
                    LogLevel.Info, $"Response of request \'{request.Description}\' successfully received.");
            }

            if (request is not IRequest<Unit>)
            {
                Socket!.OnMessage += HandleMessage;
                _logger.Log(LogLevel.Debug, $"Subscribed to message handler successfully.");
            }
            
            _logger.Log(LogLevel.Debug, $"Attempting to send request \'{request.Description}\'.");
            var requestMessage = request.ToJsonString();
            _logger.Log(LogLevel.Trace, requestMessage);
            Socket!.Send(requestMessage);
            _logger.Log(LogLevel.Info, $"Sent request \'{request.Description}\' successfully.");

            if (request is IRequest<Unit>)
            {
                _logger.Log(
                    LogLevel.Debug,
                    $"Skipped waiting for received data for request \'{request.Description}\'.");
                return default;
            }

            var received = sendEvent.WaitOne(2000);
            Socket!.OnMessage -= HandleMessage;
            _logger.Log(LogLevel.Debug, $"Unsubscribed from message handler successfully.");

            if (!received)
            {
                throw new Exception($"Failed to receive data from request \'{request.Description}\'.");
            }

            _logger.Log(
                LogLevel.Info, $"Received response from request \'{request.Description}\' successfully.");
            return results;
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
        
        /// <summary>
        /// Logs the <see cref="IWebSocketWrapper.OnClose"/> events. 
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="CloseEventArgs"/>.
        /// </param>
        private void TraceWebSocketClose(object? sender, CloseEventArgs e)
        {
            _logger.Log(LogLevel.Trace, "WebSocket closed.");
        }
        
        /// <summary>
        /// Logs the <see cref="IWebSocketWrapper.OnError"/> events. 
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="ErrorEventArgs"/>.
        /// </param>
        private void TraceWebSocketError(object? sender, ErrorEventArgs e)
        {
            _logger.Log(LogLevel.Trace, $"WebSocket error: {e.Message}");
        }

        /// <summary>
        /// Logs the <see cref="IWebSocketWrapper.OnMessage"/> events. 
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="MessageEventArgs"/>.
        /// </param>
        private void TraceWebSocketMessage(object? sender, MessageEventArgs e)
        {
            _logger.Log(LogLevel.Trace, e.IsBinary ? $"[{string.Join(",", e.RawData)}]" : e.Data);
        }

        /// <summary>
        /// Logs the <see cref="IWebSocketWrapper.OnOpen"/> events. 
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="EventArgs"/>.
        /// </param>
        private void TraceWebSocketOpen(object? sender, EventArgs e)
        {
            _logger.Log(LogLevel.Trace, "WebSocket connection opened.");
        }
    }
}