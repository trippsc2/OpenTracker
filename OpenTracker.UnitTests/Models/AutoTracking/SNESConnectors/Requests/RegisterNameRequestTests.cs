using System.Reactive;
using System.Threading;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using OpenTracker.Models.AutoTracking.SNESConnectors.Requests;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors.Requests;

public class RegisterNameRequestTests
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
            
        Assert.Equal(expected, _sut.Description);
    }

    [Fact]
    public void StatusRequired_ShouldReturnExpected()
    {
        const ConnectionStatus expected = ConnectionStatus.Connected;
            
        Assert.Equal(expected, _sut.StatusRequired);
    }
        
    [Fact]
    public void ToJsonString_ShouldReturnExpected()
    {
        var expected = $"{{\"Opcode\":\"Name\",\"Space\":\"SNES\",\"Operands\":[\"{AppName}\"]}}";
            
        Assert.Equal(expected, _sut.ToJsonString());
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldAlwaysReturnDefault()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
            
        Assert.Equal(Unit.Default, _sut.ProcessResponseAndReturnResults(
            messageEventArgs, new ManualResetEvent(false)));
    }
        
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IRegisterNameRequest.Factory>();
        var sut = factory();
            
        Assert.NotNull(sut as RegisterNameRequest);
    }
}