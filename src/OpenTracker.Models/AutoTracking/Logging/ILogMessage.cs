using OpenTracker.Models.Logging;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This interface contains auto-tracking log message data.
    /// </summary>
    public interface ILogMessage
    {
        /// <summary>
        /// The <see cref="LogLevel"/> of the log message.
        /// </summary>
        LogLevel Level { get; }
        
        /// <summary>
        /// A <see cref="string"/> representing the content of the log message.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// A factory for creating a new <see cref="ILogMessage"/> objects.
        /// </summary>
        /// <param name="level">
        ///     The <see cref="LogLevel"/> of the log message.
        /// </param>
        /// <param name="content">
        ///     A <see cref="string"/> representing the content of the log message.
        /// </param>
        /// <returns>
        ///     A new <see cref="ILogMessage"/> object.
        /// </returns>
        delegate ILogMessage Factory(LogLevel level, string content);
    }
}