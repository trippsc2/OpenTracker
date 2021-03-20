using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode
{
    public class ChangeBossShuffleTests
    {
        private readonly IMode _mode = new OpenTracker.Models.Modes.Mode();

        [Fact]
        public void CanExecute_ShouldReturnTrueAlways()
        {
            var sut = new ChangeBossShuffle(_mode, false);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteDo_ShouldSetBossShuffleToNewValue(bool expected, bool newValue)
        {
            var sut = new ChangeBossShuffle(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.BossShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteUndo_ShouldSetBossShuffleToPreviousValue(bool expected, bool previousValue)
        {
            _mode.BossShuffle = previousValue;
            var sut = new ChangeBossShuffle(_mode, !previousValue);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.BossShuffle);
        }
    }
}