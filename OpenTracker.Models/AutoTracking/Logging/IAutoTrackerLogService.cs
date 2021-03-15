using System.Collections.ObjectModel;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This interface handles logging the auto-tracker.
    /// </summary>
    public interface IAutoTrackerLogService
    {
        ObservableCollection<ILogMessage> LogCollection { get; }

        void Log(LogLevel logLevel, string message);
    }
}