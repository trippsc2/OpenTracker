using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.Values;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values
{
    public class AutoTrackBitwiseIntegerValueTests
    {
        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0)]
        [InlineData(255, 0, 0, 0)]
        [InlineData(0, 1, 0, 0)]
        [InlineData(1, 1, 0, 1)]
        [InlineData(254, 1, 0, 0)]
        [InlineData(255, 1, 0, 1)]
        [InlineData(0, 3, 0, 0)]
        [InlineData(1, 3, 0, 1)]
        [InlineData(2, 3, 0, 2)]
        [InlineData(3, 3, 0, 3)]
        [InlineData(252, 3, 0, 0)]
        [InlineData(253, 3, 0, 1)]
        [InlineData(254, 3, 0, 2)]
        [InlineData(255, 3, 0, 3)]
        [InlineData(0, 0, 1, 0)]
        [InlineData(1, 0, 1, 0)]
        [InlineData(255, 0, 1, 0)]
        [InlineData(0, 1, 1, 0)]
        [InlineData(255, 1, 1, 0)]
        [InlineData(0, 3, 1, 0)]
        [InlineData(1, 3, 1, 0)]
        [InlineData(2, 3, 1, 1)]
        [InlineData(3, 3, 1, 1)]
        [InlineData(252, 3, 1, 0)]
        [InlineData(253, 3, 1, 0)]
        [InlineData(254, 3, 1, 1)]
        [InlineData(255, 3, 1, 1)]
        public void MemoryAddressChanged_CurrentValueShouldMatchExpected(
            byte memoryAddressValue, byte mask, int shift, int? expected)
        {
            var memoryAddress = new MemoryAddress();
            var sut = new AutoTrackBitwiseIntegerValue(memoryAddress, mask, shift);

            memoryAddress.Value = memoryAddressValue;
            
            Assert.Equal(expected, sut.CurrentValue);
        }

        [Fact]
        public void MemoryAddressChanged_ShouldRaisePropertyChanged()
        {
            var memoryAddress = new MemoryAddress();
            var sut = new AutoTrackBitwiseIntegerValue(memoryAddress, 255, 0);
            
            Assert.PropertyChanged(sut, nameof(IAutoTrackValue.CurrentValue),
                () => memoryAddress.Value = 1);
        }
    }
}