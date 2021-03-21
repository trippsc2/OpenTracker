using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo.Locations;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Locations
{
    public class PinLocationTests
    {
        private readonly IPinnedLocationCollection _pinnedLocationCollection =
            Substitute.For<IPinnedLocationCollection>();
        private readonly ILocation _location = Substitute.For<ILocation>();
        private readonly PinLocation _sut;

        public PinLocationTests()
        {
            _sut = new PinLocation(_pinnedLocationCollection, _location);
        }

        [Theory]
        [InlineData(true, false, 0)]
        [InlineData(false, true, 0)]
        [InlineData(true, true, 1)]
        public void CanExecute_ShouldReturnTrue_WhenIndexOfLocationIsNot0(
            bool expected, bool contains, int indexOf)
        {
            _pinnedLocationCollection.Contains(_location).Returns(contains);
            _pinnedLocationCollection.IndexOf(_location).Returns(indexOf);
            
            Assert.Equal(expected, _sut.CanExecute());
        }
    }
}