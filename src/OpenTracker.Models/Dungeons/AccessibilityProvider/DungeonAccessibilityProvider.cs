using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.KeyDoor;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider;

/// <summary>
/// This class contains the logic for updating the dungeon accessibility.
/// </summary>
[DependencyInjection]
public sealed class DungeonAccessibilityProvider : ReactiveObject, IDungeonAccessibilityProvider
{
    private readonly IDungeon _dungeon;
    private readonly IMutableDungeonQueue _mutableDungeonQueue;
        
    private readonly IKeyDoorIterator _keyDoorIterator;
    private readonly IResultAggregator _resultAggregator;

    [Reactive]
    public bool Visible { get; private set; }
    [Reactive]
    public bool SequenceBreak { get; private set; }
    [Reactive]
    public int Accessible { get; private set; }

    public List<BossAccessibilityProvider> BossAccessibilityProviders { get; } = new();

    private ICappedItem? Map => _dungeon.Map;
    private ICappedItem? Compass => _dungeon.Compass;
    private ISmallKeyItem SmallKey => _dungeon.SmallKey;
    private IBigKeyItem? BigKey => _dungeon.BigKey;

    private IEnumerable<DungeonItemID> SmallKeyDrops => _dungeon.SmallKeyDrops;
    private IEnumerable<DungeonItemID> BigKeyDrops => _dungeon.BigKeyDrops;
    private IEnumerable<DungeonItemID> Bosses => _dungeon.Bosses;
    private IEnumerable<DungeonNodeID> Nodes => _dungeon.Nodes;
    private IEnumerable<INode> EntryNodes => _dungeon.EntryNodes;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="mutableDungeonQueue">
    ///     An Autofac factory for creating <see cref="IMutableDungeonQueue"/> objects.
    /// </param>
    /// <param name="dungeon">
    ///     The <see cref="IDungeon"/> data.
    /// </param>
    /// <param name="keyDoorIterator">
    ///     The <see cref="IKeyDoorIterator"/>.
    /// </param>
    /// <param name="resultAggregator">
    ///     The <see cref="IResultAggregator"/>.
    /// </param>
    public DungeonAccessibilityProvider(
        IMode mode, 
        IMutableDungeonQueue.Factory mutableDungeonQueue,
        IDungeon dungeon,
        IKeyDoorIterator.Factory keyDoorIterator, IResultAggregator.Factory resultAggregator)
    {
        _dungeon = dungeon;
        _mutableDungeonQueue = mutableDungeonQueue(_dungeon);

        _keyDoorIterator = keyDoorIterator(_dungeon, _mutableDungeonQueue);
        _resultAggregator = resultAggregator(_dungeon, _mutableDungeonQueue);

        foreach (var _ in Bosses)
        {
            BossAccessibilityProviders.Add(new BossAccessibilityProvider());
        }

        mode.PropertyChanged += OnModeChanged;

        if (BigKey is not null)
        {
            BigKey.PropertyChanged += OnBigKeyChanged;
        }

        SmallKey.PropertyChanged += OnSmallKeyChanged;

        foreach (var node in EntryNodes)
        {
            ((IOverworldNode) node).ChangePropagated += OnNodeChanged;
        }
            
        SubscribeToConnectionRequirements();
        UpdateValues();
    }

    /// <summary>
    /// Subscribes to the <see cref="IMode.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IMode.MapShuffle) when Map is not null:
            case nameof(IMode.CompassShuffle) when Compass is not null:
            case nameof(IMode.SmallKeyShuffle):
            case nameof(IMode.BigKeyShuffle) when BigKey is not null:
            case nameof(IMode.GenericKeys):
            case nameof(IMode.GuaranteedBossItems) when Bosses.Any():
            case nameof(IMode.KeyDropShuffle) when SmallKeyDrops.Any() || BigKeyDrops.Any():
                UpdateValues();
                break;
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="IItem.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnBigKeyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IItem.Current))
        {
            UpdateValues();
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="ISmallKeyItem.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnSmallKeyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ISmallKeyItem.EffectiveCurrent))
        {
            UpdateValues();
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="IRequirement.ChangePropagated"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="EventArgs"/>.
    /// </param>
    private void OnRequirementChangePropagated(object? sender, EventArgs e)
    {
        UpdateValues();
    }

    /// <summary>
    /// Subscribes to the <see cref="IOverworldNode.ChangePropagated"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="EventArgs"/>.
    /// </param>
    private void OnNodeChanged(object? sender, EventArgs e)
    {
        UpdateValues();
    }

    /// <summary>
    /// Subscribes to PropertyChanged event on each requirement.
    /// </summary>
    private void SubscribeToConnectionRequirements()
    {
        var requirementSubscriptions = new List<IRequirement>();
        var dungeonData = _mutableDungeonQueue.GetNext();

        foreach (var node in Nodes)
        {
            foreach (var connection in dungeonData.Nodes[node].Connections)
            {
                var requirement = connection.Requirement;

                if (requirement is null || requirement is KeyDoorRequirement ||
                    requirementSubscriptions.Contains(requirement))
                {
                    continue;
                }
                    
                requirement.ChangePropagated += OnRequirementChangePropagated;
                requirementSubscriptions.Add(requirement);
            }
        }
            
        _mutableDungeonQueue.Requeue(dungeonData);
    }

    /// <summary>
    /// Updates all values in the accessibility provider.
    /// </summary>
    private void UpdateValues()
    {
        var finalQueue = new BlockingCollection<IDungeonState>();

        _keyDoorIterator.ProcessKeyDoorPermutations(finalQueue);
        var result = _resultAggregator.AggregateResults(finalQueue);

        for (var i = 0; i < result.BossAccessibility.Count; i++)
        {
            BossAccessibilityProviders[i].Accessibility = result.BossAccessibility[i];
        }

        Visible = result.Visible;
        SequenceBreak = result.SequenceBreak;
        Accessible = result.Accessible;
    }
}