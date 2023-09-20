using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode;

[ExcludeFromCodeCoverage]
public sealed class ChangeSmallKeyShuffleTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void CanExecute_ShouldReturnTrueAlways()
    {
        var sut = new ChangeSmallKeyShuffle(_mode, false);
            
        Assert.True(sut.CanExecute());
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteDo_ShouldSetSmallKeyShuffleToNewValue(bool expected, bool newValue)
    {
        var sut = new ChangeSmallKeyShuffle(_mode, newValue);
        sut.ExecuteDo();
            
        Assert.Equal(expected, _mode.SmallKeyShuffle);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteUndo_ShouldSetSmallKeyShuffleToPreviousValue(bool expected, bool previousValue)
    {
        _mode.SmallKeyShuffle.Returns(previousValue);
        var sut = new ChangeSmallKeyShuffle(_mode, !previousValue);
        sut.ExecuteDo();
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _mode.SmallKeyShuffle);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IChangeSmallKeyShuffle.Factory>();
        var sut = factory(false);
            
        Assert.NotNull(sut as ChangeSmallKeyShuffle);
    }
}