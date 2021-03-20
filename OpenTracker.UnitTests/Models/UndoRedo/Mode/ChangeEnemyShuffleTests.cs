using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode
{
    public class ChangeEnemyShuffleTests
    {
        private readonly IMode _mode = new OpenTracker.Models.Modes.Mode();

        [Fact]
        public void CanExecute_ShouldReturnTrueAlways()
        {
            var sut = new ChangeEnemyShuffle(_mode, false);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteDo_ShouldSetEnemyShuffleToNewValue(bool expected, bool newValue)
        {
            var sut = new ChangeEnemyShuffle(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.EnemyShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteUndo_ShouldSetEnemyShuffleToPreviousValue(bool expected, bool previousValue)
        {
            _mode.EnemyShuffle = previousValue;
            var sut = new ChangeEnemyShuffle(_mode, !previousValue);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.EnemyShuffle);
        }
    }
}