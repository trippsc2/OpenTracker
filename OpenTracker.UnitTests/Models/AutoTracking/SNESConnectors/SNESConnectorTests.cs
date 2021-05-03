using System;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Logging;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors
{
    public class SNESConnectorTests
    {
        private readonly IAutoTrackerLogService _logService = Substitute.For<IAutoTrackerLogService>();

        private static IWebSocketWrapper? _webSocket;
        private static string? _uri;
        
        private readonly IRequest.Factory _requestFactory =
            (opcode, space, flags, operands) => new Request(opcode, space, flags, operands);
        private readonly IWebSocketWrapper.Factory _webSocketFactory = (uri, _) =>
        {
            _uri = uri;
            _webSocket = Substitute.For<IWebSocketWrapper>();
            _webSocket.Url.Returns(new Uri(uri));
            
            return _webSocket;
        };

        private readonly SNESConnector _sut;

        public SNESConnectorTests()
        {
            _sut = new SNESConnector(_logService, _requestFactory, _webSocketFactory);
        }

        [Fact]
        public void SetUri_ShouldLogUriBeingSet()
        {
            _sut.SetUri("ws://localhost:8080");
            
            _logService.Received(1).Log(Arg.Any<LogLevel>(), Arg.Any<string>());
        }

        [Fact]
        public async void SetUri_ShouldSetUriInWebSocket()
        {
            const string uri = "ws://localhost:8080";
            _sut.SetUri(uri);
            await _sut.ConnectAsync();
            
            Assert.Equal(uri, _uri);
        }

        [Fact]
        public async void ConnectAsync_ShouldReturnFalse_WhenURIIsNull()
        {
            Assert.False(await _sut.ConnectAsync());
        }
    }
}