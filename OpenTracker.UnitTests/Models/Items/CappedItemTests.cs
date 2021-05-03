using System;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class CappedItemTests
    {
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();
        
        private readonly IAddItem.Factory _addItemFactory = _ => Substitute.For<IAddItem>();
        private readonly IRemoveItem.Factory _removeItemFactory = _ => Substitute.For<IRemoveItem>();
        private readonly ICycleItem.Factory _cycleItemFactory = _ => Substitute.For<ICycleItem>();

        private readonly IAutoTrackValue _autoTrackValue = Substitute.For<IAutoTrackValue>();
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_ShouldSetMaximum(int expected, int maximum)
        {
            var sut = new CappedItem(
                _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 0, maximum,
                _autoTrackValue);
            
            Assert.Equal(expected, sut.Maximum);
        }
        
        [Theory]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Ctor_ShouldThrowExceptionIfStartingIsGreaterThanMaximum(int starting, int maximum)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new CappedItem(
                    _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, starting, maximum,
                    _autoTrackValue);
            });
        }

        [Theory]
        [InlineData(false, 0, 0)]
        [InlineData(true, 0, 1)]
        [InlineData(false, 1, 1)]
        [InlineData(true, 1, 2)]
        public void CanAdd_ShouldReturnTrue_WhenCurrentIsLessThanMaximum(bool expected, int starting, int maximum)
        {
            var sut = new CappedItem(
                _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, starting, maximum,
                null);

            Assert.Equal(expected, sut.CanAdd());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void Add_ShouldThrowException_WhenCurrentIsEqualToMaximum(int starting, int maximum)
        {
            var sut = new CappedItem(
                _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, starting, maximum,
                null);

            Assert.Throws<Exception>(() => sut.Add());
        }
        
        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(1, 0, 2)]
        [InlineData(2, 1, 2)]
        [InlineData(1, 0, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(3, 2, 3)]
        public void Add_ShouldAddOneToCurrent_WhenLessThanMaximum(int expected, int starting, int maximum)
        {
            var sut = new CappedItem(
                _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, starting, maximum,
                null);
            sut.Add();

            Assert.Equal(expected, sut.Current);
        }

        [Fact]
        public void CreateCycleItemAction_ShouldReturnNewCycleItemAction()
        {
            var sut = new CappedItem(
                _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, 0, 1,
                _autoTrackValue);
            var cycleItem = sut.CreateCycleItemAction();
            
            Assert.NotNull(cycleItem);
        }

        [Theory]
        [InlineData(0, 0, 0, false)]
        [InlineData(1, 0, 1, false)]
        [InlineData(0, 1, 1, false)]
        [InlineData(1, 0, 2, false)]
        [InlineData(2, 1, 2, false)]
        [InlineData(0, 2, 2, false)]
        [InlineData(1, 0, 3, false)]
        [InlineData(2, 1, 3, false)]
        [InlineData(3, 2, 3, false)]
        [InlineData(0, 3, 3, false)]
        [InlineData(0, 0, 0, true)]
        [InlineData(1, 0, 1, true)]
        [InlineData(0, 1, 1, true)]
        [InlineData(2, 0, 2, true)]
        [InlineData(0, 1, 2, true)]
        [InlineData(1, 2, 2, true)]
        [InlineData(3, 0, 3, true)]
        [InlineData(0, 1, 3, true)]
        [InlineData(1, 2, 3, true)]
        [InlineData(2, 3, 3, true)]
        public void Cycle_ShouldSetCurrent(int expected, int current, int maximum, bool reverse)
        {
            var sut = new CappedItem(
                _saveLoadManager, _addItemFactory, _removeItemFactory, _cycleItemFactory, current, maximum,
                _autoTrackValue);
            sut.Cycle(reverse);
            
            Assert.Equal(expected, sut.Current);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ICappedItem.Factory>();
            var sut = factory(0, 1, _autoTrackValue);
            
            Assert.NotNull(sut as CappedItem);
        }
    }
}
