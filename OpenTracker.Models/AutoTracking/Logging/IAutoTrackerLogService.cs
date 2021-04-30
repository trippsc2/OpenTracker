using System.Collections.ObjectModel;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    ///     This interface handles logging the auto-tracker.
    /// </summary>
    public interface IAutoTrackerLogService
    {
        /// <summary>
        ///     The observable collection of log messages.
        /// </summary>
        ObservableCollection<ILogMessage> LogCollection { get; }

        /// <summary>
        ///     Logs a new message.
        /// </summary>
        /// <param name="logLevel">
        ///     The log level of the message.
        /// </param>
        /// <param name="message">
        ///     A string representing the content of the log message.
        /// </param>
        void Log(LogLevel logLevel, string message);
    }
}