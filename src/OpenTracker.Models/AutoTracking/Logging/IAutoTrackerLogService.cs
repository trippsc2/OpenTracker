using System.Collections.ObjectModel;
using OpenTracker.Models.Logging;

namespace OpenTracker.Models.AutoTracking.Logging;

/// <summary>
/// Represents the logging service for auto-tracker.
/// </summary>
public interface IAutoTrackerLogService
{
    /// <summary>
    /// An <see cref="ObservableCollection{T}"/> of <see cref="LogMessage"/> representing the log messages.
    /// </summary>
    ObservableCollection<LogMessage> LogCollection { get; }

    /// <summary>
    /// Logs a new message with the specified log level and content.
    /// </summary>
    /// <param name="logLevel">
    ///     A <see cref="LogLevel"/> representing the log level.
    /// </param>
    /// <param name="content">
    ///     A <see cref="string"/> representing the log message content.
    /// </param>
    void Log(LogLevel logLevel, string content);
}