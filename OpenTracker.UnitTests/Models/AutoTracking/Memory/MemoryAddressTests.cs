using Autofac;
using OpenTracker.Models.AutoTracking.Memory;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Memory
{
    public class MemoryAddressTests
    {
        private readonly MemoryAddress _sut = new MemoryAddress();

        [Fact]
        public void Reset_ShouldSetValueToNull()
        {
            _sut.Value = 1;
            _sut.Reset();
            
            Assert.Null(_sut.Value);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IMemoryAddress.Factory>();
            var sut = factory();
            
            Assert.NotNull(sut as MemoryAddress);
        }
    }
}