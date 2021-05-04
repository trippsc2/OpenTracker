using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Memory;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Memory
{
    public class MemoryAddressProviderTests
    {
        private readonly IMemoryAddress.Factory _addressFactory = () => Substitute.For<IMemoryAddress>();
        private readonly MemoryAddressProvider _sut;

        public MemoryAddressProviderTests()
        {
            _sut = new MemoryAddressProvider(_addressFactory);
        }

        [Fact]
        public void Reset_ShouldCallResetOnMemoryAddresses()
        {
            var memoryAddress = _sut.MemoryAddresses[0x7ef000];
            _sut.Reset();
            
            memoryAddress.Received().Reset();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IMemoryAddressProvider>();
            
            Assert.NotNull(sut as MemoryAddressProvider);
        }

        [Fact]
        public void AutofacSingleInstanceTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var value1 = scope.Resolve<IMemoryAddressProvider>();
            var value2 = scope.Resolve<IMemoryAddressProvider>();
            
            Assert.Equal(value1, value2);
        }
    }
}