using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo.Locations;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Locations
{
    public class ClearLocationTests
    {
        private readonly List<ISection> _sections = new List<ISection>
        {
            Substitute.For<IMarkableSection>(),
            Substitute.For<ISection>(),
            Substitute.For<ISection>()
        };
        private readonly ILocation _location = Substitute.For<ILocation>();

        public ClearLocationTests()
        {
            _location.Sections.Returns(_sections);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void CanExecute_ShouldCallCanBeCleared(bool force)
        {
            var sut = new ClearLocation(_location, force);
            _ = sut.CanExecute();

            _location.Received().CanBeCleared(force);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void CanExecute_ShouldReturnTrue_WhenCanBeClearedReturnsTrue(bool expected, bool canBeCleared)
        {
            var sut = new ClearLocation(_location);
            _location.CanBeCleared(Arg.Any<bool>()).Returns(canBeCleared);
            
            Assert.Equal(expected, sut.CanExecute());
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ExecuteDo_ShouldCallSectionClear_WhenSectionCanBeClearedReturnsTrue(bool force)
        {
            var sut = new ClearLocation(_location, force);
            _sections[0].CanBeCleared(Arg.Any<bool>()).Returns(true);
            _sections[1].CanBeCleared(Arg.Any<bool>()).Returns(true);
            _sections[2].CanBeCleared(Arg.Any<bool>()).Returns(false);
            
            sut.ExecuteDo();
            
            _sections[0].Received().Clear(force);
            _sections[1].Received().Clear(force);
            _sections[2].DidNotReceive().Clear(force);
        }

        [Fact]
        public void ExecuteDo_ShouldSetUserManipulatedToTrue_WhenSectionCanBeClearedReturnsTrue()
        {
            var sut = new ClearLocation(_location);
            _sections[0].CanBeCleared(Arg.Any<bool>()).Returns(true);
            _sections[1].CanBeCleared(Arg.Any<bool>()).Returns(true);
            _sections[2].CanBeCleared(Arg.Any<bool>()).Returns(false);
            
            sut.ExecuteDo();
            
            Assert.True(_sections[0].UserManipulated);
            Assert.True(_sections[1].UserManipulated);
            Assert.False(_sections[2].UserManipulated);
        }

        [Fact]
        public void ExecuteUndo_ShouldRestorePreviousMarking()
        {
            var sut = new ClearLocation(_location);
            var markableSection = (IMarkableSection)_sections[0];
            markableSection.CanBeCleared(Arg.Any<bool>()).Returns(true);
            var marking = Substitute.For<IMarking>();
            markableSection.Marking.Returns(marking);
            marking.Mark.Returns(MarkType.HCLeft);
            sut.ExecuteDo();
            marking.Mark.Returns(MarkType.Unknown);
            sut.ExecuteUndo();
            
            Assert.Equal(MarkType.HCLeft, marking.Mark);
        }

        [Fact]
        public void ExecuteUndo_ShouldRestorePreviousAvailable()
        {
            var sut = new ClearLocation(_location);
            _sections[0].CanBeCleared(Arg.Any<bool>()).Returns(true);
            _sections[0].Available.Returns(1);
            _sections[1].CanBeCleared(Arg.Any<bool>()).Returns(true);
            _sections[1].Available.Returns(2);
            _sections[2].CanBeCleared(Arg.Any<bool>()).Returns(false);
            _sections[2].Available.Returns(3);
            
            sut.ExecuteDo();

            _sections[0].Available.Returns(0);
            _sections[1].Available.Returns(0);
            _sections[2].Available.Returns(0);
            
            sut.ExecuteUndo();
            
            Assert.Equal(1, _sections[0].Available);
            Assert.Equal(2, _sections[1].Available);
            Assert.Equal(0, _sections[2].Available);
        }

        [Fact]
        public void ExecuteUndo_ShouldRestorePreviousUserManipulated()
        {
            var sut = new ClearLocation(_location);
            _sections[0].CanBeCleared(Arg.Any<bool>()).Returns(true);
            _sections[1].CanBeCleared(Arg.Any<bool>()).Returns(true);
            _sections[1].UserManipulated.Returns(true);
            _sections[2].CanBeCleared(Arg.Any<bool>()).Returns(false);

            sut.ExecuteDo();

            _sections[2].UserManipulated.Returns(true);

            sut.ExecuteUndo();
            
            Assert.False(_sections[0].UserManipulated);
            Assert.True(_sections[1].UserManipulated);
            Assert.True(_sections[2].UserManipulated);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<ClearLocation.Factory>();
            var sut = factory(_location);
            
            Assert.NotNull(sut);
        }
    }
}