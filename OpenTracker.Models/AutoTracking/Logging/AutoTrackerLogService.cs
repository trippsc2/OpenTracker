using System.Collections.ObjectModel;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This class handles logging the auto-tracker.
    /// </summary>
    public class AutoTrackerLogService : IAutoTrackerLogService
    {
        private readonly ILogMessage.Factory _messageFactory;
        
        public ObservableCollection<ILogMessage> LogCollection { get; } = new ObservableCollection<ILogMessage>();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageFactory">
        /// An Autofac factory for creating log messages.
        /// </param>
        public AutoTrackerLogService(ILogMessage.Factory messageFactory)
        {
            _messageFactory = messageFactory;
        }

        /// <summary>
        /// Logs a new message.
        /// </summary>
        /// <param name="logLevel">
        /// The log level of the message.
        /// </param>
        /// <param name="message">
        /// A string representing the content of the log message.
        /// </param>
        public void Log(LogLevel logLevel, string message)
        {
            LogCollection.Add(_messageFactory(logLevel, message));
        }
    }
}
