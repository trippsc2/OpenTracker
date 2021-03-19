using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo
{
    public class AddNoteTests
    {
        private readonly ILocationNoteCollection _notes;
        private readonly AddNote _sut;

        public AddNoteTests()
        {
            _notes = Substitute.For<ILocationNoteCollection>();
            var location = Substitute.For<ILocation>();
            location.Notes.Returns(_notes);

            _sut = new AddNote(() => new Marking(), location);
        }

        [Theory]
        [InlineData(true, 0)]
        [InlineData(true, 1)]
        [InlineData(true, 2)]
        [InlineData(true, 3)]
        [InlineData(false, 4)]
        public void CanExecute_ReturnsTrue_WhenNotesCountIsLessThanFour(bool expected, int count)
        {
            _notes.Count.Returns(count);
            
            Assert.Equal(expected, _sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldCallAdd()
        {
            _sut.ExecuteDo();
            
            _notes.Received().Add(Arg.Any<IMarking>());
        }

        [Fact]
        public void ExecuteUndo_ShouldCallRemove()
        {
            _sut.ExecuteUndo();
            
            _notes.Received().Remove(Arg.Any<IMarking>());
        }
    }
}