using Autofac;
using OpenTracker.Models.AutoTracking.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Logging
{
    public class LogMessageTests
    {
        private const LogLevel Level = LogLevel.Trace;
        private const string Content = "Test log message.";

        private readonly LogMessage _sut = new(Level, Content);

        [Fact]
        public void Ctor_ShouldSetLevelToExpected()
        {
            Assert.Equal(Level, _sut.Level);
        }

        [Fact]
        public void Ctor_ShouldSetMessageToExpected()
        {
            Assert.Equal(Content, _sut.Content);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ILogMessage.Factory>();
            var sut = factory(Level, Content);
            
            Assert.NotNull(sut as LogMessage);
        }
    }
}