using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo
{
    public class ChangeKeyDropShuffleTests
    {
        private readonly IMode _mode = new Mode();

        [Fact]
        public void CanExecute_ShouldReturnTrueAlways()
        {
            var sut = new ChangeKeyDropShuffle(_mode, false);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteDo_ShouldSetKeyDropShuffleToNewValue(bool expected, bool newValue)
        {
            var sut = new ChangeKeyDropShuffle(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.KeyDropShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteUndo_ShouldSetKeyDropShuffleToPreviousValue(bool expected, bool previousValue)
        {
            _mode.KeyDropShuffle = previousValue;
            var sut = new ChangeKeyDropShuffle(_mode, !previousValue);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.KeyDropShuffle);
        }
    }
}