using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class Game
    {
        public Mode Mode { get; }
        public ItemDictionary Items { get; }
        public Dictionary<LocationID, Location> Locations { get; }

        public Game()
        {
            Items = new ItemDictionary(Enum.GetValues(typeof(ItemType)).Length);
            Locations = new Dictionary<LocationID, Location>(Enum.GetValues(typeof(LocationID)).Length);
            Mode = new Mode()
            {
                ItemPlacement = ItemPlacement.Basic,
                DungeonItemShuffle = DungeonItemShuffle.Standard,
                WorldState = WorldState.StandardOpen,
                EntranceShuffle = false,
                BossShuffle = false,
                EnemyShuffle = false
            };

            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
                Items.Add(type, new Item(type));

            foreach (LocationID iD in Enum.GetValues(typeof(LocationID)))
                Locations.Add(iD, new Location(this, iD));
        }
    }
}
