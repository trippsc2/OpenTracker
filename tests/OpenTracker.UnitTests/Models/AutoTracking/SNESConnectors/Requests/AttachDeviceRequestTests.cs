using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Threading;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using OpenTracker.Models.AutoTracking.SNESConnectors.Requests;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors.Requests;

[ExcludeFromCodeCoverage]
public sealed class AttachDeviceRequestTests
{
    private const string Device = "Test";
        
    private readonly IAutoTrackerLogger _logger = Substitute.For<IAutoTrackerLogger>();

    private readonly AttachDeviceRequest _sut;

    public AttachDeviceRequestTests()
    {
        _sut = new AttachDeviceRequest(_logger, Device);
    }

    [Fact]
    public void Description_ShouldReturnExpected()
    {
        const string expected = "Attach device \'Test\'";

        _sut.Description.Should().Be(expected);
    }

    [Fact]
    public void StatusRequired_ShouldReturnExpected()
    {
        const ConnectionStatus expected = ConnectionStatus.Attaching;

        _sut.StatusRequired.Should().Be(expected);
    }
        
    [Fact]
    public void ToJsonString_ShouldReturnExpected()
    {
        const string expected = $"{{\"Opcode\":\"Attach\",\"Space\":\"SNES\",\"Operands\":[\"{Device}\"]}}";

        _sut.ToJsonString().Should().Be(expected);
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldAlwaysReturnDefault()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();

        _sut.ProcessResponseAndReturnResults(
                messageEventArgs,
                new ManualResetEvent(false))
            .Should().Be(Unit.Default);
    }
        
    [Fact]
    public void AutofacResolve_ShouldResolveAsInterfaceToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IAttachDeviceRequest.Factory>();
        var sut1 = factory("Test");
        
        sut1.Should().BeOfType<AttachDeviceRequest>();
        
        var sut2 = factory("Test");
        
        sut1.Should().NotBeSameAs(sut2);
    }
}