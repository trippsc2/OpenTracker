using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Items
{
    public class AddCrystalRequirementTests
    {
        private readonly ICrystalRequirementItem _item = Substitute.For<ICrystalRequirementItem>();

        private readonly AddCrystalRequirement _sut;

        public AddCrystalRequirementTests()
        {
            _sut = new AddCrystalRequirement(_item);
        }

        [Fact]
        public void CanExecute_ShouldReturnTrue_WhenCanAddReturnsTrue()
        {
            _item.CanAdd().Returns(true);
            
            Assert.True(_sut.CanExecute());
        }

        [Fact]
        public void CanExecute_ShouldReturnFalse_WhenCanAddReturnsFalse()
        {
            _item.CanAdd().Returns(false);
            
            Assert.False(_sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldSetKnownToTrue_WhenKnownIsFalse()
        {
            _item.Known.Returns(false);
            _sut.ExecuteDo();
            
            Assert.True(_item.Known);
        }

        [Fact]
        public void ExecuteDo_ShouldCallAdd_WhenKnownIsTrue()
        {
            _item.Known.Returns(true);
            _sut.ExecuteDo();
            
            _item.Received().Add();
        }

        [Fact]
        public void ExecuteUndo_ShouldSetKnownToFalse_WhenCanRemoveReturnsFalse()
        {
            _item.CanRemove().Returns(false);
            _item.Known.Returns(true);
            _sut.ExecuteUndo();
            
            Assert.False(_item.Known);
        }

        [Fact]
        public void ExecuteUndo_ShouldCallRemove_WhenCanRemoveReturnsTrue()
        {
            _item.CanRemove().Returns(true);
            _sut.ExecuteUndo();
            
            _item.Received().Remove();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IAddCrystalRequirement.Factory>();
            var sut = factory(_item);
            
            Assert.NotNull(sut as AddCrystalRequirement);
        }
    }
}