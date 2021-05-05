using System.IO;
using System.Threading.Tasks;
using ReactiveUI;

namespace OpenTracker.Models.Logging
{
    /// <summary>
    /// This interface contains the logging logic.
    /// </summary>
    public abstract class LoggerBase : ReactiveObject, ILogger
    {
        private readonly string _filePath;

        private LogLevel _minimumLogLevel;
        public LogLevel MinimumLogLevel
        {
            get => _minimumLogLevel;
            set => this.RaiseAndSetIfChanged(ref _minimumLogLevel, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath">
        ///     A <see cref="string"/> representing the path to the log file.
        /// </param>
        protected LoggerBase(string filePath)
        {
            _filePath = filePath;

            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
        
        public void Log(LogLevel logLevel, string message)
        {
            if (logLevel < _minimumLogLevel)
            {
                return;
            }
            
            using var streamWriter = new StreamWriter(_filePath, true);
            streamWriter.WriteLine($"{logLevel.ToString().ToUpperInvariant()}: {message}");
        }

        public async Task LogAsync(LogLevel logLevel, string message)
        {
            if (logLevel < _minimumLogLevel)
            {
                return;
            }

            await using var streamWriter = new StreamWriter(_filePath);
            await streamWriter.WriteLineAsync($"{logLevel.ToString().ToUpperInvariant()}: {message}");
        }
    }
}