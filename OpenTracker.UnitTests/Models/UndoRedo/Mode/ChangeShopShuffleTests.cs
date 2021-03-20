using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode
{
    public class ChangeShopShuffleTests
    {
        private readonly IMode _mode = new OpenTracker.Models.Modes.Mode();

        [Fact]
        public void CanExecute_ShouldReturnTrueAlways()
        {
            var sut = new ChangeShopShuffle(_mode, false);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteDo_ShouldSetShopShuffleToNewValue(bool expected, bool newValue)
        {
            var sut = new ChangeShopShuffle(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.ShopShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteUndo_ShouldSetShopShuffleToPreviousValue(bool expected, bool previousValue)
        {
            _mode.ShopShuffle = previousValue;
            var sut = new ChangeShopShuffle(_mode, !previousValue);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.ShopShuffle);
        }
    }
}