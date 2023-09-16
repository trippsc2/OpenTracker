using Autofac;
using FluentAssertions;
using OpenTracker.Models.AutoTracking.Logging;
using OpenTracker.Models.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Logging;

public sealed class AutoTrackerLogServiceTests
{
    private const LogLevel Level = LogLevel.Trace;
    private const string Message = "Test log message.";
        
    private readonly AutoTrackerLogService _sut = new();

    [Fact]
    public void Log_ShouldAddLogToLogCollection()
    {
        _sut.LogCollection.Clear();
            
        _sut.Log(Level, Message);

        _sut.LogCollection.Should().HaveCount(1);

        var logMessage = _sut.LogCollection[0];

        logMessage.Level.Should().Be(Level);
        logMessage.Content.Should().Be(Message);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveAsInterfaceToSingleInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut1 = scope.Resolve<IAutoTrackerLogService>();
            
        Assert.IsType<AutoTrackerLogService>(sut1);
            
        var sut2 = scope.Resolve<IAutoTrackerLogService>();
            
        Assert.Equal(sut1, sut2);
    }
}