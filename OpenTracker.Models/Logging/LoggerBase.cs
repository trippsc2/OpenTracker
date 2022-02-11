using System.Threading.Tasks;
using OpenTracker.Utils;

namespace OpenTracker.Models.Logging
{
    /// <summary>
    /// This base class contains the logging logic.
    /// </summary>
    public abstract class LoggerBase : ILogger
    {
        private readonly IStreamWriterWrapper _streamWriter;
        
        public LogLevel MinimumLogLevel { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileManager">
        ///     The <see cref="IFileManager"/> that allows for non-destructive unit testing.
        /// </param>
        /// <param name="streamWriterFactory">
        ///     An Autofac factory for creating new <see cref="IStreamWriterWrapper"/> objects.
        /// </param>
        /// <param name="filePath">
        ///     A <see cref="string"/> representing the path to the log file.
        /// </param>
        protected LoggerBase(
            IFileManager fileManager, IStreamWriterWrapper.Factory streamWriterFactory, string filePath)
        {
            fileManager.EnsureFileDoesNotExist(filePath);
            
            _streamWriter = streamWriterFactory(filePath, true);
        }
        
        public void Log(LogLevel logLevel, string message)
        {
            if (logLevel < MinimumLogLevel)
            {
                return;
            }
            
            _streamWriter.WriteLine($"{logLevel.ToString().ToUpperInvariant()}: {message}");
        }

        public async Task LogAsync(LogLevel logLevel, string message)
        {
            if (logLevel < MinimumLogLevel)
            {
                return;
            }

            await _streamWriter.WriteLineAsync($"{logLevel.ToString().ToUpperInvariant()}: {message}");
        }
    }
}