using System.Collections.ObjectModel;
using OpenTracker.Models.Logging;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.AutoTracking.Logging;

[DependencyInjection(SingleInstance = true)]
public sealed class AutoTrackerLogService : IAutoTrackerLogService
{
    public ObservableCollection<LogMessage> LogCollection { get; } = new();
        
    public void Log(LogLevel logLevel, string content)
    {
        LogCollection.Add(new LogMessage { Level = logLevel, Content = content });
    }
}