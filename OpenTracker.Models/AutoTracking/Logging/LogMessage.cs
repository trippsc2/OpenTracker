using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This is the class for log messages for autotracking.
    /// </summary>
    public class LogMessage
    {
        public LogLevel LogLevel { get; }
        public string Message { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logLevel">
        /// The logging level of the log message.
        /// </param>
        /// <param name="message">
        /// A string representing the message to be logged.
        /// </param>
        public LogMessage(LogLevel logLevel, string message)
        {
            LogLevel = logLevel;
            Message = message;
        }
    }
}