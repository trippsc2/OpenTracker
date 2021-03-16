using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.Values;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values
{
    public class AutoTrackAddressBoolTests
    {
        [Theory]
        [InlineData(0, 0, 1, 0)]
        [InlineData(0, 0, 2, 0)]
        [InlineData(1, 0, 1, 1)]
        [InlineData(1, 0, 2, 2)]
        [InlineData(0, 1, 1, 0)]
        [InlineData(0, 1, 2, 0)]
        [InlineData(1, 1, 1, 0)]
        [InlineData(1, 1, 2, 0)]
        [InlineData(2, 1, 1, 1)]
        [InlineData(2, 1, 2, 2)]
        public void MemoryAddressChanged_CurrentValueShouldMatchExpected(
            byte memoryAddressValue, byte comparison, int trueValue, int? expected)
        {
            var memoryAddress = new MemoryAddress();
            var sut = new AutoTrackAddressBool(memoryAddress, comparison, trueValue);

            memoryAddress.Value = memoryAddressValue;
            
            Assert.Equal(expected, sut.CurrentValue);
        }

        [Fact]
        public void MemoryAddressChanged_RaisesPropertyChanged()
        {
            var memoryAddress = new MemoryAddress();
            var sut = new AutoTrackAddressBool(memoryAddress, 0, 1);
            
            Assert.PropertyChanged(sut, nameof(IAutoTrackValue.CurrentValue),
                () => memoryAddress.Value = 1);
        }
    }
}