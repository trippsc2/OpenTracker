using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Nodes;

public class DungeonNodeDictionaryTests
{
    private readonly IDungeonNode.Factory _factory = _ => Substitute.For<IDungeonNode>();
        
    private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();

    private readonly DungeonNodeDictionary _sut;

    public DungeonNodeDictionaryTests()
    {
        _sut = new DungeonNodeDictionary(_factory, _dungeonData);
    }

    [Fact]
    public void PopulateNodes_ShouldCreateSpecifiedNodes()
    {
        var nodes = new List<DungeonNodeID>
        {
            DungeonNodeID.HCSanctuary
        };
            
        _sut.PopulateNodes(nodes);

        Assert.Contains(DungeonNodeID.HCSanctuary, _sut.Keys);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IDungeonNodeDictionary.Factory>();
        var sut = factory(_dungeonData);
            
        Assert.NotNull(sut as DungeonNodeDictionary);
    }
}