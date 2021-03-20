using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Items
{
    public class RemoveCrystalRequirementTests
    {
        private readonly ICrystalRequirementItem _item = Substitute.For<ICrystalRequirementItem>();

        private readonly RemoveCrystalRequirement _sut;

        public RemoveCrystalRequirementTests()
        {
            _sut = new RemoveCrystalRequirement(_item);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void CanExecute_ShouldReturnTrue_WhenKnownEqualsTrue(bool expected, bool known)
        {
            _item.Known.Returns(known);
            
            Assert.Equal(expected, _sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldSetKnownToFalse_WhenCanRemoveReturnsFalse()
        {
            _item.CanRemove().Returns(false);
            _item.Known.Returns(true);
            _sut.ExecuteDo();
            
            Assert.False(_item.Known);
        }

        [Fact]
        public void ExecuteDo_ShouldCallRemove_WhenCanRemoveReturnsTrue()
        {
            _item.CanRemove().Returns(true);
            _item.Known.Returns(true);
            _item.Current.Returns(1);
            _sut.ExecuteDo();
            
            _item.Received().Remove();
        }

        [Fact]
        public void ExecuteUndo_ShouldSetKnownToTrue_WhenKnownEqualsFalse()
        {
            _item.Known.Returns(false);
            _sut.ExecuteUndo();
            
            Assert.True(_item.Known);
        }

        [Fact]
        public void ExecuteUndo_ShouldCallAdd_WhenKnownEqualsTrue()
        {
            _item.Known.Returns(true);
            _sut.ExecuteUndo();
            
            _item.Received().Add();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<RemoveCrystalRequirement.Factory>();
            var sut = factory(_item);
            
            Assert.NotNull(sut);
        }
    }
}