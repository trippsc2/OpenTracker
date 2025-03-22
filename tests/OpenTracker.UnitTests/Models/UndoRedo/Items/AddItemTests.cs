using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Items
{
    public class AddItemTests
    {
        private readonly IItem _item = Substitute.For<IItem>();
        private readonly AddItem _sut;

        public AddItemTests()
        {
            _sut = new AddItem(_item);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void CanExecute_ReturnsTrue_WhenCanAddReturnsTrue(bool expected, bool canAdd)
        {
            _item.CanAdd().Returns(canAdd);
            
            Assert.Equal(expected, _sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldCallAdd()
        {
            _sut.ExecuteDo();
            
            _item.Received().Add();
        }

        [Fact]
        public void ExecuteUndo_ShouldCallRemove()
        {
            _sut.ExecuteUndo();
            
            _item.Received().Remove();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IAddItem.Factory>();
            var sut = factory(_item);
            
            Assert.NotNull(sut as AddItem);
        }
    }
}