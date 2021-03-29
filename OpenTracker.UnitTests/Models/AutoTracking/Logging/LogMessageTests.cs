using Autofac;
using OpenTracker.Models.AutoTracking.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Logging
{
    public class LogMessageTests
    {
        private const LogLevel Level = LogLevel.Trace;
        private const string Message = "Test log message.";

        private readonly LogMessage _sut = new(Level, Message);

        [Fact]
        public void Ctor_ShouldSetLevelToExpected()
        {
            Assert.Equal(Level, _sut.Level);
        }

        [Fact]
        public void Ctor_ShouldSetMessageToExpected()
        {
            Assert.Equal(Message, _sut.Message);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ILogMessage.Factory>();
            var sut = factory(Level, Message);
            
            Assert.NotNull(sut as LogMessage);
        }
    }
}