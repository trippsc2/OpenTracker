using OpenTracker.Models.AutoTracking.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Logging
{
    public class AutoTrackerLogServiceTests
    {
        private readonly IAutoTrackerLogService _sut;
        
        public AutoTrackerLogServiceTests()
        {
            _sut = new AutoTrackerLogService((logLevel, message) => new LogMessage(logLevel, message));
        }
        
        [Fact]
        public void Log_ShouldAddLogToLogCollection()
        {
            const LogLevel logLevel = LogLevel.Trace;
            const string message = "Test log message.";
            var logCollection = _sut.LogCollection;
            logCollection.Clear();
            
            Assert.PropertyChanged(logCollection, "Count", () => _sut.Log(logLevel, message));
            Assert.Single(logCollection);
        }
    }
}