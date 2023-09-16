using System;
using System.Collections.Generic;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Nodes;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Items;

public class DungeonItemTests
{
    private readonly IDungeonItemDictionary _dungeonItems = Substitute.For<IDungeonItemDictionary>();
    private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();
    private readonly IOverworldNode _node = Substitute.For<IOverworldNode>();
        
    private readonly DungeonItem _sut;

    public DungeonItemTests()
    {
        _dungeonData.DungeonItems.Returns(_dungeonItems);
            
        _sut = new DungeonItem(_dungeonData, _node);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        _node.Accessibility.Returns(AccessibilityLevel.Normal);
            
        Assert.PropertyChanged(_sut, nameof(IDungeonItem.Accessibility),
            () => _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _node, new PropertyChangedEventArgs(nameof(IDungeonNode.Accessibility))));
    }

    [Fact]
    public void ItemCreated_ShouldUpdateAccessibility()
    {
        const AccessibilityLevel accessibility = AccessibilityLevel.Normal;
        _node.Accessibility.Returns(accessibility);
        _dungeonItems.ItemCreated += Raise.Event<EventHandler<KeyValuePair<DungeonItemID, IDungeonItem>>>(
            _dungeonItems, new KeyValuePair<DungeonItemID, IDungeonItem>(
                DungeonItemID.HCSanctuary, _sut));
            
        Assert.Equal(accessibility, _sut.Accessibility);
    }

    [Fact]
    public void ItemCreated_ShouldNotUpdateAccessibility_WhenThisNotCreated()
    {
        _node.Accessibility.Returns(AccessibilityLevel.Normal);
        _dungeonItems.ItemCreated += Raise.Event<EventHandler<KeyValuePair<DungeonItemID, IDungeonItem>>>(
            _dungeonItems, new KeyValuePair<DungeonItemID, IDungeonItem>(
                DungeonItemID.HCSanctuary, Substitute.For<IDungeonItem>()));
            
        Assert.Equal(AccessibilityLevel.None, _sut.Accessibility);
    }

    [Fact]
    public void NodeChanged_ShouldUpdateAccessibility()
    {
        const AccessibilityLevel accessibility = AccessibilityLevel.Normal;
        _node.Accessibility.Returns(accessibility);
        _node.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _node, new PropertyChangedEventArgs(nameof(IDungeonNode.Accessibility)));
            
        Assert.Equal(accessibility, _sut.Accessibility);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IDungeonItem.Factory>();
        var sut = factory(_dungeonData, _node);
            
        Assert.NotNull(sut as DungeonItem);
    }
}