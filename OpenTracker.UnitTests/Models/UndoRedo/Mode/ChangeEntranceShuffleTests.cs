using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode;

[ExcludeFromCodeCoverage]
public sealed class ChangeEntranceShuffleTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void CanExecute_ShouldAlwaysReturnTrue()
    {
        var sut = new ChangeEntranceShuffle(_mode, EntranceShuffle.None);
            
        Assert.True(sut.CanExecute());
    }

    [Theory]
    [InlineData(EntranceShuffle.None, EntranceShuffle.None)]
    [InlineData(EntranceShuffle.Dungeon, EntranceShuffle.Dungeon)]
    [InlineData(EntranceShuffle.All, EntranceShuffle.All)]
    [InlineData(EntranceShuffle.Insanity, EntranceShuffle.Insanity)]
    public void ExecuteDo_ShouldSetEntranceShuffleToNewValue(EntranceShuffle expected, EntranceShuffle newValue)
    {
        var sut = new ChangeEntranceShuffle(_mode, newValue);
        sut.ExecuteDo();
            
        Assert.Equal(expected, _mode.EntranceShuffle);
    }

    [Theory]
    [InlineData(EntranceShuffle.None, EntranceShuffle.None)]
    [InlineData(EntranceShuffle.Dungeon, EntranceShuffle.Dungeon)]
    [InlineData(EntranceShuffle.All, EntranceShuffle.All)]
    [InlineData(EntranceShuffle.Insanity, EntranceShuffle.Insanity)]
    public void ExecuteUndo_ShouldSetEntranceShuffleToPreviousValue(
        EntranceShuffle expected, EntranceShuffle previousValue)
    {
        _mode.EntranceShuffle.Returns(previousValue);
        var sut = new ChangeEntranceShuffle(_mode, EntranceShuffle.None);
        sut.ExecuteDo();
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _mode.EntranceShuffle);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IChangeEntranceShuffle.Factory>();
        var sut = factory(EntranceShuffle.None);
            
        Assert.NotNull(sut as ChangeEntranceShuffle);
    }
}