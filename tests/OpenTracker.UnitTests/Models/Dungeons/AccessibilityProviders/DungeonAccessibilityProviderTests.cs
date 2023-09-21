using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.AccessibilityProviders;

[ExcludeFromCodeCoverage]
public sealed class DungeonAccessibilityProviderTests
{
    private readonly IDungeon _dungeon = Substitute.For<IDungeon>();
    private readonly IMode _mode = Substitute.For<IMode>();
    private readonly IMutableDungeonQueue _mutableDungeonQueue = Substitute.For<IMutableDungeonQueue>();

    private readonly IKeyDoorIterator _keyDoorIterator = Substitute.For<IKeyDoorIterator>();
    private readonly IResultAggregator _resultAggregator = Substitute.For<IResultAggregator>();

    private readonly ISmallKeyItem _smallKey = Substitute.For<ISmallKeyItem>();
    private readonly IBigKeyItem _bigKey = Substitute.For<IBigKeyItem>();

    private readonly List<DungeonItemID> _dungeonItems = new();
    private readonly List<DungeonItemID> _bosses = new();
    private readonly List<DungeonItemID> _smallKeyDrops = new();
    private readonly List<DungeonItemID> _bigKeyDrops = new();
    private readonly List<KeyDoorID> _smallKeyDoors = new();
    private readonly List<KeyDoorID> _bigKeyDoors = new();
    private readonly List<IKeyLayout> _keyLayouts = new();
    private readonly List<DungeonNodeID> _nodes = new();
    private readonly List<IOverworldNode> _entryNodes = new() { Substitute.For<IOverworldNode>() };

    private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();

    private readonly List<AccessibilityLevel> _bossAccessibility = new();
    private readonly IDungeonResult _result = Substitute.For<IDungeonResult>();

    public DungeonAccessibilityProviderTests()
    {
        _dungeon.SmallKey.Returns(_smallKey);
        _dungeon.BigKey.Returns(_bigKey);

        _dungeon.DungeonItems.Returns(_dungeonItems);
        _dungeon.Bosses.Returns(_bosses);
        _dungeon.SmallKeyDrops.Returns(_smallKeyDrops);
        _dungeon.BigKeyDrops.Returns(_bigKeyDrops);
        _dungeon.SmallKeyDoors.Returns(_smallKeyDoors);
        _dungeon.BigKeyDoors.Returns(_bigKeyDoors);
        _dungeon.KeyLayouts.Returns(_keyLayouts);
        _dungeon.Nodes.Returns(_nodes);
        _dungeon.EntryNodes.Returns(_entryNodes);

        _mutableDungeonQueue.GetNext().Returns(_dungeonData);

        _smallKey.GetKeyValues().Returns(new List<int> {0});
        _bigKey.GetKeyValues().Returns(new List<bool> {false});

        _result.BossAccessibility.Returns(_bossAccessibility);

        _resultAggregator.AggregateResults(Arg.Any<BlockingCollection<IDungeonState>>()).Returns(_result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    [InlineData(4, 4)]
    public void Ctor_ShouldCreateBossProvidersEqualToBossCount(int expected, int bossCount)
    {
        for (var i = 0; i < bossCount; i++)
        {
            _bosses.Add(DungeonItemID.ATBoss);
            _bossAccessibility.Add(AccessibilityLevel.None);
        }
        
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);

        sut.BossAccessibilityProviders.Count.Should().Be(expected);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    [InlineData(4, 4)]
    public void Accessible_ShouldEqualResultValue(int expected, int accessible)
    {
        _result.Accessible.Returns(accessible);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);

        sut.Accessible.Should().Be(expected);
    }

    [Fact]
    public void Accessible_ShouldRaisePropertyChanged()
    {
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        using var monitor = sut.Monitor();
        
        _result.Accessible.Returns(1);
        _smallKey.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _smallKey,
                new PropertyChangedEventArgs(nameof(ISmallKeyItem.EffectiveCurrent)));
        
        monitor.Should().RaisePropertyChangeFor(x => x.Accessible);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void SequenceBreak_ShouldEqualResultValue(bool expected, bool sequenceBreak)
    {
        _result.SequenceBreak.Returns(sequenceBreak);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);

        sut.SequenceBreak.Should().Be(expected);
    }

    [Fact]
    public void SequenceBreak_ShouldRaisePropertyChanged()
    {
        _result.SequenceBreak.Returns(false);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        using var monitor = sut.Monitor();
        
        _result.SequenceBreak.Returns(true);
        _smallKey.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _smallKey,
                new PropertyChangedEventArgs(nameof(ISmallKeyItem.EffectiveCurrent)));

        monitor.Should().RaisePropertyChangeFor(x => x.SequenceBreak);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Visible_ShouldEqualResultValue(bool expected, bool sequenceBreak)
    {
        _result.Visible.Returns(sequenceBreak);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);

        sut.Visible.Should().Be(expected);
    }

    [Fact]
    public void Visible_ShouldRaisePropertyChanged()
    {
        _result.Visible.Returns(false);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        using var monitor = sut.Monitor();
        
        _result.Visible.Returns(true);
        _smallKey.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _smallKey,
                new PropertyChangedEventArgs(nameof(ISmallKeyItem.EffectiveCurrent)));
        
        monitor.Should().RaisePropertyChangeFor(x => x.Visible);
    }

    [Fact]
    public void BigKeyChanged_ShouldUpdateValues()
    {
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        Assert.PropertyChanged(sut, nameof(IDungeonAccessibilityProvider.Accessible), () =>
            _bigKey.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _smallKey, new PropertyChangedEventArgs(nameof(IBigKeyItem.Current))));
    }

    [Fact]
    public void ModeChanged_ShouldUpdateValues_WhenMapShuffleChangedAndMapIsNotNull()
    {
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.MapShuffle)));
            
        Assert.Equal(1, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldDoNothing_WhenMapShuffleChangedAndMapIsNull()
    {
        _dungeon.Map.ReturnsNull();
            
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.MapShuffle)));
            
        Assert.Equal(0, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldUpdateValues_WhenCompassShuffleChangedAndCompassIsNotNull()
    {
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.CompassShuffle)));
            
        Assert.Equal(1, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldDoNothing_WhenCompassShuffleChangedAndCompassIsNull()
    {
        _dungeon.Compass.ReturnsNull();
            
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.CompassShuffle)));
            
        Assert.Equal(0, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldUpdateValues_WhenSmallShuffleChanged()
    {
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.SmallKeyShuffle)));
            
        Assert.Equal(1, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldUpdateValues_WhenBigKeyShuffleChangedAndBigKeyIsNotNull()
    {
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.BigKeyShuffle)));
            
        Assert.Equal(1, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldDoNothing_WhenBigKeyShuffleChangedAndBigKeyIsNull()
    {
        _dungeon.BigKey.ReturnsNull();
            
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.BigKeyShuffle)));
            
        Assert.Equal(0, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldUpdateValues_WhenKeyDropShuffleChangedAndSmallKeyDropsExist()
    {
        _dungeon.SmallKeyDrops.Add(DungeonItemID.ATBoss);
            
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.KeyDropShuffle)));
            
        Assert.Equal(1, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldUpdateValues_WhenKeyDropShuffleChangedAndBigKeyDropsExist()
    {
        _dungeon.BigKeyDrops.Add(DungeonItemID.ATBoss);
            
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.KeyDropShuffle)));
            
        Assert.Equal(1, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldDoNothing_WhenKeyDropShuffleChangedAndNoKeyDropsExist()
    {
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.KeyDropShuffle)));
            
        Assert.Equal(0, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldUpdateValues_WhenGenericKeysChanged()
    {
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.GenericKeys)));
            
        Assert.Equal(1, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldUpdateValues_WhenGuaranteedBossItemsChangedAndBossesExist()
    {
        _dungeon.Bosses.Add(DungeonItemID.ATBoss);
            
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.GuaranteedBossItems)));
            
        Assert.Equal(1, sut.Accessible);
    }

    [Fact]
    public void ModeChanged_ShouldDoNothing_WhenGuaranteedBossItemsChangedAndNoBossesExist()
    {
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
            _mode, new PropertyChangedEventArgs(nameof(IMode.GuaranteedBossItems)));
            
        Assert.Equal(0, sut.Accessible);
    }

    [Fact]
    public void NodeChanged_ShouldUpdateValues()
    {
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        Assert.PropertyChanged(sut, nameof(IDungeonAccessibilityProvider.Accessible), () =>
            _entryNodes[0].ChangePropagated += Raise.Event<EventHandler>(_mode, EventArgs.Empty));
    }

    [Fact]
    public void RequirementChangePropagated_ShouldUpdateValues()
    {
        var requirement = Substitute.For<IRequirement>();
        var nodeConnection = Substitute.For<INodeConnection>();
        var node = Substitute.For<IDungeonNode>();
        var nodeConnections = new List<INodeConnection>
        {
            nodeConnection
        };
        node.Connections.Returns(nodeConnections);
        nodeConnection.Requirement.Returns(requirement);
        _nodes.Add(DungeonNodeID.AT);
        _dungeonData.Nodes[Arg.Any<DungeonNodeID>()].Returns(node);
            
        _result.Accessible.Returns(0);
        var sut = new DungeonAccessibilityProvider(_mode,
            _ => _mutableDungeonQueue, _dungeon, (_, _) => _keyDoorIterator,
            (_, _) => _resultAggregator);
        _result.Accessible.Returns(1);
            
        Assert.PropertyChanged(sut, nameof(IDungeonAccessibilityProvider.Accessible), () =>
            requirement.ChangePropagated += Raise.Event<EventHandler>(
                _mode, EventArgs.Empty));
    }

    [Fact]
    public void AutofacResolve_ShouldResolveAsInterfaceToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IDungeonAccessibilityProvider.Factory>();
        var sut1 = factory(_dungeon);

        sut1.Should().BeOfType<DungeonAccessibilityProvider>();
        
        var sut2 = factory(_dungeon);
        
        sut1.Should().NotBeSameAs(sut2);
    }
}