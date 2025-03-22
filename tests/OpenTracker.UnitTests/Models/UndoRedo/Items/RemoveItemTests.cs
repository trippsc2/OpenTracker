using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Items
{
    public class RemoveItemTests
    {
        private readonly IItem _item = Substitute.For<IItem>();
        private readonly RemoveItem _sut;

        public RemoveItemTests()
        {
            _sut = new RemoveItem(_item);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void CanExecute_ReturnsTrue_WhenCanRemoveReturnsTrue(bool expected, bool canRemove)
        {
            _item.CanRemove().Returns(canRemove);
            
            Assert.Equal(expected, _sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldCallRemove()
        {
            _sut.ExecuteDo();
            
            _item.Received().Remove();
        }

        [Fact]
        public void ExecuteUndo_ShouldCallAdd()
        {
            _sut.ExecuteUndo();
            
            _item.Received().Add();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IRemoveItem.Factory>();
            var sut = factory(_item);
            
            Assert.NotNull(sut as RemoveItem);
        }
    }
}