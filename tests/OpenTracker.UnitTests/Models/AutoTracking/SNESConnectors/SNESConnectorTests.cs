using System;
using System.Runtime.Serialization;
using System.Threading;
using Autofac;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using OpenTracker.Models.AutoTracking.SNESConnectors.Requests;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using WebSocketSharp;
using Xunit;

// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors;

public class SNESConnectorTests
{
    private readonly IAutoTrackerLogger _logger = Substitute.For<IAutoTrackerLogger>();

    private readonly IWebSocketWrapper _webSocket = Substitute.For<IWebSocketWrapper>();
    private readonly IGetDevicesRequest _getDevicesRequest = Substitute.For<IGetDevicesRequest>();
    private readonly IAttachDeviceRequest _attachDeviceRequest = Substitute.For<IAttachDeviceRequest>();
    private readonly IRegisterNameRequest _registerNameRequest = Substitute.For<IRegisterNameRequest>();
    private readonly IGetDeviceInfoRequest _getDeviceInfoRequest = Substitute.For<IGetDeviceInfoRequest>();
    private readonly IReadMemoryRequest _readMemoryRequest = Substitute.For<IReadMemoryRequest>();
    private readonly IMessageEventArgsWrapper _messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();

    private readonly SNESConnector _sut;

    public SNESConnectorTests()
    {
        _webSocket.Url.Returns(new Uri("ws://localhost:23074"));
        _webSocket.When(x => x.Send(Arg.Any<string>())).Do(
            _ => _webSocket.OnMessage += Raise.Event<EventHandler<MessageEventArgs>>(
                _webSocket, FormatterServices.GetUninitializedObject(typeof(MessageEventArgs))));
            
        _getDevicesRequest.Description.Returns(string.Empty);
        _getDevicesRequest.StatusRequired.Returns(ConnectionStatus.SelectDevice);
        _getDevicesRequest.When(x => x.ProcessResponseAndReturnResults(
                Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>()))
            .Do(x => ((ManualResetEvent)x[1]).Set());

        _attachDeviceRequest.Description.Returns(string.Empty);
        _attachDeviceRequest.StatusRequired.Returns(ConnectionStatus.Attaching);
        _attachDeviceRequest.When(x => x.ProcessResponseAndReturnResults(
                Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>()))
            .Do(x => ((ManualResetEvent)x[1]).Set());

        _registerNameRequest.Description.Returns(string.Empty);
        _registerNameRequest.StatusRequired.Returns(ConnectionStatus.Connected);
        _registerNameRequest.When(x => x.ProcessResponseAndReturnResults(
                Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>()))
            .Do(x => ((ManualResetEvent)x[1]).Set());

        _getDeviceInfoRequest.Description.Returns(string.Empty);
        _getDeviceInfoRequest.StatusRequired.Returns(ConnectionStatus.Connected);
        _getDeviceInfoRequest.When(x => x.ProcessResponseAndReturnResults(
                Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>()))
            .Do(x => ((ManualResetEvent)x[1]).Set());

        _readMemoryRequest.Description.Returns(string.Empty);
        _readMemoryRequest.StatusRequired.Returns(ConnectionStatus.Connected);

        IWebSocketWrapper WebSocketFactory(string url, string[] protocols) => _webSocket;
        IGetDevicesRequest GetDevicesFactory() => _getDevicesRequest;
        IAttachDeviceRequest AttachDeviceFactory(string _) => _attachDeviceRequest;
        IRegisterNameRequest RegisterNameFactory() => _registerNameRequest;
        IGetDeviceInfoRequest GetDeviceInfoFactory() => _getDeviceInfoRequest;
        IReadMemoryRequest ReadMemoryFactory(ulong @ulong, int i) => _readMemoryRequest;
        IMessageEventArgsWrapper MessageEventArgsWrapperFactory(MessageEventArgs _) => _messageEventArgs;

        _sut = new SNESConnector(
            _logger, WebSocketFactory, GetDevicesFactory, AttachDeviceFactory, RegisterNameFactory,
            GetDeviceInfoFactory, ReadMemoryFactory, MessageEventArgsWrapperFactory);
    }

    [Fact]
    public void SetURI_ShouldCallLogOnLogger()
    {
        const string uriString = "Test";
        _sut.SetURI(uriString);
            
        _logger.Received(1).Information(Arg.Any<string>(), Arg.Any<string>());
    }

    [Fact]
    public async void ConnectAsync_ShouldChangeStatusToSelectDevice_WhenConnectedSuccessfully()
    {
        _webSocket.When(x => x.Connect()).Do(
            _ => _webSocket.OnOpen += Raise.Event<EventHandler>(
                _webSocket, EventArgs.Empty));
        await _sut.ConnectAsync();
            
        Assert.Equal(ConnectionStatus.SelectDevice, _sut.Status);
    }

    [Fact]
    public async void ConnectAsync_ShouldChangeStatusToError_WhenSocketAlreadyExists()
    {
        await _sut.ConnectAsync();
        await _sut.ConnectAsync();

        Assert.Equal(ConnectionStatus.Error, _sut.Status);
    }

    [Fact]
    public async void ConnectAsync_ShouldChangeStatusToError_WhenCannotConnect()
    {
        await _sut.ConnectAsync();

        Assert.Equal(ConnectionStatus.Error, _sut.Status);
    }

    [Fact]
    public async void DisconnectAsync_ShouldChangeStatusToNotConnected()
    {
        await _sut.ConnectAsync();
        await _sut.DisconnectAsync();
            
        Assert.Equal(ConnectionStatus.NotConnected, _sut.Status);
    }

    [Fact]
    public async void DisconnectAsync_ShouldCallCloseOnSocket()
    {
        await _sut.ConnectAsync();
        await _sut.DisconnectAsync();
            
        _webSocket.Received(1).Close();
    }

    [Fact]
    public async void DisconnectAsync_ShouldCallDisposeOnSocket()
    {
        await _sut.ConnectAsync();
        await _sut.DisconnectAsync();
            
        _webSocket.Received(1).Dispose();
    }

    [Fact]
    public async void GetDevicesAsync_ShouldChangeStatusToError_WhenStatusIsNotSelectDeviceOrConnected()
    {
        await _sut.GetDevicesAsync();
            
        Assert.Equal(ConnectionStatus.Error, _sut.Status);
    }

    [Fact]
    public async void GetDevicesAsync_ShouldCallProcessResponseAndReturnResultsOnGetDevicesRequest()
    {
        _webSocket.When(x => x.Connect()).Do(
            _ => _webSocket.OnOpen += Raise.Event<EventHandler>(
                _webSocket, EventArgs.Empty));
        await _sut.ConnectAsync();
        await _sut.GetDevicesAsync();

        _getDevicesRequest.Received(1).ProcessResponseAndReturnResults(
            Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>());
    }

    [Fact]
    public async void AttachDevice_ShouldCallToJsonStringOnAttachDeviceRequest()
    {
        _webSocket.When(x => x.Connect()).Do(
            _ => _webSocket.OnOpen += Raise.Event<EventHandler>(
                _webSocket, EventArgs.Empty));
        await _sut.ConnectAsync();
        await _sut.AttachDeviceAsync("Test");

        _attachDeviceRequest.Received(1).ToJsonString();
    }

    [Fact]
    public async void AttachDevice_ShouldCallToJsonStringOnRegisterNameRequest()
    {
        _webSocket.When(x => x.Connect()).Do(
            _ => _webSocket.OnOpen += Raise.Event<EventHandler>(
                _webSocket, EventArgs.Empty));
        await _sut.ConnectAsync();
        await _sut.AttachDeviceAsync("Test");

        _registerNameRequest.Received(1).ToJsonString();
    }

    [Fact]
    public async void AttachDevice_ShouldChangeStatusToError_WhenGetDeviceInfoReturnsNull()
    {
        _webSocket.When(x => x.Connect()).Do(
            _ => _webSocket.OnOpen += Raise.Event<EventHandler>(
                _webSocket, EventArgs.Empty));
        _getDeviceInfoRequest.ProcessResponseAndReturnResults(
            Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>()).ReturnsNull();
        await _sut.ConnectAsync();
        await _sut.AttachDeviceAsync("Test");

        Assert.Equal(ConnectionStatus.Error, _sut.Status);
    }

    [Fact]
    public async void AttachDevice_ShouldCallProcessResponseAndReturnResultsOnGetDeviceInfoRequest()
    {
        _webSocket.When(x => x.Connect()).Do(
            _ => _webSocket.OnOpen += Raise.Event<EventHandler>(
                _webSocket, EventArgs.Empty));
        _getDeviceInfoRequest.ProcessResponseAndReturnResults(
            Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>()).Returns(
            Array.Empty<string>());
        await _sut.ConnectAsync();
        await _sut.AttachDeviceAsync("Test");

        _getDeviceInfoRequest.Received(1).ProcessResponseAndReturnResults(
            Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>());
    }

    [Fact]
    public async void AttachDevice_ShouldChangeStatusToConnected_WhenGetDeviceInfoReturnsNotNull()
    {
        _webSocket.When(x => x.Connect()).Do(
            _ => _webSocket.OnOpen += Raise.Event<EventHandler>(
                _webSocket, EventArgs.Empty));
        _getDeviceInfoRequest.ProcessResponseAndReturnResults(
            Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>()).Returns(
            Array.Empty<string>());
        await _sut.ConnectAsync();
        await _sut.AttachDeviceAsync("Test");

        Assert.Equal(ConnectionStatus.Connected, _sut.Status);
    }

    [Fact]
    public async void ReadMemoryAsync_ShouldDoNothing_WhenStatusIsInvalid()
    {
        _webSocket.When(x => x.Connect()).Do(
            _ => _webSocket.OnOpen += Raise.Event<EventHandler>(
                _webSocket, EventArgs.Empty));
        await _sut.ConnectAsync();
        await _sut.ReadMemoryAsync(0x7ef010, 2);

        Assert.Equal(ConnectionStatus.Error, _sut.Status);
    }

    [Fact]
    public async void ReadMemoryAsync_ShouldCallProcessResponseAndReturnResultsOnReadMemoryRequest()
    {
        _webSocket.When(x => x.Connect()).Do(
            _ => _webSocket.OnOpen += Raise.Event<EventHandler>(
                _webSocket, EventArgs.Empty));
        _readMemoryRequest.When(x => x.ProcessResponseAndReturnResults(
                Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>()))
            .Do(x => ((ManualResetEvent)x[1]).Set());
        await _sut.ConnectAsync();
        await _sut.AttachDeviceAsync("Test");
        await _sut.ReadMemoryAsync(0x7ef010, 2);

        _readMemoryRequest.Received(1).ProcessResponseAndReturnResults(
            Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>());
    }

    [Fact]
    public async void ReadMemoryAsync_ShouldChangeStatusToError_WhenRequestThrowsException()
    {
        _webSocket.When(x => x.Connect()).Do(
            _ => _webSocket.OnOpen += Raise.Event<EventHandler>(
                _webSocket, EventArgs.Empty));
        _readMemoryRequest.When(x => x.ProcessResponseAndReturnResults(
                Arg.Any<IMessageEventArgsWrapper>(), Arg.Any<ManualResetEvent>()))
            .Do(_ => throw new Exception("Test"));
        await _sut.ConnectAsync();
        await _sut.AttachDeviceAsync("Test");
        await _sut.ReadMemoryAsync(0x7ef010, 2);
            
        Assert.Equal(ConnectionStatus.Error, _sut.Status);
    }

    [Fact]
    public async void TraceWebSocketClose_ShouldCallLogOnLogger()
    {
        await _sut.ConnectAsync();
        _webSocket.OnClose += Raise.Event<EventHandler<CloseEventArgs>>(
            _webSocket, FormatterServices.GetUninitializedObject(typeof(CloseEventArgs)));
            
        _logger.Received(1).Verbose(Arg.Any<string>());
    }

    [Fact]
    public async void TraceWebSocketError_ShouldCallLogOnLogger()
    {
        await _sut.ConnectAsync();
        _webSocket.OnError += Raise.Event<EventHandler<ErrorEventArgs>>(
            _webSocket, FormatterServices.GetUninitializedObject(typeof(ErrorEventArgs)));
            
        _logger.Received(1).Verbose(Arg.Any<string>(), Arg.Any<string>());
    }
        
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<ISNESConnector>();
            
        Assert.NotNull(sut as SNESConnector);
    }

    [Fact]
    public void AutofacSingleInstanceTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var value1 = scope.Resolve<ISNESConnector>();
        var value2 = scope.Resolve<ISNESConnector>();
            
        Assert.Equal(value1, value2);
    }
}