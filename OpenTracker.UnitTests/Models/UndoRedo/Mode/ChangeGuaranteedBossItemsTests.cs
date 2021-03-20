using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode
{
    public class ChangeGuaranteedBossItemsTests
    {
        private readonly IMode _mode = new OpenTracker.Models.Modes.Mode();

        [Fact]
        public void CanExecute_ShouldReturnTrueAlways()
        {
            var sut = new ChangeGuaranteedBossItems(_mode, false);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteDo_ShouldSetGuaranteedBossItemsToNewValue(bool expected, bool newValue)
        {
            var sut = new ChangeGuaranteedBossItems(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.GuaranteedBossItems);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteUndo_ShouldSetGuaranteedBossItemsToPreviousValue(bool expected, bool previousValue)
        {
            _mode.GuaranteedBossItems = previousValue;
            var sut = new ChangeGuaranteedBossItems(_mode, !previousValue);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.GuaranteedBossItems);
        }
    }
}