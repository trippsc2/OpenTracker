using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode;

[ExcludeFromCodeCoverage]
public sealed class ChangeEnemyShuffleTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void CanExecute_ShouldReturnTrueAlways()
    {
        var sut = new ChangeEnemyShuffle(_mode, false);
            
        Assert.True(sut.CanExecute());
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteDo_ShouldSetEnemyShuffleToNewValue(bool expected, bool newValue)
    {
        var sut = new ChangeEnemyShuffle(_mode, newValue);
        sut.ExecuteDo();
            
        Assert.Equal(expected, _mode.EnemyShuffle);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteUndo_ShouldSetEnemyShuffleToPreviousValue(bool expected, bool previousValue)
    {
        _mode.EnemyShuffle.Returns(previousValue);
        var sut = new ChangeEnemyShuffle(_mode, !previousValue);
        sut.ExecuteDo();
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _mode.EnemyShuffle);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IChangeEnemyShuffle.Factory>();
        var sut = factory(false);
            
        Assert.NotNull(sut as ChangeEnemyShuffle);
    }
}