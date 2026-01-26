using Autofac;
using OpenTracker.Models.AutoTracking.Logging;
using OpenTracker.Models.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Logging;

public class AutoTrackerLogServiceTests
{
    private const LogLevel Level = LogLevel.Trace;
    private const string Message = "Test log message.";
        
    private readonly AutoTrackerLogService _sut;
        
    public AutoTrackerLogServiceTests()
    {
        _sut = new AutoTrackerLogService((logLevel, message) => new LogMessage(logLevel, message));
    }
        
    [Fact]
    public void Log_ShouldAddLogToLogCollection()
    {
        var logCollection = _sut.LogCollection;
        logCollection.Clear();
        _sut.Log(Level, Message);

        Assert.Single(logCollection);
    }

    [Fact]
    public void LogCollection_ShouldRaisePropertyChanged()
    {
        var logCollection = _sut.LogCollection;
        logCollection.Clear();
            
        Assert.PropertyChanged(
            logCollection, "Count", () => _sut.Log(Level, Message));
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IAutoTrackerLogService>();
            
        Assert.NotNull(sut as AutoTrackerLogService);
    }

    [Fact]
    public void AutofacSingleInstanceTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var value1 = scope.Resolve<IAutoTrackerLogService>();
        var value2 = scope.Resolve<IAutoTrackerLogService>();
            
        Assert.Equal(value1, value2);
    }
}