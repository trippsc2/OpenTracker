using Autofac;
using OpenTracker.Models.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.Logging
{
    public class StreamWriterWrapperTests
    {
        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IStreamWriterWrapper.Factory>();
            var sut = factory("Test", true);
            
            Assert.NotNull(sut as StreamWriterWrapper);
        }
    }
}