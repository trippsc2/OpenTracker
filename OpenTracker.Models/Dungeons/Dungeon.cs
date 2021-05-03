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
using ReactiveUI;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    ///     This class contains the immutable dungeon data.
    /// </summary>
    public class Dungeon : ReactiveObject, IDungeon
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
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings data.
        /// </param>
        /// <param name="id">
        ///     The dungeon ID.
        /// </param>
        /// <param name="keyLayoutFactory">
        ///     A factory for creating key layouts.
        /// </param>
        /// <param name="map">
        ///     The map item for the dungeon.
        /// </param>
        /// <param name="compass">
        ///     The compass item for the dungeon.
        /// </param>
        /// <param name="smallKey">
        ///     The small key item for the dungeon.
        /// </param>
        /// <param name="bigKey">
        ///     The big key item for the dungeon.
        /// </param>
        /// <param name="dungeonItems">
        ///     A list of dungeon item IDs in this dungeon.
        /// </param>
        /// <param name="bosses">
        ///     A list of dungeon item IDs for bosses in this dungeon.
        /// </param>
        /// <param name="smallKeyDrops">
        ///     A list of dungeon item IDs for small key drops in this dungeon.
        /// </param>
        /// <param name="bigKeyDrops">
        ///     A list of dungeon item IDs for big key drops in this dungeon.
        /// </param>
        /// <param name="smallKeyDoors">
        ///     A list of small key door IDs in this dungeon.
        /// </param>
        /// <param name="bigKeyDoors">
        ///     A list of big key door IDs in this dungeon.
        /// </param>
        /// <param name="nodes">
        ///     A list of dungeon nodes in this dungeon.
        /// </param>
        /// <param name="entryNodes">
        ///     A list of entry nodes for this dungeon.
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
        ///     Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
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
        ///     Updates the value of the Total and TotalWithMapAndCompass properties.
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
}
