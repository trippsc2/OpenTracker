namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    ///     This is the class for log messages for auto-tracking.
    /// </summary>
    public class LogMessage : ILogMessage
    {
        /// <summary>
        ///     The log level of the message.
        /// </summary>
        public LogLevel Level { get; }
        
        /// <summary>
        ///     A string representing the message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="level">
        ///     The logging level of the log message.
        /// </param>
        /// <param name="message">
        ///     A string representing the message to be logged.
        /// </param>
        public LogMessage(LogLevel level, string message)
        {
            Level = level;
            Message = message;
        }
    }
}