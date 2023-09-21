using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode;

[ExcludeFromCodeCoverage]
public sealed class ChangeKeyDropShuffleTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void CanExecute_ShouldReturnTrueAlways()
    {
        var sut = new ChangeKeyDropShuffle(_mode, false);
            
        Assert.True(sut.CanExecute());
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteDo_ShouldSetKeyDropShuffleToNewValue(bool expected, bool newValue)
    {
        var sut = new ChangeKeyDropShuffle(_mode, newValue);
        sut.ExecuteDo();
            
        Assert.Equal(expected, _mode.KeyDropShuffle);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteUndo_ShouldSetKeyDropShuffleToPreviousValue(bool expected, bool previousValue)
    {
        _mode.KeyDropShuffle.Returns(previousValue);
        var sut = new ChangeKeyDropShuffle(_mode, !previousValue);
        sut.ExecuteDo();
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _mode.KeyDropShuffle);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IChangeKeyDropShuffle.Factory>();
        var sut = factory(false);
            
        Assert.NotNull(sut as ChangeKeyDropShuffle);
    }
}