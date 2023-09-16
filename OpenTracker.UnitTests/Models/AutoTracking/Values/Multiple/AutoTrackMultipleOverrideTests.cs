using System.Collections.Generic;
using System.ComponentModel;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Multiple;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Multiple
{
    public class AutoTrackMultipleOverrideTests
    {
        [Theory]
        [InlineData(null, null, null)]
        [InlineData(0, 0, null)]
        [InlineData(1, 1, null)]
        [InlineData(2, 2, null)]
        [InlineData(0, null, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 2, 0)]
        [InlineData(1, null, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 1)]
        [InlineData(2, null, 2)]
        [InlineData(2, 0, 2)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(null, null, null, null)]
        [InlineData(0, 0, null, null)]
        [InlineData(1, 1, null, null)]
        [InlineData(2, 2, null, null)]
        [InlineData(0, null, 0, null)]
        [InlineData(0, 0, 0, null)]
        [InlineData(1, 1, 0, null)]
        [InlineData(2, 2, 0, null)]
        [InlineData(1, null, 1, null)]
        [InlineData(1, 0, 1, null)]
        [InlineData(1, 1, 1, null)]
        [InlineData(2, 2, 1, null)]
        [InlineData(2, null, 2, null)]
        [InlineData(2, 0, 2, null)]
        [InlineData(1, 1, 2, null)]
        [InlineData(2, 2, 2, null)]
        [InlineData(0, null, null, 0)]
        [InlineData(0, 0, null, 0)]
        [InlineData(1, 1, null, 0)]
        [InlineData(2, 2, null, 0)]
        [InlineData(0, null, 0, 0)]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1, 1, 0, 0)]
        [InlineData(2, 2, 0, 0)]
        [InlineData(1, null, 1, 0)]
        [InlineData(1, 0, 1, 0)]
        [InlineData(1, 1, 1, 0)]
        [InlineData(2, 2, 1, 0)]
        [InlineData(2, null, 2, 0)]
        [InlineData(2, 0, 2, 0)]
        [InlineData(1, 1, 2, 0)]
        [InlineData(2, 2, 2, 0)]
        [InlineData(1, null, null, 1)]
        [InlineData(1, 0, null, 1)]
        [InlineData(1, 1, null, 1)]
        [InlineData(2, 2, null, 1)]
        [InlineData(1, null, 0, 1)]
        [InlineData(1, 0, 0, 1)]
        [InlineData(1, 1, 0, 1)]
        [InlineData(2, 2, 0, 1)]
        [InlineData(1, null, 1, 1)]
        [InlineData(1, 0, 1, 1)]
        [InlineData(1, 1, 1, 1)]
        [InlineData(2, 2, 1, 1)]
        [InlineData(2, null, 2, 1)]
        [InlineData(2, 0, 2, 1)]
        [InlineData(1, 1, 2, 1)]
        [InlineData(2, 2, 2, 1)]
        [InlineData(2, null, null, 2)]
        [InlineData(2, 0, null, 2)]
        [InlineData(1, 1, null, 2)]
        [InlineData(2, 2, null, 2)]
        [InlineData(2, null, 0, 2)]
        [InlineData(2, 0, 0, 2)]
        [InlineData(1, 1, 0, 2)]
        [InlineData(2, 2, 0, 2)]
        [InlineData(1, null, 1, 2)]
        [InlineData(1, 0, 1, 2)]
        [InlineData(1, 1, 1, 2)]
        [InlineData(2, 2, 1, 2)]
        [InlineData(2, null, 2, 2)]
        [InlineData(2, 0, 2, 2)]
        [InlineData(1, 1, 2, 2)]
        [InlineData(2, 2, 2, 2)]
        public void CurrentValue_ShouldEqualExpected(int? expected, params int?[] values)
        {
            var valueList = new List<IAutoTrackValue>();

            foreach (var value in values)
            {
                var substitute = Substitute.For<IAutoTrackValue>();
                substitute.CurrentValue.Returns(value);
                valueList.Add(substitute);
            }

            var sut = new AutoTrackMultipleOverride(valueList);

            Assert.Equal(expected, sut.CurrentValue);
        }

        [Fact]
        public void ValueChanged_ShouldRaisePropertyChanged()
        {
            var values = new List<IAutoTrackValue>
            {
                Substitute.For<IAutoTrackValue>(),
                Substitute.For<IAutoTrackValue>(),
                Substitute.For<IAutoTrackValue>()
            };

            foreach (var value in values)
            {
                value.CurrentValue.Returns(0);
            }

            var sut = new AutoTrackMultipleOverride(values);

            values[0].CurrentValue.Returns(1);
            Assert.PropertyChanged(sut, nameof(IAutoTrackValue.CurrentValue),
                () => values[0].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    values[0], new PropertyChangedEventArgs(nameof(IAutoTrackValue.CurrentValue))));

            values[0].CurrentValue.Returns(0);
            values[1].CurrentValue.Returns(2);
            Assert.PropertyChanged(sut, nameof(IAutoTrackValue.CurrentValue),
                () => values[1].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    values[1], new PropertyChangedEventArgs(nameof(IAutoTrackValue.CurrentValue))));

            values[1].CurrentValue.Returns(0);
            values[2].CurrentValue.Returns(3);
            Assert.PropertyChanged(sut, nameof(IAutoTrackValue.CurrentValue),
                () => values[2].PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    values[2], new PropertyChangedEventArgs(nameof(IAutoTrackValue.CurrentValue))));
        }
    }
}