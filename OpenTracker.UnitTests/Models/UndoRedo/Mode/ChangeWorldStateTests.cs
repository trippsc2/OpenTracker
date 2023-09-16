using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode;

public class ChangeWorldStateTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void CanExecute_ShouldAlwaysReturnTrue()
    {
        var sut = new ChangeWorldState(_mode, WorldState.StandardOpen);
            
        Assert.True(sut.CanExecute());
    }

    [Theory]
    [InlineData(WorldState.StandardOpen, WorldState.StandardOpen)]
    [InlineData(WorldState.Inverted, WorldState.Inverted)]
    public void ExecuteDo_ShouldSetWorldStateToNewValue(WorldState expected, WorldState newValue)
    {
        var sut = new ChangeWorldState(_mode, newValue);
        sut.ExecuteDo();
            
        Assert.Equal(expected, _mode.WorldState);
    }

    [Theory]
    [InlineData(WorldState.StandardOpen, WorldState.StandardOpen)]
    [InlineData(WorldState.Inverted, WorldState.Inverted)]
    public void ExecuteUndo_ShouldSetWorldStateToPreviousValue(
        WorldState expected, WorldState previousValue)
    {
        _mode.WorldState.Returns(previousValue);
        var sut = new ChangeWorldState(_mode, WorldState.StandardOpen);
        sut.ExecuteDo();
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _mode.WorldState);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IChangeWorldState.Factory>();
        var sut = factory(WorldState.StandardOpen);
            
        Assert.NotNull(sut as ChangeWorldState);
    }
}