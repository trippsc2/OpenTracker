using NSubstitute;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Sections
{
    public class UncollectSectionTests
    {
        private readonly ISection _section = Substitute.For<ISection>();
        private readonly UncollectSection _sut;

        public UncollectSectionTests()
        {
            _sut = new UncollectSection(_section);
        }

        [Fact]
        public void CanExecute_ShouldCallCanBeUncleared()
        {
            _ = _sut.CanExecute();

            _section.Received().CanBeUncleared();
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void CanExecute_ShouldReturnTrue_WhenCanBeClearedReturnsTrue(bool expected, bool canBeUncleared)
        {
            _section.CanBeUncleared().Returns(canBeUncleared);
            
            Assert.Equal(expected, _sut.CanExecute());
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void ExecuteDo_ShouldSubtractAvailableBy1(int expected, int starting)
        {
            _section.Available.Returns(starting);
            _sut.ExecuteDo();
            
            Assert.Equal(expected, _section.Available);
        }

        [Fact]
        public void ExecuteDo_ShouldSetUserManipulatedToTrue()
        {
            _sut.ExecuteDo();
            
            Assert.True(_section.UserManipulated);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void ExecuteUndo_ShouldRestorePreviousAvailableValue(int expected, int starting)
        {
            _section.Available.Returns(starting);
            
            _sut.ExecuteDo();
            _sut.ExecuteUndo();
            
            Assert.Equal(expected, _section.Available);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteUndo_ShouldRestorePreviousUserManipulated(bool expected, bool starting)
        {
            _section.UserManipulated.Returns(starting);
            
            _sut.ExecuteDo();
            _sut.ExecuteUndo();
            
            Assert.Equal(expected, _section.UserManipulated);
        }
    }
}