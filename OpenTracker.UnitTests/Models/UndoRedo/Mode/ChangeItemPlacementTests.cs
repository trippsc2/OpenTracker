using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode
{
    public class ChangeItemPlacementTests
    {
        private readonly IMode _mode = new OpenTracker.Models.Modes.Mode();

        [Fact]
        public void CanExecute_ShouldAlwaysReturnTrue()
        {
            var sut = new ChangeItemPlacement(_mode, ItemPlacement.Basic);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(ItemPlacement.Advanced, ItemPlacement.Advanced)]
        [InlineData(ItemPlacement.Basic, ItemPlacement.Basic)]
        public void ExecuteDo_ShouldSetItemPlacementToNewValue(ItemPlacement expected, ItemPlacement newValue)
        {
            var sut = new ChangeItemPlacement(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.ItemPlacement);
        }

        [Theory]
        [InlineData(ItemPlacement.Advanced, ItemPlacement.Advanced)]
        [InlineData(ItemPlacement.Basic, ItemPlacement.Basic)]
        public void ExecuteUndo_ShouldSetItemPlacementToPreviousValue(
            ItemPlacement expected, ItemPlacement previousValue)
        {
            _mode.ItemPlacement = previousValue;
            var sut = new ChangeItemPlacement(_mode, ItemPlacement.Basic);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.ItemPlacement);
        }
    }
}