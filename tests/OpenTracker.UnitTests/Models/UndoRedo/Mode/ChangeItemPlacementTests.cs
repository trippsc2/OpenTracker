using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode
{
    public class ChangeItemPlacementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

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
            _mode.ItemPlacement.Returns(previousValue);
            var sut = new ChangeItemPlacement(_mode, ItemPlacement.Basic);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.ItemPlacement);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IChangeItemPlacement.Factory>();
            var sut = factory(ItemPlacement.Advanced);
            
            Assert.NotNull(sut as ChangeItemPlacement);
        }
    }
}