using OpenTracker.Models.AutoTracking.Values;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values
{
    public class AutoTrackStaticValueTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void CurrentValue_ShouldEqualExpected(int? expected, int value)
        {
            var sut = new AutoTrackStaticValue(value);
            
            Assert.Equal(expected, sut.CurrentValue);
        }
    }
}