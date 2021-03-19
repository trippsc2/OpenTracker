using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo
{
    public class AddItemTests
    {
        private readonly IItem _item = Substitute.For<IItem>();
        private readonly AddItem _sut;

        public AddItemTests()
        {
            _sut = new AddItem(_item);
        }

        [Fact]
        public void CanExecute_ReturnsTrue_WhenCanAddReturnsTrue()
        {
            _item.CanAdd().Returns(true);
            
            Assert.True(_sut.CanExecute());
        }

        [Fact]
        public void CanExecute_ReturnsFalse_WhenCanAddReturnsFalse()
        {
            _item.CanAdd().Returns(false);
            
            Assert.False(_sut.CanExecute());
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
    }
}