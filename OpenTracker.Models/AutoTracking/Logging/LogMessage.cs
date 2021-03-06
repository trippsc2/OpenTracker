﻿using OpenTracker.Models.Logging;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This class contains auto-tracking log message data.
    /// </summary>
    public class LogMessage : ILogMessage
    {
        public LogLevel Level { get; }
        public string Content { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="level">
        ///     The <see cref="LogLevel"/> of the log message.
        /// </param>
        /// <param name="content">
        ///     A <see cref="string"/> representing the content of the log message.
        /// </param>
        public LogMessage(LogLevel level, string content)
        {
            Level = level;
            Content = content;
        }
    }
}