using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors.Socket;

[ExcludeFromCodeCoverage]
public sealed class WebSocketWrapperTests
{
    [Fact]
    public void AutofacResolve_ShouldResolveInterfaceToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IWebSocketWrapper.Factory>();
        var sut1 = factory("ws://localhost:8080");

        sut1.Should().BeOfType<WebSocketWrapper>();
        
        var sut2 = factory("ws://localhost:8080");

        sut1.Should().NotBeSameAs(sut2);
    }
}