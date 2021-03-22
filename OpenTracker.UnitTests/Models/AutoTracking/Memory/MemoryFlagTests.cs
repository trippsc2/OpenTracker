using NSubstitute;
using OpenTracker.Models.AutoTracking.Memory;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Memory
{
    public class MemoryFlagTests
    {
        private readonly IMemoryAddress _memoryAddress = Substitute.For<IMemoryAddress>();

        [Fact]
        public void Status_ShouldEqualNull_WhenMemoryAddressValueIsNull()
        {
            _memoryAddress.Value.Returns((byte?)null);
            var sut = new MemoryFlag(_memoryAddress, 0x1);
            
            Assert.Null(sut.Status);
        }
    }
}