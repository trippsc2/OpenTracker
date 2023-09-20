using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Items;

[ExcludeFromCodeCoverage]
public sealed class DungeonItemDictionaryTests
{
    private readonly IDungeonItemFactory _factory = Substitute.For<IDungeonItemFactory>();
    private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();
    private readonly IDungeonItem _dungeonItem = Substitute.For<IDungeonItem>();
        
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly IDungeonItemDictionary _sut;

    public DungeonItemDictionaryTests()
    {
        _factory.GetDungeonItem(Arg.Any<IMutableDungeon>(), Arg.Any<DungeonItemID>()).Returns(_dungeonItem);
        _sut = new DungeonItemDictionary(() => _factory, _dungeonData);
    }

    [Fact]
    public void PopulateItems_ShouldCreateSpecifiedItems()
    {
        var items = new List<DungeonItemID>
        {
            DungeonItemID.ATBoss
        };
            
        _sut.PopulateItems(items);

        Assert.Contains(DungeonItemID.ATBoss, _sut.Keys);
    }
    
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IDungeonItemDictionary.Factory>();
        var sut = factory(_dungeonData);
            
        Assert.NotNull(sut as DungeonItemDictionary);
    }
}