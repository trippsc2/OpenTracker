using System;
using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.KeyLayouts.Factories;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons;

/// <summary>
/// This class contains the immutable dungeon data.
/// </summary>
[DependencyInjection]
public sealed class Dungeon : ReactiveObject, IDungeon
{
    private readonly IMode _mode;
        
    public DungeonID ID { get; }
        
    public ICappedItem? Map { get; }
    public ICappedItem? Compass { get; }
    public ISmallKeyItem SmallKey { get; }
    public IBigKeyItem? BigKey { get; }

    public IList<DungeonItemID> DungeonItems { get; }
    public IList<DungeonItemID> Bosses { get; }
    public IList<DungeonItemID> SmallKeyDrops { get; }
    public IList<DungeonItemID> BigKeyDrops { get; }
    public IList<KeyDoorID> SmallKeyDoors { get; }
    public IList<KeyDoorID> BigKeyDoors { get; }
    public IList<IKeyLayout> KeyLayouts { get; }
    public IList<DungeonNodeID> Nodes { get; }
    public IList<IOverworldNode> EntryNodes { get; }

    public int TotalWithMapAndCompass { get; private set; }

    private int _total;
    public int Total
    {
        get => _total;
        private set => this.RaiseAndSetIfChanged(ref _total, value);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mode">
    ///     The <see cref="IMode"/> data.
    /// </param>
    /// <param name="keyLayoutFactory">
    ///     The <see cref="IKeyLayoutFactory"/>.
    /// </param>
    /// <param name="id">
    ///     The <see cref="DungeonID"/>.
    /// </param>
    /// <param name="map">
    ///     The nullable <see cref="ICappedItem"/> representing the dungeon map.
    /// </param>
    /// <param name="compass">
    ///     The nullable <see cref="ICappedItem"/> representing the dungeon compass.
    /// </param>
    /// <param name="smallKey">
    ///     The <see cref="ISmallKeyItem"/> representing the dungeon small key.
    /// </param>
    /// <param name="bigKey">
    ///     The nullable <see cref="IBigKeyItem"/> representing the dungeon big key.
    /// </param>
    /// <param name="dungeonItems">
    ///     A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon items.
    /// </param>
    /// <param name="bosses">
    ///     A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon bosses.
    /// </param>
    /// <param name="smallKeyDrops">
    ///     A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon small key drops.
    /// </param>
    /// <param name="bigKeyDrops">
    ///     A <see cref="IList{T}"/> of <see cref="DungeonItemID"/> representing the dungeon big key drops.
    /// </param>
    /// <param name="smallKeyDoors">
    ///     A <see cref="IList{T}"/> of <see cref="KeyDoorID"/> representing the dungeon small key doors.
    /// </param>
    /// <param name="bigKeyDoors">
    ///     A <see cref="IList{T}"/> of <see cref="KeyDoorID"/> representing the dungeon big key doors.
    /// </param>
    /// <param name="nodes">
    ///     A <see cref="IList{T}"/> of <see cref="DungeonNodeID"/> representing the dungeon nodes.
    /// </param>
    /// <param name="entryNodes">
    ///     A <see cref="IList{T}"/> of <see cref="IOverworldNode"/> representing the dungeon entry nodes.
    /// </param>
    public Dungeon(
        IMode mode, IKeyLayoutFactory keyLayoutFactory, DungeonID id, ICappedItem? map, ICappedItem? compass,
        ISmallKeyItem smallKey, IBigKeyItem? bigKey, IList<DungeonItemID> dungeonItems, IList<DungeonItemID> bosses,
        IList<DungeonItemID> smallKeyDrops, IList<DungeonItemID> bigKeyDrops, IList<KeyDoorID> smallKeyDoors,
        IList<KeyDoorID> bigKeyDoors, IList<DungeonNodeID> nodes, IList<IOverworldNode> entryNodes)
    {
        _mode = mode;

        ID = id;
            
        Map = map;
        Compass = compass;
        SmallKey = smallKey;
        BigKey = bigKey;
            
        DungeonItems = dungeonItems;
        Bosses = bosses;

        SmallKeyDrops = smallKeyDrops;
        BigKeyDrops = bigKeyDrops;

        SmallKeyDoors = smallKeyDoors;
        BigKeyDoors = bigKeyDoors;

        KeyLayouts = keyLayoutFactory.GetDungeonKeyLayouts(this);

        Nodes = nodes;
        EntryNodes = entryNodes;

        _mode.PropertyChanged += OnModeChanged;
            
        UpdateTotal();
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
            case nameof(IMode.KeyDropShuffle) when SmallKeyDrops.Count > 0 || BigKeyDrops.Count > 0:
            case nameof(IMode.MapShuffle) when Map is not null:
            case nameof(IMode.CompassShuffle) when Compass is not null:
            case nameof(IMode.SmallKeyShuffle):
            case nameof(IMode.BigKeyShuffle) when BigKey is not null:
                UpdateTotal();
                break;
        }
    }

    /// <summary>
    /// Updates the value of the <see cref="Total"/> and <see cref="TotalWithMapAndCompass"/> properties.
    /// </summary>
    private void UpdateTotal()
    {
        var total = DungeonItems.Count;

        if (_mode.KeyDropShuffle)
        {
            total += SmallKeyDrops.Count + BigKeyDrops.Count;
        }
            
        if (!_mode.SmallKeyShuffle)
        {
            total -= SmallKey.Maximum;
        }

        if (!_mode.BigKeyShuffle && BigKey is not null)
        {
            total -= BigKey.Maximum;
        }

        TotalWithMapAndCompass = Math.Max(0, total);

        if (!_mode.MapShuffle && Map is not null)
        {
            total -= Map.Maximum;
        }

        if (!_mode.CompassShuffle && Compass is not null)
        {
            total -= Compass.Maximum;
        }

        Total = Math.Max(0, total);
    }
}