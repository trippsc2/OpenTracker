using System.Collections.ObjectModel;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.Logging
{
    /// <summary>
    /// This interface handles logging the autotracker.
    /// </summary>
    public interface IAutoTrackerLogService
    {
        ObservableCollection<ILogMessage> LogCollection { get; }

        void Log(LogLevel logLevel, string message);
    }
}