using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo
{
    public class ChangeEntranceShuffleTests
    {
        private readonly IMode _mode = new Mode();

        [Fact]
        public void CanExecute_ShouldAlwaysReturnTrue()
        {
            var sut = new ChangeEntranceShuffle(_mode, EntranceShuffle.None);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(EntranceShuffle.None, EntranceShuffle.None)]
        [InlineData(EntranceShuffle.Dungeon, EntranceShuffle.Dungeon)]
        [InlineData(EntranceShuffle.All, EntranceShuffle.All)]
        [InlineData(EntranceShuffle.Insanity, EntranceShuffle.Insanity)]
        public void ExecuteDo_ShouldSetEntranceShuffleToNewValue(EntranceShuffle expected, EntranceShuffle newValue)
        {
            var sut = new ChangeEntranceShuffle(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.EntranceShuffle);
        }

        [Theory]
        [InlineData(EntranceShuffle.None, EntranceShuffle.None)]
        [InlineData(EntranceShuffle.Dungeon, EntranceShuffle.Dungeon)]
        [InlineData(EntranceShuffle.All, EntranceShuffle.All)]
        [InlineData(EntranceShuffle.Insanity, EntranceShuffle.Insanity)]
        public void ExecuteUndo_ShouldSetEntranceShuffleToPreviousValue(
            EntranceShuffle expected, EntranceShuffle previousValue)
        {
            _mode.EntranceShuffle = previousValue;
            var sut = new ChangeEntranceShuffle(_mode, EntranceShuffle.None);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.EntranceShuffle);
        }
    }
}