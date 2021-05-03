using System;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo.Locations;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Locations
{
    public class UnpinLocationTests
    {
        private readonly IPinnedLocationCollection _pinnedLocations = Substitute.For<IPinnedLocationCollection>();
        private readonly ILocation _location = Substitute.For<ILocation>();
        private readonly UnpinLocation _sut;

        public UnpinLocationTests()
        {
            _sut = new UnpinLocation(_pinnedLocations, _location);
        }

        [Fact]
        public void CanExecute_ShouldReturnTrue()
        {
            Assert.True(_sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldCallRemove()
        {
            _sut.ExecuteDo();
            
            _pinnedLocations.Received().Remove(_location);
        }

        [Fact]
        public void ExecuteUndo_ShouldThrowNullReferenceException_WhenExecuteDoHasNotBeenRun()
        {
            Assert.Throws<NullReferenceException>(() => _sut.ExecuteUndo());
        }

        [Fact]
        public void ExecuteUndo_ShouldCallInsert_WhenContainsReturnsTrue()
        {
            _pinnedLocations.IndexOf(_location).Returns(1);
            _sut.ExecuteDo();
            _sut.ExecuteUndo();
            
            _pinnedLocations.Received().Insert(1, _location);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IUnpinLocation.Factory>();
            var sut = factory(_location);
            
            Assert.NotNull(sut as UnpinLocation);
        }
    }
}