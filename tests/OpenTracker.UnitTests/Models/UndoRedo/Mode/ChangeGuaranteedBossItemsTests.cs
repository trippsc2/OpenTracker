using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode;

[ExcludeFromCodeCoverage]
public sealed class ChangeGuaranteedBossItemsTests
{
    private readonly IMode _mode = Substitute.For<IMode>();

    [Fact]
    public void CanExecute_ShouldReturnTrueAlways()
    {
        var sut = new ChangeGuaranteedBossItems(_mode, false);
            
        Assert.True(sut.CanExecute());
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteDo_ShouldSetGuaranteedBossItemsToNewValue(bool expected, bool newValue)
    {
        var sut = new ChangeGuaranteedBossItems(_mode, newValue);
        sut.ExecuteDo();
            
        Assert.Equal(expected, _mode.GuaranteedBossItems);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteUndo_ShouldSetGuaranteedBossItemsToPreviousValue(bool expected, bool previousValue)
    {
        _mode.GuaranteedBossItems.Returns(previousValue);
        var sut = new ChangeGuaranteedBossItems(_mode, !previousValue);
        sut.ExecuteDo();
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _mode.GuaranteedBossItems);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IChangeGuaranteedBossItems.Factory>();
        var sut = factory(false);
            
        Assert.NotNull(sut as ChangeGuaranteedBossItems);
    }
}