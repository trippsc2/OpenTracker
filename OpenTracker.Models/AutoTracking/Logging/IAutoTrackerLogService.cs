using System.Collections.ObjectModel;
using OpenTracker.Models.Logging;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This interface handles logging the auto-tracker.
    /// </summary>
    public interface IAutoTrackerLogService
    {
        /// <summary>
        /// A <see cref="ObservableCollection{T}"/> of <see cref="LogMessage"/> representing the log messages.
        /// </summary>
        ObservableCollection<LogMessage> LogCollection { get; }

        /// <summary>
        /// Logs a new message.
        /// </summary>
        /// <param name="logLevel">
        ///     The <see cref="LogLevel"/> of the message.
        /// </param>
        /// <param name="content">
        ///     A <see cref="string"/> representing the content of the log message.
        /// </param>
        void Log(LogLevel logLevel, string content);
    }
}