using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values
{
    public class AutoTrackFlagBoolTests
    {
        [Theory]
        [InlineData(null, null, 1, 0)]
        [InlineData(0, (byte)0, 1, 0)]
        [InlineData(0, (byte)1, 1, 0)]
        [InlineData(0, (byte)254, 1, 0)]
        [InlineData(0, (byte)255, 1, 0)]
        [InlineData(null, null, 1, 1)]
        [InlineData(0, (byte)0, 1, 1)]
        [InlineData(1, (byte)1, 1, 1)]
        [InlineData(0, (byte)254, 1, 1)]
        [InlineData(1, (byte)255, 1, 1)]
        [InlineData(null, null, 1, 2)]
        [InlineData(0, (byte)0, 1, 2)]
        [InlineData(2, (byte)1, 1, 2)]
        [InlineData(0, (byte)254, 1, 2)]
        [InlineData(2, (byte)255, 1, 2)]
        [InlineData(null, null, 2, 0)]
        [InlineData(0, (byte)0, 2, 0)]
        [InlineData(0, (byte)1, 2, 0)]
        [InlineData(0, (byte)2, 2, 0)]
        [InlineData(0, (byte)3, 2, 0)]
        [InlineData(0, (byte)252, 2, 0)]
        [InlineData(0, (byte)253, 2, 0)]
        [InlineData(0, (byte)254, 2, 0)]
        [InlineData(0, (byte)255, 2, 0)]
        [InlineData(null, null, 2, 1)]
        [InlineData(0, (byte)0, 2, 1)]
        [InlineData(0, (byte)1, 2, 1)]
        [InlineData(1, (byte)2, 2, 1)]
        [InlineData(1, (byte)3, 2, 1)]
        [InlineData(0, (byte)252, 2, 1)]
        [InlineData(0, (byte)253, 2, 1)]
        [InlineData(1, (byte)254, 2, 1)]
        [InlineData(1, (byte)255, 2, 1)]
        [InlineData(null, null, 2, 2)]
        [InlineData(0, (byte)0, 2, 2)]
        [InlineData(0, (byte)1, 2, 2)]
        [InlineData(2, (byte)2, 2, 2)]
        [InlineData(2, (byte)3, 2, 2)]
        [InlineData(0, (byte)252, 2, 2)]
        [InlineData(0, (byte)253, 2, 2)]
        [InlineData(2, (byte)254, 2, 2)]
        [InlineData(2, (byte)255, 2, 2)]
        public void CurrentValue_ShouldEqualExpected(int? expected, byte? memoryAddressValue, byte flag, int trueValue)
        {
            var memoryAddress = new MemoryAddress();
            var memoryFlag = new MemoryFlag(memoryAddress, flag);
            var sut = new AutoTrackFlagBool(memoryFlag, trueValue);

            memoryAddress.Value = memoryAddressValue;

            Assert.Equal(expected, sut.CurrentValue);
        }

        [Fact]
        public void FlagChanged_ShouldRaisePropertyChanged()
        {
            var memoryAddress = new MemoryAddress();
            var memoryFlag = new MemoryFlag(memoryAddress, 1);
            var sut = new AutoTrackFlagBool(memoryFlag, 1);
            
            Assert.PropertyChanged(sut, nameof(IAutoTrackValue.CurrentValue),
                () => memoryAddress.Value = 1);
        }
    }
}