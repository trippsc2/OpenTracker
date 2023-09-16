using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Result;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Result;

public class DungeonResultTests
{
    [Fact]
    public void Ctor_ShouldSetBossAccessibility()
    {
        var bossAccessibility = Substitute.For<IList<AccessibilityLevel>>();
        var sut = new DungeonResult(bossAccessibility, 0, false, false);
            
        Assert.Equal(bossAccessibility, sut.BossAccessibility);
    }
        
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void Ctor_ShouldSetAccessible(int expected, int accessible)
    {
        var sut = new DungeonResult(
            new List<AccessibilityLevel>(), accessible, false, false);
            
        Assert.Equal(expected, sut.Accessible);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Ctor_ShouldSetSequenceBreak(bool expected, bool sequenceBreak)
    {
        var sut = new DungeonResult(
            new List<AccessibilityLevel>(), 0, sequenceBreak, false);
            
        Assert.Equal(expected, sut.SequenceBreak);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Ctor_ShouldSetVisible(bool expected, bool visible)
    {
        var sut = new DungeonResult(
            new List<AccessibilityLevel>(), 0, false, visible);
            
        Assert.Equal(expected, sut.Visible);
    }
        
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void Ctor_ShouldSetMinimumInaccessible(int expected, int minimumInaccessible)
    {
        var sut = new DungeonResult(
            new List<AccessibilityLevel>(), 0, false, false,
            minimumInaccessible);
            
        Assert.Equal(expected, sut.MinimumInaccessible);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IDungeonResult.Factory>();
        var sut = factory(
            new List<AccessibilityLevel>(), 0, false, false);
            
        Assert.NotNull(sut as DungeonResult);
    }
}