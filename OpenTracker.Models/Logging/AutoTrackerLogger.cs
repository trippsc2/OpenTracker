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
        /// <param name="fileManager">
        ///     The <see cref="IFileManager"/> that allows for non-destructive unit testing.
        /// </param>
        /// <param name="streamWriterFactory">
        ///     An Autofac factory for creating new <see cref="IStreamWriterWrapper"/> objects.
        /// </param>
        public AutoTrackerLogger(IFileManager fileManager, IStreamWriterWrapper.Factory streamWriterFactory)
            : base(fileManager, streamWriterFactory, AppPath.AutoTrackingLogFilePath)
        {
            MinimumLogLevel = LogLevel.Info;
#if DEBUG
            MinimumLogLevel = LogLevel.Trace;
#endif
        }
    }
}