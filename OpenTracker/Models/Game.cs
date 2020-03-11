using OpenTracker.Enums;
using OpenTracker.Interfaces;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class Game
    {
        public ItemDictionary Items { get; }
        public Dictionary<LocationID, ILocation> Locations { get; }
        public Mode Mode { get; }

        public Game()
        {
            Items = new ItemDictionary(Enum.GetValues(typeof(ItemType)).Length);
            Locations = new Dictionary<LocationID, ILocation>(Enum.GetValues(typeof(LocationID)).Length);
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
            {
                if (type <= ItemType.RedCrystal)
                    Items.Add(type, new Item(type));
            }

            foreach (LocationID type in Enum.GetValues(typeof(LocationID)))
            {
                if (type <= LocationID.GanonsTower)
                    Locations.Add(type, new ItemLocation(this, type));
            }
        }
    }
}
