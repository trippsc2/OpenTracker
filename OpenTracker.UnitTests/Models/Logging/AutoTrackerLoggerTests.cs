using Autofac;
using NSubstitute;
using OpenTracker.Models.Logging;
using OpenTracker.Utils;
using Xunit;

namespace OpenTracker.UnitTests.Models.Logging
{
    public class AutoTrackerLoggerTests
    {
        private readonly IFileManager _fileManager = Substitute.For<IFileManager>();
        private IStreamWriterWrapper? _streamWriter;
        private string? _factoryFilePath;

        private readonly AutoTrackerLogger _sut;

        public AutoTrackerLoggerTests()
        {
            IStreamWriterWrapper StreamWriterFactory(string path, bool _)
            {
                _factoryFilePath = path;
                _streamWriter = Substitute.For<IStreamWriterWrapper>();

                return _streamWriter;
            }

            _sut = new AutoTrackerLogger(_fileManager, StreamWriterFactory);
        }

        [Fact]
        public void Ctor_ShouldSetMinimumLogLevelToExpected()
        {
            const LogLevel expected = LogLevel.Trace;
            Assert.Equal(expected, _sut.MinimumLogLevel);
        }

        [Fact]
        public void Ctor_ShouldCallEnsureFileDoesNotExistOnFileManager()
        {
            _fileManager.Received(1).EnsureFileDoesNotExist(Arg.Any<string>());
        }

        [Theory]
        [InlineData(LogLevel.Debug, LogLevel.Trace)]
        [InlineData(LogLevel.Info, LogLevel.Trace)]
        [InlineData(LogLevel.Info, LogLevel.Debug)]
        [InlineData(LogLevel.Warn, LogLevel.Trace)]
        [InlineData(LogLevel.Warn, LogLevel.Debug)]
        [InlineData(LogLevel.Warn, LogLevel.Info)]
        [InlineData(LogLevel.Error, LogLevel.Trace)]
        [InlineData(LogLevel.Error, LogLevel.Debug)]
        [InlineData(LogLevel.Error, LogLevel.Info)]
        [InlineData(LogLevel.Error, LogLevel.Warn)]
        [InlineData(LogLevel.Fatal, LogLevel.Trace)]
        [InlineData(LogLevel.Fatal, LogLevel.Debug)]
        [InlineData(LogLevel.Fatal, LogLevel.Info)]
        [InlineData(LogLevel.Fatal, LogLevel.Warn)]
        [InlineData(LogLevel.Fatal, LogLevel.Error)]
        public void Log_ShouldNotCreateStreamWriter_WhenLogLevelIsLessThanMinimumLogLevel(
            LogLevel minimum, LogLevel actual)
        {
            _sut.MinimumLogLevel = minimum;
            _sut.Log(actual, "Test");
            
            Assert.Null(_streamWriter);
        }
        
        [Theory]
        [InlineData(LogLevel.Trace, LogLevel.Trace)]
        [InlineData(LogLevel.Trace, LogLevel.Debug)]
        [InlineData(LogLevel.Trace, LogLevel.Info)]
        [InlineData(LogLevel.Trace, LogLevel.Warn)]
        [InlineData(LogLevel.Trace, LogLevel.Error)]
        [InlineData(LogLevel.Trace, LogLevel.Fatal)]
        [InlineData(LogLevel.Debug, LogLevel.Debug)]
        [InlineData(LogLevel.Debug, LogLevel.Info)]
        [InlineData(LogLevel.Debug, LogLevel.Warn)]
        [InlineData(LogLevel.Debug, LogLevel.Error)]
        [InlineData(LogLevel.Debug, LogLevel.Fatal)]
        [InlineData(LogLevel.Info, LogLevel.Info)]
        [InlineData(LogLevel.Info, LogLevel.Warn)]
        [InlineData(LogLevel.Info, LogLevel.Error)]
        [InlineData(LogLevel.Info, LogLevel.Fatal)]
        [InlineData(LogLevel.Warn, LogLevel.Warn)]
        [InlineData(LogLevel.Warn, LogLevel.Error)]
        [InlineData(LogLevel.Warn, LogLevel.Fatal)]
        [InlineData(LogLevel.Error, LogLevel.Error)]
        [InlineData(LogLevel.Error, LogLevel.Fatal)]
        [InlineData(LogLevel.Fatal, LogLevel.Fatal)]
        public void Log_ShouldCreateStreamWriter_WhenLogLevelIsGreaterThanOrEqualToMinimumLogLevel(
            LogLevel minimum, LogLevel actual)
        {
            _sut.MinimumLogLevel = minimum;
            _sut.Log(actual, "Test");
            
            Assert.NotNull(_streamWriter);
        }

        [Fact]
        public void Log_ShouldCreateStreamWriterWithExpectedFilePath()
        {
            _sut.Log(LogLevel.Fatal, "Test");
            
            Assert.Equal(AppPath.AutoTrackingLogFilePath, _factoryFilePath);
        }

        [Fact]
        public void Log_ShouldCallWriteLineOnStreamWriter()
        {
            _sut.Log(LogLevel.Fatal, "Test");
            
            _streamWriter!.Received(1).WriteLine(Arg.Any<string?>());
        }

        [Fact]
        public void Log_ShouldCallDisposeOnStreamWriter()
        {
            _sut.Log(LogLevel.Fatal, "Test");
            
            _streamWriter!.Received(1).Dispose();
        }

        [Theory]
        [InlineData(LogLevel.Debug, LogLevel.Trace)]
        [InlineData(LogLevel.Info, LogLevel.Trace)]
        [InlineData(LogLevel.Info, LogLevel.Debug)]
        [InlineData(LogLevel.Warn, LogLevel.Trace)]
        [InlineData(LogLevel.Warn, LogLevel.Debug)]
        [InlineData(LogLevel.Warn, LogLevel.Info)]
        [InlineData(LogLevel.Error, LogLevel.Trace)]
        [InlineData(LogLevel.Error, LogLevel.Debug)]
        [InlineData(LogLevel.Error, LogLevel.Info)]
        [InlineData(LogLevel.Error, LogLevel.Warn)]
        [InlineData(LogLevel.Fatal, LogLevel.Trace)]
        [InlineData(LogLevel.Fatal, LogLevel.Debug)]
        [InlineData(LogLevel.Fatal, LogLevel.Info)]
        [InlineData(LogLevel.Fatal, LogLevel.Warn)]
        [InlineData(LogLevel.Fatal, LogLevel.Error)]
        public async void LogAsync_ShouldNotCreateStreamWriter_WhenLogLevelIsLessThanMinimumLogLevel(
            LogLevel minimum, LogLevel actual)
        {
            _sut.MinimumLogLevel = minimum;
            await _sut.LogAsync(actual, "Test");
            
            Assert.Null(_streamWriter);
        }
        
        [Theory]
        [InlineData(LogLevel.Trace, LogLevel.Trace)]
        [InlineData(LogLevel.Trace, LogLevel.Debug)]
        [InlineData(LogLevel.Trace, LogLevel.Info)]
        [InlineData(LogLevel.Trace, LogLevel.Warn)]
        [InlineData(LogLevel.Trace, LogLevel.Error)]
        [InlineData(LogLevel.Trace, LogLevel.Fatal)]
        [InlineData(LogLevel.Debug, LogLevel.Debug)]
        [InlineData(LogLevel.Debug, LogLevel.Info)]
        [InlineData(LogLevel.Debug, LogLevel.Warn)]
        [InlineData(LogLevel.Debug, LogLevel.Error)]
        [InlineData(LogLevel.Debug, LogLevel.Fatal)]
        [InlineData(LogLevel.Info, LogLevel.Info)]
        [InlineData(LogLevel.Info, LogLevel.Warn)]
        [InlineData(LogLevel.Info, LogLevel.Error)]
        [InlineData(LogLevel.Info, LogLevel.Fatal)]
        [InlineData(LogLevel.Warn, LogLevel.Warn)]
        [InlineData(LogLevel.Warn, LogLevel.Error)]
        [InlineData(LogLevel.Warn, LogLevel.Fatal)]
        [InlineData(LogLevel.Error, LogLevel.Error)]
        [InlineData(LogLevel.Error, LogLevel.Fatal)]
        [InlineData(LogLevel.Fatal, LogLevel.Fatal)]
        public async void LogAsync_ShouldCreateStreamWriter_WhenLogLevelIsGreaterThanOrEqualToMinimumLogLevel(
            LogLevel minimum, LogLevel actual)
        {
            _sut.MinimumLogLevel = minimum;
            await _sut.LogAsync(actual, "Test");
            
            Assert.NotNull(_streamWriter);
        }

        [Fact]
        public async void LogAsync_ShouldCreateStreamWriterWithExpectedFilePath()
        {
            await _sut.LogAsync(LogLevel.Fatal, "Test");
            
            Assert.Equal(AppPath.AutoTrackingLogFilePath, _factoryFilePath);
        }

        [Fact]
        public async void LogAsync_ShouldCallWriteLineOnStreamWriter()
        {
            await _sut.LogAsync(LogLevel.Fatal, "Test");
            
            await _streamWriter!.Received(1).WriteLineAsync(Arg.Any<string?>());
        }

        [Fact]
        public async void LogAsync_ShouldCallDisposeOnStreamWriter()
        {
            await _sut.LogAsync(LogLevel.Fatal, "Test");
            
            await _streamWriter!.Received(1).DisposeAsync();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IAutoTrackerLogger>();
            
            Assert.NotNull(sut as AutoTrackerLogger);
        }

        [Fact]
        public void AutofacSingleInstanceTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var value1 = scope.Resolve<IAutoTrackerLogger>();
            var value2 = scope.Resolve<IAutoTrackerLogger>();
            
            Assert.Equal(value1, value2);
        }
    }
}