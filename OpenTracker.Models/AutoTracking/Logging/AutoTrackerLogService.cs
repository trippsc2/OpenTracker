using System.Collections.ObjectModel;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    ///     This class handles logging the auto-tracker.
    /// </summary>
    public class AutoTrackerLogService : IAutoTrackerLogService
    {
        private readonly ILogMessage.Factory _messageFactory;
        
        public ObservableCollection<ILogMessage> LogCollection { get; } = new();
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="messageFactory">
        ///     An Autofac factory for creating log messages.
        /// </param>
        public AutoTrackerLogService(ILogMessage.Factory messageFactory)
        {
            _messageFactory = messageFactory;
        }

        public void Log(LogLevel logLevel, string message)
        {
            LogCollection.Add(_messageFactory(logLevel, message));
        }
    }
}
