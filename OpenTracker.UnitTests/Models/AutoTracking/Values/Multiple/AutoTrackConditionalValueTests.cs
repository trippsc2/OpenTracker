using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Multiple;
using OpenTracker.UnitTests.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Multiple
{
    public class AutoTrackConditionalValueTests
    {
        private readonly MockAutoTrackValue _trueValue = new();
        private readonly MockAutoTrackValue _falseValue = new();
        private readonly MockRequirement _condition = new();
        
        private readonly AutoTrackConditionalValue _sut;

        public AutoTrackConditionalValueTests()
        {
            _sut = new AutoTrackConditionalValue(_condition, _trueValue, _falseValue);
        }

        [Theory]
        [InlineData(null, null, null, false)]
        [InlineData(null, 0, null, false)]
        [InlineData(null, 1, null, false)]
        [InlineData(0, null, 0, false)]
        [InlineData(0, 0, 0, false)]
        [InlineData(0, 1, 0, false)]
        [InlineData(1, null, 1, false)]
        [InlineData(1, 0, 1, false)]
        [InlineData(1, 1, 1, false)]
        [InlineData(null, null, null, true)]
        [InlineData(0, 0, null, true)]
        [InlineData(1, 1, null, true)]
        [InlineData(null, null, 0, true)]
        [InlineData(0, 0, 0, true)]
        [InlineData(1, 1, 0, true)]
        [InlineData(null, null, 1, true)]
        [InlineData(0, 0, 1, true)]
        [InlineData(1, 1, 1, true)]
        public void CurrentValue_ShouldEqualExpected(int? expected, int? trueValue, int? falseValue, bool conditionMet)
        {
            _trueValue.CurrentValue = trueValue;
            _falseValue.CurrentValue = falseValue;
            _condition.Met = conditionMet;

            Assert.Equal(expected, _sut.CurrentValue);
        }

        [Fact]
        public void ConditionChanged_ShouldRaisePropertyChanged()
        {
            _trueValue.CurrentValue = 12;
            _falseValue.CurrentValue = 11;
            _condition.Met = false;
            
            Assert.PropertyChanged(_sut, nameof(IAutoTrackValue.CurrentValue), () => _condition.Met = true);
        }

        [Fact]
        public void ValueChanged_ShouldRaisePropertyChanged()
        {
            _condition.Met = false;
            
            Assert.PropertyChanged(
                _sut,
                nameof(IAutoTrackValue.CurrentValue),
                () => _falseValue.CurrentValue = 11);

            _condition.Met = true;
            
            Assert.PropertyChanged(
                _sut,
                nameof(IAutoTrackValue.CurrentValue),
                () => _trueValue.CurrentValue = 12);
        }
    }
}