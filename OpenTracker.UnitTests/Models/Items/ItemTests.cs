using System;
using System.ComponentModel;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class ItemTests
    {
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();
        private readonly IUndoRedoManager _undoRedoManager = Substitute.For<IUndoRedoManager>();

        private readonly IAddItem.Factory _addItemFactory = item => Substitute.For<IAddItem>();
        private readonly IRemoveItem.Factory _removeItemFactory = item => Substitute.For<IRemoveItem>(); 
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_CurrentShouldMatchStarting(int expected, int starting)
        {
            var sut = new Item(_saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, starting,
                null);

            Assert.Equal(expected, sut.Current);
        }

        [Theory]
        [InlineData(true, 0)]
        [InlineData(true, 1)]
        [InlineData(true, 2)]
        public void CanAdd_ShouldAlwaysReturnTrue(bool expected, int starting)
        {
            var sut = new Item(_saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, starting,
                null);

            Assert.Equal(expected, sut.CanAdd());
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Add_ShouldAlwaysAddOneToCurrent(int expected, int starting)
        {
            var sut = new Item(_saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, starting,
                null);
            sut.Add();

            Assert.Equal(expected, sut.Current);
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1)]
        [InlineData(true, 2)]
        public void CanRemove_ShouldReturnTrueIfCurrentGreaterThanZero(bool expected, int starting)
        {
            var sut = new Item(_saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, starting,
                null);

            Assert.Equal(expected, sut.CanRemove());
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public void Remove_ShouldAlwaysSubtractOneToCurrent(int expected, int starting)
        {
            var sut = new Item(_saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, starting,
                null);
            sut.Remove();

            Assert.Equal(expected, sut.Current);
        }

        [Fact]
        public void Remove_ShouldThrowExceptionIfCurrentEqualsZero()
        {
            var sut = new Item(_saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, 0,
                null);
            Assert.Throws<Exception>(() => { sut.Remove(); });
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Reset_ShouldSetCurrentToTheStartingValue(int expected, int starting)
        {
            var sut = new Item(_saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, starting, null);
            sut.Add();
            sut.Reset();

            Assert.Equal(expected, sut.Current);
        }

        [Fact]
        public void Current_ShouldRaisePropertyChanged()
        {
            var sut = new Item(_saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, 0,
                null);

            Assert.PropertyChanged(sut, nameof(IItem.Current), () => { sut.Add(); });
        }

        [Theory]
        [InlineData(3, null)]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Current_ShouldBeEqualToAutoTrackValueAndUnchangedByNullAutoTrackValue(
            int expected, int? autoTrackValue)
        {
            var autoTrack = Substitute.For<IAutoTrackValue>();
            var sut = new Item(_saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, 3,
                autoTrack);

            autoTrack.CurrentValue.Returns(autoTrackValue);
            autoTrack.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                autoTrack, new PropertyChangedEventArgs(nameof(IAutoTrackValue.CurrentValue)));
            
            Assert.Equal(expected, sut.Current);
        }
    }
}
