using System.Threading.Tasks;
using ReactiveUI;

namespace OpenTracker.Models.Logging
{
    /// <summary>
    /// This interface contains the logging logic.
    /// </summary>
    public interface ILogger : IReactiveObject
    {
        /// <summary>
        /// The minimum <see cref="LogLevel"/> to log.
        /// </summary>
        LogLevel MinimumLogLevel { get; set; }

        /// <summary>
        /// Logs a message of the specified level.
        /// </summary>
        /// <param name="logLevel">
        ///     The <see cref="LogLevel"/> of the message.
        /// </param>
        /// <param name="message">
        ///     A <see cref="string"/> representing the log message.
        /// </param>
        void Log(LogLevel logLevel, string message);

        /// <summary>
        /// Logs a message of the specified level asynchronously.
        /// </summary>
        /// <param name="logLevel">
        ///     The <see cref="LogLevel"/> of the message.
        /// </param>
        /// <param name="message">
        ///     A <see cref="string"/> representing the log message.
        /// </param>
        Task LogAsync(LogLevel logLevel, string message);
    }
}