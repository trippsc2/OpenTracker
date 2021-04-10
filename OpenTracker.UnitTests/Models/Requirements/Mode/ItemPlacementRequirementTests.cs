using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Mode
{
    public class ItemPlacementRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();

        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            const ItemPlacement itemPlacement = ItemPlacement.Basic;
            var sut = new ItemPlacementRequirement(_mode, itemPlacement);
            _mode.ItemPlacement.Returns(itemPlacement);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.ItemPlacement)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, ItemPlacement.Basic, ItemPlacement.Basic)]
        [InlineData(false, ItemPlacement.Basic, ItemPlacement.Advanced)]
        [InlineData(false, ItemPlacement.Advanced, ItemPlacement.Basic)]
        [InlineData(true, ItemPlacement.Advanced, ItemPlacement.Advanced)]
        public void Met_ShouldReturnExpectedValue(
            bool expected, ItemPlacement itemPlacement, ItemPlacement requirement)
        {
            _mode.ItemPlacement.Returns(itemPlacement);
            var sut = new ItemPlacementRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IItemPlacementRequirement.Factory>();
            var sut = factory(ItemPlacement.Basic);
            
            Assert.NotNull(sut as ItemPlacementRequirement);
        }
    }
}