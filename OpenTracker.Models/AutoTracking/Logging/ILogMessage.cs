using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This is the interface for log messages for autotracking.
    /// </summary>
    public interface ILogMessage
    {
        LogLevel LogLevel { get; }
        string Message { get; }

        delegate ILogMessage Factory(LogLevel logLevel, string message);
    }
}