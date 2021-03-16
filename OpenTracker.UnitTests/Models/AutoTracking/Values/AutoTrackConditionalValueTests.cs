using System.ComponentModel;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values
{
    public class AutoTrackConditionalValueTests
    {
        private readonly IAutoTrackValue _trueValue;
        private readonly IAutoTrackValue _falseValue;
        private readonly IRequirement _condition;
        private readonly AutoTrackConditionalValue _sut;

        public AutoTrackConditionalValueTests()
        {
            _trueValue = Substitute.For<IAutoTrackValue>();
            _falseValue = Substitute.For<IAutoTrackValue>();
            _condition = Substitute.For<IRequirement>();

            _sut = new AutoTrackConditionalValue(_condition, _trueValue, _falseValue);
        }

        [Theory]
        [InlineData(null, null, false, null)]
        [InlineData(0, null, false, null)]
        [InlineData(1, null, false, null)]
        [InlineData(null, 0, false, 0)]
        [InlineData(0, 0, false, 0)]
        [InlineData(1, 0, false, 0)]
        [InlineData(null, 1, false, 1)]
        [InlineData(0, 1, false, 1)]
        [InlineData(1, 1, false, 1)]
        [InlineData(null, null, true, null)]
        [InlineData(0, null, true, 0)]
        [InlineData(1, null, true, 1)]
        [InlineData(null, 0, true, null)]
        [InlineData(0, 0, true, 0)]
        [InlineData(1, 0, true, 1)]
        [InlineData(null, 1, true, null)]
        [InlineData(0, 1, true, 0)]
        [InlineData(1, 1, true, 1)]
        public void ValueChanged_CurrentValueShouldMatchExpected(
            int? trueValue, int? falseValue, bool conditionMet, int? expected)
        {
            _trueValue.CurrentValue.Returns(trueValue);
            _falseValue.CurrentValue.Returns(falseValue);
            _condition.Met.Returns(conditionMet);

            Assert.Equal(expected, _sut.CurrentValue);
        }

        [Fact]
        public void ValueChanged_ShouldRaisePropertyChanged()
        {
            _trueValue.CurrentValue.Returns(12);
            _falseValue.CurrentValue.Returns(11);
            _condition.Met.Returns(true);
            
            Assert.PropertyChanged(_sut, nameof(IAutoTrackValue.CurrentValue),
                () => _condition.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _condition, new PropertyChangedEventArgs(nameof(IRequirement.Met))));
        }
    }
}