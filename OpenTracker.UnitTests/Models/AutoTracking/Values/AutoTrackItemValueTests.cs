using System.ComponentModel;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values
{
    public class AutoTrackItemValueTests
    {
        private readonly IItem _item;
        private readonly AutoTrackItemValue _sut;

        public AutoTrackItemValueTests()
        {
            _item = Substitute.For<IItem>();
            _sut = new AutoTrackItemValue(_item);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void ItemChanged_CurrentValueShouldEqualExpected(int? expected, int itemCurrent)
        {
            _item.Current.Returns(itemCurrent);
            
            Assert.Equal(expected, _sut.CurrentValue);
        }

        [Fact]
        public void ItemChanged_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IAutoTrackValue.CurrentValue),
                () => _item.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _item, new PropertyChangedEventArgs(nameof(IItem.Current))));
        }
    }
}