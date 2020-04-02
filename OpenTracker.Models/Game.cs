using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class Game
    {
        public Mode Mode { get; }
        public BossDictionary Bosses { get; }
        public ItemDictionary Items { get; }
        public Dictionary<RegionID, Region> Regions { get; }
        public LocationDictionary Locations { get; }

        public Game()
        {
            Mode = new Mode()
            {
                ItemPlacement = ItemPlacement.Advanced,
                DungeonItemShuffle = DungeonItemShuffle.Standard,
                WorldState = WorldState.StandardOpen,
                EntranceShuffle = false,
                BossShuffle = false,
                EnemyShuffle = false
            };

            Bosses = new BossDictionary(this, Enum.GetValues(typeof(BossType)).Length);
            Items = new ItemDictionary(Mode, Enum.GetValues(typeof(ItemType)).Length);
            Regions = new Dictionary<RegionID, Region>(Enum.GetValues(typeof(RegionID)).Length);
            Locations = new LocationDictionary(Enum.GetValues(typeof(LocationID)).Length);

            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
                Items.Add(type, new Item(type));

            foreach (BossType type in Enum.GetValues(typeof(BossType)))
                Bosses.Add(type, new Boss(this, type));

            Bosses.SubscribeToMemberEvents();

            foreach (RegionID iD in Enum.GetValues(typeof(RegionID)))
                Regions.Add(iD, new Region(this, iD));

            foreach (Region region in Regions.Values)
                region.SubscribeToRegions();

            foreach (LocationID iD in Enum.GetValues(typeof(LocationID)))
                Locations.Add(iD, new Location(this, iD));

        }

        public void Reset()
        {
            Locations.Reset();
            Items.Reset();
        }
    }
}
