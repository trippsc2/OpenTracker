using Autofac;
using NSubstitute;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo.Locations;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Locations;

public class PinLocationTests
{
    private readonly IPinnedLocationCollection _pinnedLocations = Substitute.For<IPinnedLocationCollection>();
    private readonly ILocation _location = Substitute.For<ILocation>();
    private readonly PinLocation _sut;

    public PinLocationTests()
    {
        _sut = new PinLocation(_pinnedLocations, _location);
    }

    [Theory]
    [InlineData(true, false, 0)]
    [InlineData(false, true, 0)]
    [InlineData(true, true, 1)]
    public void CanExecute_ShouldReturnTrue_WhenIndexOfLocationIsNot0(
        bool expected, bool contains, int indexOf)
    {
        _pinnedLocations.Contains(_location).Returns(contains);
        _pinnedLocations.IndexOf(_location).Returns(indexOf);
            
        Assert.Equal(expected, _sut.CanExecute());
    }

    [Fact]
    public void ExecuteDo_ShouldCallInsert()
    {
        _pinnedLocations.Contains(_location).Returns(false);
        _sut.ExecuteDo();
            
        _pinnedLocations.Received().Insert(0, _location);
    }

    [Fact]
    public void ExecuteDo_ShouldCallRemove_WhenContainsReturnsTrue()
    {
        _pinnedLocations.Contains(_location).Returns(true);
        _pinnedLocations.IndexOf(_location).Returns(1);
        _sut.ExecuteDo();
            
        _pinnedLocations.Received().Remove(_location);
    }

    [Fact]
    public void ExecuteUndo_ShouldCallRemove()
    {
        _pinnedLocations.Contains(_location).Returns(false);
        _pinnedLocations.IndexOf(_location).Returns(1);
        _sut.ExecuteDo();
        _sut.ExecuteUndo();
            
        _pinnedLocations.Received().Remove(_location);
    }

    [Fact]
    public void ExecuteUndo_ShouldCallInsert_WhenContainsReturnsTrue()
    {
        _pinnedLocations.Contains(_location).Returns(true);
        _pinnedLocations.IndexOf(_location).Returns(1);
        _sut.ExecuteDo();
        _sut.ExecuteUndo();
            
        _pinnedLocations.Received().Insert(1, _location);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IPinLocation.Factory>();
        var sut = factory(_location);
            
        Assert.NotNull(sut as PinLocation);
    }
}