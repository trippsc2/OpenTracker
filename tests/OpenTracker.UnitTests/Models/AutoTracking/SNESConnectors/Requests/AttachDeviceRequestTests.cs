using System.Reactive;
using System.Threading;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using OpenTracker.Models.AutoTracking.SNESConnectors.Requests;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors.Requests
{
    public class AttachDeviceRequestTests
    {
        private const string Device = "Test";
        
        private readonly IAutoTrackerLogger _logger = Substitute.For<IAutoTrackerLogger>();

        private readonly AttachDeviceRequest _sut;

        public AttachDeviceRequestTests()
        {
            _sut = new AttachDeviceRequest(_logger, Device);
        }

        [Fact]
        public void Description_ShouldReturnExpected()
        {
            const string expected = "Attach device \'Test\'";
            
            Assert.Equal(expected, _sut.Description);
        }

        [Fact]
        public void StatusRequired_ShouldReturnExpected()
        {
            const ConnectionStatus expected = ConnectionStatus.Attaching;
            
            Assert.Equal(expected, _sut.StatusRequired);
        }
        
        [Fact]
        public void ToJsonString_ShouldReturnExpected()
        {
            var expected = $"{{\"Opcode\":\"Attach\",\"Space\":\"SNES\",\"Operands\":[\"{Device}\"]}}";
            
            Assert.Equal(expected, _sut.ToJsonString());
        }

        [Fact]
        public void ProcessResponseAndReturnResults_ShouldAlwaysReturnDefault()
        {
            var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
            
            Assert.Equal(Unit.Default, _sut.ProcessResponseAndReturnResults(
                messageEventArgs, new ManualResetEvent(false)));
        }
        
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IAttachDeviceRequest.Factory>();
            var sut = factory("Test");
            
            Assert.NotNull(sut as AttachDeviceRequest);
        }
    }
}