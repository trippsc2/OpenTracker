using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values
{
    public class AutoTrackAddressValueTests
    {
        [Theory]
        [InlineData(null, null, 0, 0)]
        [InlineData(0, (byte)0, 0, 0)]
        [InlineData(null, (byte)1, 0, 0)]
        [InlineData(null, null, 0, 1)]
        [InlineData(null, (byte)0, 0, 1)]
        [InlineData(null, null, 1, 0)]
        [InlineData(0, (byte)0, 1, 0)]
        [InlineData(1, (byte)1, 1, 0)]
        [InlineData(null, (byte)2, 1, 0)]
        [InlineData(null, null, 1, 1)]
        [InlineData(1, (byte)0, 1, 1)]
        [InlineData(null, (byte)1, 1, 1)]
        [InlineData(null, null, 1, 2)]
        [InlineData(null, (byte)0, 1, 2)]
        [InlineData(null, null, 2, 0)]
        [InlineData(0, (byte)0, 2, 0)]
        [InlineData(1, (byte)1, 2, 0)]
        [InlineData(2, (byte)2, 2, 0)]
        [InlineData(null, (byte)3, 2, 0)]
        [InlineData(null, null, 2, 1)]
        [InlineData(1, (byte)0, 2, 1)]
        [InlineData(2, (byte)1, 2, 1)]
        [InlineData(null, (byte)2, 2, 1)]
        [InlineData(2, (byte)0, 2, 2)]
        [InlineData(null, (byte)1, 2, 2)]
        [InlineData(null, null, 2, 3)]
        [InlineData(null, (byte)0, 2, 3)]
        public void CurrentValue_ShouldEqualExpected(
            int? expected, byte? memoryAddressValue, byte maximum, int adjustment)
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