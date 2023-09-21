using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Logging;

/// <summary>
/// Auto-tracker logger
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class AutoTrackerLogger : LoggerBase, IAutoTrackerLogger
{
    /// <summary>
    /// Initializes a new <see cref="AutoTrackerLogger"/> object
    /// </summary>
    public AutoTrackerLogger() : base(AppPath.AutoTrackingLogFilePath)
    {
    }
}