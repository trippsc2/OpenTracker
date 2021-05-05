using OpenTracker.Utils;

namespace OpenTracker.Models.Logging
{
    /// <summary>
    /// This class contains the logic for logging auto-tracking.
    /// </summary>
    public class AutoTrackerLogger : LoggerBase, IAutoTrackerLogger
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AutoTrackerLogger() : base(AppPath.AutoTrackingLogFilePath)
        {
            MinimumLogLevel = LogLevel.Warn;
#if DEBUG
            MinimumLogLevel = LogLevel.Debug;
#endif
        }
    }
}