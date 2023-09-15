using OpenTracker.Models.Logging;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This class contains auto-tracking log message data.
    /// </summary>
    public sealed class LogMessage
    {
        /// <summary>
        /// The <see cref="LogLevel"/> of the log message.
        /// </summary>
        public required LogLevel Level { get; init; }
        /// <summary>
        /// A <see cref="string"/> representing the content of the log message.
        /// </summary>
        public required string Content { get; init; }
    }
}