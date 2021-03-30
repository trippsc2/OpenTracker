using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.KeyLayouts.Factories;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This is the class containing dungeon data.
    /// </summary>
    public class Dungeon : ReactiveObject, IDungeon
    {
        private readonly IMode _mode;
        
        public DungeonID ID { get; }
        
        public ICappedItem? Map { get; }
        public ICappedItem? Compass { get; }
        public ISmallKeyItem SmallKey { get; }
        public IBigKeyItem? BigKey { get; }

        public List<DungeonItemID> DungeonItems { get; }
        public List<DungeonItemID> Bosses { get; }
        public List<DungeonItemID> SmallKeyDrops { get; }
        public List<DungeonItemID> BigKeyDrops { get; }
        public List<KeyDoorID> SmallKeyDoors { get; }
        public List<KeyDoorID> BigKeyDoors { get; }
        public List<IKeyLayout> KeyLayouts { get; }
        public List<DungeonNodeID> Nodes { get; }
        public List<IRequirementNode> EntryNodes { get; }

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
        /// The mode settings data.
        /// </param>
        /// <param name="id">
        /// The dungeon ID.
        /// </param>
        /// <param name="keyLayoutFactory">
        /// A factory for creating key layouts.
        /// </param>
        /// <param name="map">
        /// The map item for the dungeon.
        /// </param>
        /// <param name="compass">
        /// The compass item for the dungeon.
        /// </param>
        /// <param name="smallKey">
        /// The small key item for the dungeon.
        /// </param>
        /// <param name="bigKey">
        /// The big key item for the dungeon.
        /// </param>
        /// <param name="dungeonItems">
        /// A list of dungeon item IDs in this dungeon.
        /// </param>
        /// <param name="bosses">
        /// A list of dungeon item IDs for bosses in this dungeon.
        /// </param>
        /// <param name="smallKeyDrops">
        /// A list of dungeon item IDs for small key drops in this dungeon.
        /// </param>
        /// <param name="bigKeyDrops">
        /// A list of dungeon item IDs for big key drops in this dungeon.
        /// </param>
        /// <param name="smallKeyDoors">
        /// A list of small key door IDs in this dungeon.
        /// </param>
        /// <param name="bigKeyDoors">
        /// A list of big key door IDs in this dungeon.
        /// </param>
        /// <param name="nodes">
        /// A list of dungeon nodes in this dungeon.
        /// </param>
        /// <param name="entryNodes">
        /// A list of entry nodes for this dungeon.
        /// </param>
        public Dungeon(
            IMode mode, IKeyLayoutFactory keyLayoutFactory, DungeonID id, ICappedItem? map, ICappedItem? compass,
            ISmallKeyItem smallKey, IBigKeyItem? bigKey, List<DungeonItemID> dungeonItems, List<DungeonItemID> bosses,
            List<DungeonItemID> smallKeyDrops, List<DungeonItemID> bigKeyDrops, List<KeyDoorID> smallKeyDoors,
            List<KeyDoorID> bigKeyDoors, List<DungeonNodeID> nodes, List<IRequirementNode> entryNodes)
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
        /// Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IMode.KeyDropShuffle) when SmallKeyDrops.Count > 0 || BigKeyDrops.Count > 0:
                case nameof(IMode.MapShuffle) when Map is not null:
                case nameof(IMode.CompassShuffle) when Compass is not null:
                case nameof(IMode.SmallKeyShuffle):
                case nameof(IMode.BigKeyShuffle):
                    UpdateTotal();
                    break;
            }
        }

        private void UpdateTotal()
        {
            var total = DungeonItems.Count;

            if (_mode.KeyDropShuffle)
            {
                total += SmallKeyDrops.Count + BigKeyDrops.Count;
            }

            if (!_mode.MapShuffle && Map is not null)
            {
                total -= Map.Maximum;
            }

            if (!_mode.CompassShuffle && Compass is not null)
            {
                total -= Compass.Maximum;
            }

            if (!_mode.SmallKeyShuffle)
            {
                total -= SmallKey.Maximum;
            }

            if (!_mode.BigKeyShuffle && BigKey is not null)
            {
                total -= BigKey.Maximum;
            }

            Total = total;
        }
    }
}
