using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Multiple;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Multiple
{
    public class AutoTrackMultipleDifferenceTests
    {
        private readonly MockAutoTrackValue _value1 = new();
        private readonly MockAutoTrackValue _value2 = new();
        
        private readonly AutoTrackMultipleDifference _sut;

        public AutoTrackMultipleDifferenceTests()
        {
            _sut = new AutoTrackMultipleDifference(_value1, _value2);
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData(0, 0, null)]
        [InlineData(1, 1, null)]
        [InlineData(2, 2, null)]
        [InlineData(null, null, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 2, 0)]
        [InlineData(null, null, 1)]
        [InlineData(0, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 2, 1)]
        [InlineData(null, null, 2)]
        [InlineData(0, 0, 2)]
        [InlineData(0, 1, 2)]
        [InlineData(0, 2, 2)]
        public void CurrentValue_ShouldEqualExpected(int? expected, int? value1, int? value2)
        {
            _value1.CurrentValue = value1;
            _value2.CurrentValue = value2;
            
            Assert.Equal(expected, _sut.CurrentValue);
        }

        [Fact]
        public void ValueChanged_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(
                _sut,
                nameof(IAutoTrackValue.CurrentValue),
                () => _value1.CurrentValue = 1);
            Assert.PropertyChanged(
                _sut,
                nameof(IAutoTrackValue.CurrentValue),
                () => _value2.CurrentValue = 2);
        }
    }
}