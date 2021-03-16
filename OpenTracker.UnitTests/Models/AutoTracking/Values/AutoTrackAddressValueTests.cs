using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.Values;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values
{
    public class AutoTrackAddressValueTests
    {
        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1, 0, 0, null)]
        [InlineData(0, 0, 1, null)]
        [InlineData(0, 1, 0, 0)]
        [InlineData(1, 1, 0, 1)]
        [InlineData(2, 1, 0, null)]
        [InlineData(0, 1, 1, 1)]
        [InlineData(1, 1, 1, null)]
        [InlineData(0, 1, 2, null)]
        [InlineData(0, 2, 0, 0)]
        [InlineData(1, 2, 0, 1)]
        [InlineData(2, 2, 0, 2)]
        [InlineData(3, 2, 0, null)]
        [InlineData(0, 2, 1, 1)]
        [InlineData(1, 2, 1, 2)]
        [InlineData(2, 2, 1, null)]
        [InlineData(0, 2, 2, 2)]
        [InlineData(1, 2, 2, null)]
        [InlineData(0, 2, 3, null)]
        public void MemoryAddressChanged_CurrentValueShouldMatchExpected(
            byte memoryAddressValue, byte maximum, int adjustment, int? expected)
        {
            var memoryAddress = new MemoryAddress();
            var sut = new AutoTrackAddressValue(memoryAddress, maximum, adjustment);

            memoryAddress.Value = memoryAddressValue;
            
            Assert.Equal(expected, sut.CurrentValue);
        }

        [Fact]
        public void MemoryAddressChanged_ShouldRaisePropertyChanged()
        {
            var memoryAddress = new MemoryAddress();
            var sut = new AutoTrackAddressValue(memoryAddress, 255, 0);
            
            Assert.PropertyChanged(sut, nameof(IAutoTrackValue.CurrentValue),
                () => memoryAddress.Value = 1);
        }
    }
}