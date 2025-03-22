using System.Collections.ObjectModel;
using OpenTracker.Models.Logging;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This class handles logging the auto-tracker.
    /// </summary>
    public class AutoTrackerLogService : IAutoTrackerLogService
    {
        private readonly ILogMessage.Factory _messageFactory;
        
        public ObservableCollection<ILogMessage> LogCollection { get; } = new();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageFactory">
        ///     An Autofac factory for creating <see cref="ILogMessage"/> objects.
        /// </param>
        public AutoTrackerLogService(ILogMessage.Factory messageFactory)
        {
            _messageFactory = messageFactory;
        }

        public void Log(LogLevel logLevel, string content)
        {
            LogCollection.Add(_messageFactory(logLevel, content));
        }
    }
}
