using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts;

[ExcludeFromCodeCoverage]
public sealed class BigKeyLayoutTests
{
    private readonly IList<DungeonItemID> _bigKeyLocations = new List<DungeonItemID>();
    private readonly IList<IKeyLayout> _children = new List<IKeyLayout>();
    private readonly IRequirement _requirement = Substitute.For<IRequirement>();

    private readonly BigKeyLayout _sut;

    public BigKeyLayoutTests()
    {
        _sut = new BigKeyLayout(_bigKeyLocations, _children, _requirement);
    }

    [Fact]
    public void CanBeTrue_ShouldReturnFalse_WhenRequirementMetReturnsFalse()
    {
        var state = Substitute.For<IDungeonState>();
        _requirement.Met.Returns(false);
            
        Assert.False(_sut.CanBeTrue(new List<DungeonItemID>(), new List<DungeonItemID>(), state));
    }

    [Theory]
    [InlineData(true, false, false, false)]
    [InlineData(true, false, false, true)]
    [InlineData(true, false, true, false)]
    [InlineData(false, false, true, true)]
    [InlineData(false, true, false, false)]
    [InlineData(true, true, false, true)]
    [InlineData(true, true, true, false)]
    [InlineData(true, true, true, true)]
    public void CanBeTrue_ShouldReturnExpected(
        bool expected, bool bigKeyCollected, bool item1Accessible, bool item2Accessible)
    {
        _requirement.Met.Returns(true);
        _children.Add(Substitute.For<IKeyLayout>());
        _children[0].CanBeTrue(
            Arg.Any<IList<DungeonItemID>>(), Arg.Any<IList<DungeonItemID>>(),
            Arg.Any<IDungeonState>()).Returns(true);

        var state = Substitute.For<IDungeonState>();
        state.BigKeyCollected.Returns(bigKeyCollected);

        const DungeonItemID id1 = DungeonItemID.HCSanctuary;
        const DungeonItemID id2 = DungeonItemID.HCMapChest;
            
        _bigKeyLocations.Add(id1);
        _bigKeyLocations.Add(id2);

        var inaccessible = new List<DungeonItemID>();
        var accessible = new List<DungeonItemID>();

        if (item1Accessible)
        {
            accessible.Add(id1);
        }
        else
        {
            inaccessible.Add(id1);
        }

        if (item2Accessible)
        {
            accessible.Add(id2);
        }
        else
        {
            inaccessible.Add(id2);
        }
            
        Assert.Equal(expected, _sut.CanBeTrue(inaccessible, accessible, state));
    }

    [Fact]
    public void CanBeTrue_ShouldReturnFalse_WhenNoChildReturnsTrue()
    {
        _requirement.Met.Returns(true);

        var state = Substitute.For<IDungeonState>();

        const DungeonItemID id = DungeonItemID.HCSanctuary;
            
        _bigKeyLocations.Add(id);

        var dungeonData = Substitute.For<IMutableDungeon>();
        dungeonData.DungeonItems[id].Accessibility.Returns(AccessibilityLevel.None);

        _children.Add(Substitute.For<IKeyLayout>());
        _children[0].CanBeTrue(
            Arg.Any<IList<DungeonItemID>>(), Arg.Any<IList<DungeonItemID>>(),
            Arg.Any<IDungeonState>()).Returns(false);

        Assert.False(_sut.CanBeTrue(
            new List<DungeonItemID> {id}, new List<DungeonItemID>(), state));
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IBigKeyLayout.Factory>();
        var sut = factory(_bigKeyLocations, _children, _requirement);
            
        Assert.NotNull(sut as BigKeyLayout);
    }
}