using System.Collections.ObjectModel;
using OpenTracker.Models.Logging;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This class handles logging the auto-tracker.
    /// </summary>
    public class AutoTrackerLogService : IAutoTrackerLogService
    {
        public ObservableCollection<LogMessage> LogCollection { get; } = new();
        
        public void Log(LogLevel logLevel, string content)
        {
            LogCollection.Add(new LogMessage { Level = logLevel, Content = content });
        }
    }
}
