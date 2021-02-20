using System.Collections.ObjectModel;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking.Logging
{
    public interface IAutoTrackerLogService
    {
        ObservableCollection<LogMessage> LogCollection { get; }

        void Log(LogLevel logLevel, string message);
    }
}