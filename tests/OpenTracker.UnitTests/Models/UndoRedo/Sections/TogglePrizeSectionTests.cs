using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Sections;

[ExcludeFromCodeCoverage]
public sealed class TogglePrizeSectionTests
{
    private readonly ISection _section = Substitute.For<ISection>();

    [Fact]
    public void CanExecute_ShouldCallCanBeUncleared()
    {
        var sut = new TogglePrizeSection(_section, false);
        _ = sut.CanExecute();

        _section.Received().CanBeUncleared();
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CanExecute_ShouldCallCanBeCleared(bool force)
    {
        var sut = new TogglePrizeSection(_section, force);
        _ = sut.CanExecute();

        _section.Received().CanBeCleared(force);
    }

    [Theory]
    [InlineData(false, false, false)]
    [InlineData(true, true, false)]
    [InlineData(true, false, true)]
    [InlineData(true, true, true)]
    public void CanExecute_ShouldReturnTrue_WhenCanBeClearedReturnsTrue(
        bool expected, bool canBeUncleared, bool canBeCleared)
    {
        var sut = new TogglePrizeSection(_section, false);
        _section.CanBeUncleared().Returns(canBeUncleared);
        _section.CanBeCleared(Arg.Any<bool>()).Returns(canBeCleared);
            
        Assert.Equal(expected, sut.CanExecute());
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    public void ExecuteDo_ToggleAvailableBetween1And0(int expected, int starting)
    {
        var sut = new TogglePrizeSection(_section, false);
        _section.Available.Returns(starting);
        _section.IsAvailable().Returns(starting == 1);
        sut.ExecuteDo();
            
        Assert.Equal(expected, _section.Available);
    }

    [Fact]
    public void ExecuteDo_ShouldSetUserManipulatedToTrue()
    {
        var sut = new TogglePrizeSection(_section, false);
        sut.ExecuteDo();
            
        Assert.True(_section.UserManipulated);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    public void ExecuteUndo_ShouldRestorePreviousAvailableValue(int expected, int starting)
    {
        _section.Available.Returns(starting);
        _section.IsAvailable().Returns(starting == 1);
        var sut = new TogglePrizeSection(_section, false);
            
        sut.ExecuteDo();
            
        _section.IsAvailable().Returns(starting == 0);
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _section.Available);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteUndo_ShouldRestorePreviousUserManipulated(bool expected, bool starting)
    {
        _section.UserManipulated.Returns(starting);
        var sut = new TogglePrizeSection(_section, false);
            
        sut.ExecuteDo();
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _section.UserManipulated);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<ITogglePrizeSection.Factory>();
        var sut = factory(_section, false);
            
        Assert.NotNull(sut as TogglePrizeSection);
    }
}