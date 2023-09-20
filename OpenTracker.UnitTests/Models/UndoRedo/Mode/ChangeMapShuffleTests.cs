using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode;

[ExcludeFromCodeCoverage]
public sealed class ChangeMapShuffleTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void CanExecute_ShouldReturnTrueAlways()
    {
        var sut = new ChangeMapShuffle(_mode, false);
            
        Assert.True(sut.CanExecute());
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteDo_ShouldSetMapShuffleToNewValue(bool expected, bool newValue)
    {
        var sut = new ChangeMapShuffle(_mode, newValue);
        sut.ExecuteDo();
            
        Assert.Equal(expected, _mode.MapShuffle);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteUndo_ShouldSetMapShuffleToPreviousValue(bool expected, bool previousValue)
    {
        _mode.MapShuffle.Returns(previousValue);
        var sut = new ChangeMapShuffle(_mode, !previousValue);
        sut.ExecuteDo();
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _mode.MapShuffle);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IChangeMapShuffle.Factory>();
        var sut = factory(false);
            
        Assert.NotNull(sut as ChangeMapShuffle);
    }
}