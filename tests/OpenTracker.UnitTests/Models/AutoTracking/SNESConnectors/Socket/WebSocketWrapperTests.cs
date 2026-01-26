using Autofac;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors.Socket;

public class WebSocketWrapperTests
{
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IWebSocketWrapper.Factory>();
        var sut = factory("ws://localhost:8080");
            
        Assert.NotNull(sut as WebSocketWrapper);
    }
}