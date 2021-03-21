using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Items
{
    public class CycleItemTests
    {
        private readonly IItem _item = Substitute.For<IItem>();
        private readonly CycleItem _sut;

        public CycleItemTests()
        {
            _sut = new CycleItem(_item);
        }

        [Fact]
        public void CanExecute_ReturnsTrue()
        {
            Assert.True(_sut.CanExecute());
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
            var factory = scope.Resolve<ICycleItem.Factory>();
            var sut = factory(_item);
            
            Assert.NotNull(sut as CycleItem);
        }
    }
}