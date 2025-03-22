using Autofac;
using NSubstitute;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.UndoRedo.Prize;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Prize
{
    public class ChangePrizeTests
    {
        private readonly IPrizePlacement _prizePlacement = Substitute.For<IPrizePlacement>();
        private readonly ChangePrize _sut;

        public ChangePrizeTests()
        {
            _sut = new ChangePrize(_prizePlacement);
        }

        [Fact]
        public void CanExecute_ShouldCallCanCycle()
        {
            _ = _sut.CanExecute();

            _prizePlacement.Received().CanCycle();
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void CanExecute_ShouldReturnTrue_WhenCanCycleReturnsTrue(bool expected, bool canCycle)
        {
            _prizePlacement.CanCycle().Returns(canCycle);
            
            Assert.Equal(expected, _sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldCallCycle()
        {
            _sut.ExecuteDo();
            
            _prizePlacement.Received().Cycle();
        }

        [Fact]
        public void ExecuteUndo_ShouldCallCycle()
        {
            _sut.ExecuteUndo();

            _prizePlacement.Received().Cycle(true);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IChangePrize.Factory>();
            var sut = factory(_prizePlacement);
            
            Assert.NotNull(sut as ChangePrize);
        }
    }
}