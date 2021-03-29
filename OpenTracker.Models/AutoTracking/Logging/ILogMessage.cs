namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This is the interface for log messages for auto-tracking.
    /// </summary>
    public interface ILogMessage
    {
        /// <summary>
        ///     The log level of the log message.
        /// </summary>
        LogLevel Level { get; }
        
        /// <summary>
        ///     A string representing the log message.
        /// </summary>
        string Message { get; }

        /// <summary>
        ///     A factory for creating a new log message.
        /// </summary>
        /// <param name="level">
        ///     The log level of the log message.
        /// </param>
        /// <param name="message">
        ///     A string representing the message to be logged.
        /// </param>
        delegate ILogMessage Factory(LogLevel level, string message);
    }
}