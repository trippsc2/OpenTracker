using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo
{
    public class ChangeMapShuffleTests
    {
        private readonly IMode _mode = new Mode();

        [Fact]
        public void CanExecute_ShouldReturnTrueAlways()
        {
            var sut = new ChangeMapShuffle(_mode, false);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteDo_ShouldSetMapShuffleToNewValue(bool expected, bool newValue)
        {
            var sut = new ChangeMapShuffle(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.MapShuffle);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteUndo_ShouldSetMapShuffleToPreviousValue(bool expected, bool previousValue)
        {
            _mode.MapShuffle = previousValue;
            var sut = new ChangeMapShuffle(_mode, !previousValue);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.MapShuffle);
        }
    }
}