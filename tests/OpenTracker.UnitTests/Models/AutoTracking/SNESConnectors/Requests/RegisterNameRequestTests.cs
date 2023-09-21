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
public sealed class RegisterNameRequestTests
{
    private const string AppName = "OpenTracker";
        
    private readonly IAutoTrackerLogger _logger = Substitute.For<IAutoTrackerLogger>();

    private readonly RegisterNameRequest _sut;

    public RegisterNameRequestTests()
    {
        _sut = new RegisterNameRequest(_logger);
    }

    [Fact]
    public void Description_ShouldReturnExpected()
    {
        const string expected = "Register app name";

        _sut.Description.Should().Be(expected);
    }

    [Fact]
    public void StatusRequired_ShouldReturnExpected()
    {
        const ConnectionStatus expected = ConnectionStatus.Connected;

        _sut.StatusRequired.Should().Be(expected);
    }
        
    [Fact]
    public void ToJsonString_ShouldReturnExpected()
    {
        const string expected = $"{{\"Opcode\":\"Name\",\"Space\":\"SNES\",\"Operands\":[\"{AppName}\"]}}";

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
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IRegisterNameRequest.Factory>();
        var sut1 = factory();
            
        sut1.Should().BeOfType<RegisterNameRequest>();
        
        var sut2 = factory();
        
        sut1.Should().NotBeSameAs(sut2);
    }
}