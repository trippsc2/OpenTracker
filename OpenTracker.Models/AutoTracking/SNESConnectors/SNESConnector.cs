using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using OpenTracker.Models.AutoTracking.SNESConnectors.Requests;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.SNESConnectors;

public sealed class SNESConnector : ReactiveObject, ISNESConnector
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
    
    [Reactive]
    public ConnectionStatus Status { get; private set; }

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
        _logger.Information("URI set to \'{UriValue}\'", _uri);
    }

    public Task ConnectAsync()
    {
        return Task.Run(() =>
        {
            try
            {
                Connect();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        });
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

    private void CreateWebSocket()
    {
        _logger.Debug("Attempting to create WebSocket with URI \'{Uri}\'",
            _uri);
        Socket = _webSocketFactory(_uri!);
        _logger.Debug("WebSocket successfully created with URI \'{Uri}\'",
            _uri);
    }

    private void ConnectToWebSocket(EventWaitHandle openEvent)
    {
        void OpenHandler(object? sender, EventArgs e)
        {
            openEvent.Set();
        }

        _logger.Debug("Attempting to connect to USB2SNES websocket at {Uri}",
            _socket!.Url.OriginalString);
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
            
        _logger.Information("Successfully connected to USB2SNES websocket at \'{Uri}\'",
            _socket!.Url.OriginalString);
        Status = ConnectionStatus.SelectDevice;
    }

    public Task DisconnectAsync()
    {
        return Task.Run(Disconnect);
    }

    private void Disconnect()
    {
        _logger.Debug("Attempting to disconnect and dispose of WebSocket");
        Socket?.Close();
        _logger.Debug("Disconnected from websocket server");
        Socket?.Dispose();
        _logger.Debug("Disposed WebSocket class");
        Socket = null;
        _logger.Debug("Unset the Socket property");
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Status = ConnectionStatus.NotConnected;
        _logger.Information("Successfully disconnected websocket");
    }

    public Task<IEnumerable<string>?> GetDevicesAsync()
    {
        return Task.Run(() =>
        {
            try
            {
                return HandleRequest(_getDevicesFactory());
            }
            catch (Exception exception)
            {
                HandleException(exception);
                return null;
            }
        });
    }

    public Task AttachDeviceAsync(string device)
    {
        return Task.Run(() =>
        {
            try
            {
                AttachDevice(device);
            }
            catch (Exception exception)
            {
                HandleException(exception);
            }
        });
    }

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

    public Task<byte[]?> ReadMemoryAsync(ulong address, int bytesToRead = 1)
    {
        return Task.Run(() =>
        {
            try
            {
                return HandleRequest(_readMemoryFactory(address, bytesToRead));
            }
            catch (Exception exception)
            {
                HandleException(exception);
                return null;
            }
        });
    }

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

    private T? SendRequestAndReceiveResult<T>(IRequest<T> request, ManualResetEvent sendEvent)
    {
        T? results = default;

        if (request is not IRequest<Unit>)
        {
            Socket!.OnMessage += HandleMessage;
            _logger.Debug("Subscribed to message handler successfully");
        }
            
        _logger.Debug("Attempting to send request \'{Request}\'",
            request.Description);
        var requestMessage = request.ToJsonString();
        _logger.Verbose("{Request}", requestMessage);
        Socket!.Send(requestMessage);
        _logger.Information("Sent request \'{Request}\' successfully",
            request.Description);

        if (request is IRequest<Unit>)
        {
            _logger.Debug("Skipped waiting for received data for request \'{Request}\'",
                request.Description);
            return default;
        }

        var received = sendEvent.WaitOne(2000);
        Socket!.OnMessage -= HandleMessage;
        _logger.Debug("Unsubscribed from message handler successfully");

        if (!received)
        {
            throw new Exception($"Failed to receive data from request \'{request.Description}\'.");
        }

        _logger.Information("Received response from request \'{Request}\' successfully",
            request.Description);
        return results;

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
                
            _logger.Information("Response of request \'{Request}\' successfully received",
                request.Description);
        }
    }

    /// <summary>
    /// Logs the exception and sets the <see cref="Status"/> property to <see cref="ConnectionStatus.Error"/>.
    /// </summary>
    /// <param name="exception">
    ///     The <see cref="Exception"/> to be handled.
    /// </param>
    private void HandleException(Exception exception)
    {
        _logger.Error("{ExceptionMessage}", exception.Message);
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
        _logger.Verbose("WebSocket closed by event");
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
        _logger.Verbose("WebSocket error: {ExceptionMessage}",
            e.Message);
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
        _logger.Verbose("{MessageData}",
            e.IsBinary ? $"[{string.Join(",", e.RawData)}]" : e.Data);
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
        _logger.Verbose("WebSocket connection opened");
    }
}