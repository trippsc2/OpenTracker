using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.UndoRedo.Notes;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Notes
{
    public class RemoveNoteTests
    {
        private readonly IMarking _note = Substitute.For<IMarking>();
        private readonly ILocationNoteCollection _notes;
        private readonly RemoveNote _sut;

        public RemoveNoteTests()
        {
            _notes = Substitute.For<ILocationNoteCollection>();
            var location = Substitute.For<ILocation>();
            location.Notes.Returns(_notes);

            _sut = new RemoveNote(_note, location);
        }

        [Fact]
        public void CanExecute_ReturnsTrue()
        {
            Assert.True(_sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldCallRemove()
        {
            _sut.ExecuteDo();
            
            _notes.Received().Remove(_note);
        }

        [Fact]
        public void ExecuteUndo_ShouldCallInsert()
        {
            _notes.IndexOf(_note).Returns(1);
            _sut.ExecuteDo();
            _sut.ExecuteUndo();

            _notes.Received().Insert(1, _note);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IRemoveNote.Factory>();
            var sut = factory(_note, Substitute.For<ILocation>());
            
            Assert.NotNull(sut as RemoveNote);
        }
    }
}