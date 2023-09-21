using Serilog;
using Serilog.Events;

namespace OpenTracker.Models.Logging;

/// <summary>
/// Serilog logger base type
/// </summary>
public abstract class LoggerBase : ILogger
{
    private readonly ILogger _logger;

    protected LoggerBase(string filePath)
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel
            .Debug()
#if DEBUG
            .MinimumLevel
            .Verbose()
            .WriteTo.Debug()
#endif
            .WriteTo.File(
                filePath,
                rollingInterval: RollingInterval.Hour,
                retainedFileCountLimit: 3)
            .CreateLogger();
    }

    public void Write(LogEvent logEvent)
    {
        _logger.Write(logEvent);
    }
}