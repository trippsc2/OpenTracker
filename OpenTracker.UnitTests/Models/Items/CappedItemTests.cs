using System;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class CappedItemTests
    {
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();
        private readonly IUndoRedoManager _undoRedoManager = Substitute.For<IUndoRedoManager>();
        private readonly IAddItem.Factory _addItemFactory = item => Substitute.For<IAddItem>();
        private readonly IRemoveItem.Factory _removeItemFactory = item => Substitute.For<IRemoveItem>(); 
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Ctor_MaximumShouldEqualParameter(int expected, int maximum)
        {
            var sut = new CappedItem(
                _saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, 0, maximum,
                null);
            
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
                    _saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, starting, maximum,
                    null);
            });
        }

        [Theory]
        [InlineData(false, 0, 0)]
        [InlineData(true, 0, 1)]
        [InlineData(false, 1, 1)]
        [InlineData(true, 1, 2)]
        public void CanAdd_ShouldReturnTrueIfCurrentIsLessThanMaximum(bool expected, int starting, int maximum)
        {
            var sut = new CappedItem(
                _saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, starting, maximum,
                null);

            Assert.Equal(expected, sut.CanAdd());
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(2, 1, 2)]
        public void Add_ShouldAddOneToCurrentIfLessThanMaximumOtherwiseSetToZero(
            int expected, int starting, int maximum)
        {
            var sut = new CappedItem(
                _saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, starting, maximum,
                null);
            sut.Add();

            Assert.Equal(expected, sut.Current);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(0, 1, 2)]
        public void Remove_ShouldSubtractOneFromCurrentIfGreaterThanZeroOtherwiseSetToMaximum(
            int expected, int starting, int maximum)
        {
            var sut = new CappedItem(
                _saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, starting, maximum,
                null);
            sut.Remove();

            Assert.Equal(expected, sut.Current);
        }
    }
}
