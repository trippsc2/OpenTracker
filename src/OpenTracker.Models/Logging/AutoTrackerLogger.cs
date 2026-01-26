using OpenTracker.Utils;

namespace OpenTracker.Models.Logging;

/// <summary>
/// Auto-tracker logger
/// </summary>
public class AutoTrackerLogger : LoggerBase, IAutoTrackerLogger
{
    /// <summary>
    /// Initializes a new <see cref="AutoTrackerLogger"/> object
    /// </summary>
    public AutoTrackerLogger() : base(AppPath.AutoTrackingLogFilePath)
    {
    }
}