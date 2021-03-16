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
            _trueValue.CurrentValue.Returns(trueValue);
            _falseValue.CurrentValue.Returns(falseValue);
            _condition.Met.Returns(conditionMet);

            Assert.Equal(expected, _sut.CurrentValue);
        }

        [Fact]
        public void ConditionChanged_ShouldRaisePropertyChanged()
        {
            _trueValue.CurrentValue.Returns(12);
            _falseValue.CurrentValue.Returns(11);
            _condition.Met.Returns(true);
            
            Assert.PropertyChanged(_sut, nameof(IAutoTrackValue.CurrentValue),
                () => _condition.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _condition, new PropertyChangedEventArgs(nameof(IRequirement.Met))));
        }

        [Fact]
        public void ValueChanged_ShouldRaisePropertyChanged()
        {
            _trueValue.CurrentValue.Returns(12);
            _falseValue.CurrentValue.Returns(11);
            _condition.Met.Returns(true);
            
            Assert.PropertyChanged(_sut, nameof(IAutoTrackValue.CurrentValue),
                () => _trueValue.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _trueValue, new PropertyChangedEventArgs(nameof(IAutoTrackValue.CurrentValue))));
            Assert.PropertyChanged(_sut, nameof(IAutoTrackValue.CurrentValue),
                () => _falseValue.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _falseValue, new PropertyChangedEventArgs(nameof(IAutoTrackValue.CurrentValue))));
        }
    }
}