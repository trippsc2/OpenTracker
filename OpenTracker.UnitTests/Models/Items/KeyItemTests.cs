using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.Items
{
    public class KeyItemTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();
        private readonly ISaveLoadManager _saveLoadManager = Substitute.For<ISaveLoadManager>();
        private readonly IUndoRedoManager _undoRedoManager = Substitute.For<IUndoRedoManager>();

        private readonly IAddItem.Factory _addItemFactory = item => Substitute.For<IAddItem>();
        private readonly IRemoveItem.Factory _removeItemFactory = item => Substitute.For<IRemoveItem>();

        private readonly IItem _genericKey = Substitute.For<IItem>();
        
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 2, 0)]
        [InlineData(3, 3, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 1)]
        [InlineData(3, 3, 1)]
        [InlineData(0, 0, 2)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(3, 3, 2)]
        [InlineData(0, 0, 3)]
        [InlineData(1, 1, 3)]
        [InlineData(2, 2, 3)]
        [InlineData(3, 3, 3)]
        public void Maximum_ShouldEqualNonKeyDropMaximumWhenKeyDropShuffleEqualsFalse(
            int expected, int nonKeyDropMaximum, int keyDropMaximum)
        {
            _mode.KeyDropShuffle.Returns(false);
            var sut = new KeyItem(
                _mode, _saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, _genericKey,
                nonKeyDropMaximum, keyDropMaximum, null);
            
            Assert.Equal(expected, sut.Maximum);
        }
        
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 0)]
        [InlineData(0, 2, 0)]
        [InlineData(0, 3, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 1)]
        [InlineData(1, 3, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(2, 1, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(2, 3, 2)]
        [InlineData(3, 0, 3)]
        [InlineData(3, 1, 3)]
        [InlineData(3, 2, 3)]
        [InlineData(3, 3, 3)]
        public void Maximum_ShouldEqualKeyDropMaximumWhenKeyDropShuffleEqualsTrue(
            int expected, int nonKeyDropMaximum, int keyDropMaximum)
        {
            _mode.KeyDropShuffle.Returns(true);
            var sut = new KeyItem(
                _mode, _saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, _genericKey,
                nonKeyDropMaximum, keyDropMaximum, null);
            
            Assert.Equal(expected, sut.Maximum);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 2, 0)]
        [InlineData(3, 3, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 1)]
        [InlineData(3, 3, 1)]
        [InlineData(0, 0, 2)]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(3, 3, 2)]
        [InlineData(0, 0, 3)]
        [InlineData(1, 1, 3)]
        [InlineData(2, 2, 3)]
        [InlineData(3, 3, 3)]
        public void EffectiveCurrent_ShouldEqualCurrentIfGenericKeysEqualsFalse(
            int expected, int current, int genericKeyCurrent)
        {
            _mode.GenericKeys.Returns(false);
            _genericKey.Current.Returns(genericKeyCurrent);
            var sut = new KeyItem(
                _mode, _saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, _genericKey,
                6, 6, null)
            {
                Current = current
            };

            Assert.Equal(expected, sut.EffectiveCurrent);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 2, 0)]
        [InlineData(3, 3, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(2, 1, 1)]
        [InlineData(3, 2, 1)]
        [InlineData(4, 3, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(3, 1, 2)]
        [InlineData(4, 2, 2)]
        [InlineData(5, 3, 2)]
        [InlineData(3, 0, 3)]
        [InlineData(4, 1, 3)]
        [InlineData(5, 2, 3)]
        [InlineData(6, 3, 3)]
        [InlineData(6, 4, 4)]
        public void EffectiveCurrent_ShouldEqualCurrentPlusGenericKeyCurrentIfGenericKeysEqualsTrue(
            int expected, int current, int genericKeyCurrent)
        {
            _mode.GenericKeys.Returns(true);
            _genericKey.Current.Returns(genericKeyCurrent);
            var sut = new KeyItem(
                _mode, _saveLoadManager, _undoRedoManager, _addItemFactory, _removeItemFactory, _genericKey,
                6, 6, null)
            {
                Current = current
            };

            Assert.Equal(expected, sut.EffectiveCurrent);
        }
    }
}