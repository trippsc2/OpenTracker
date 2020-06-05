using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenTracker.Models
{
    public class Game
    {
        public AutoTracker AutoTracker { get; }
        public Mode Mode { get; }
        public ItemDictionary Items { get; }
        public BossDictionary Bosses { get; }
        public Dictionary<BossPlacementID, BossPlacement> BossPlacements { get; }
        public Dictionary<RequirementType, Requirement> Requirements { get; }
        public Dictionary<RequirementNodeID, RequirementNode> RequirementNodes { get; }
        public LocationDictionary Locations { get; }
        public ObservableCollection<(MapLocation, MapLocation)> Connections { get; }

        public Game()
        {
            AutoTracker = new AutoTracker();
            Mode = new Mode()
            {
                ItemPlacement = ItemPlacement.Advanced,
                DungeonItemShuffle = DungeonItemShuffle.Standard,
                WorldState = WorldState.StandardOpen,
                EntranceShuffle = false,
                BossShuffle = false,
                EnemyShuffle = false
            };

            Items = new ItemDictionary(Mode, Enum.GetValues(typeof(ItemType)).Length);
            Bosses = new BossDictionary(Enum.GetValues(typeof(BossType)).Length);
            BossPlacements =
                new Dictionary<BossPlacementID, BossPlacement>(Enum.GetValues(typeof(BossPlacementID)).Length);
            Requirements =
                new Dictionary<RequirementType, Requirement>(Enum.GetValues(typeof(RequirementType)).Length);
            RequirementNodes =
                new Dictionary<RequirementNodeID, RequirementNode>(Enum.GetValues(typeof(RequirementNodeID)).Length);
            Locations = new LocationDictionary(Enum.GetValues(typeof(LocationID)).Length);
            Connections = new ObservableCollection<(MapLocation, MapLocation)>();

            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
                Items.Add(type, new Item(this, type));

            foreach (BossType type in Enum.GetValues(typeof(BossType)))
                Bosses.Add(type, new Boss(this, type));

            foreach (BossPlacementID iD in Enum.GetValues(typeof(BossPlacementID)))
                BossPlacements.Add(iD, new BossPlacement(this, iD));

            foreach (RequirementType type in Enum.GetValues(typeof(RequirementType)))
                Requirements.Add(type, new Requirement(this, type));

            foreach (RequirementNodeID iD in Enum.GetValues(typeof(RequirementNodeID)))
            {
                if (iD < RequirementNodeID.HCSanctuary)
                    RequirementNodes.Add(iD, new RequirementNode(this, iD));
            }

            foreach (LocationID iD in Enum.GetValues(typeof(LocationID)))
                Locations.Add(iD, new Location(this, iD));

            Bosses.Initialize();

            foreach (RequirementNode node in RequirementNodes.Values)
                node.Initialize();

            foreach (Location location in Locations.Values)
            {
                foreach (ISection section in location.Sections)
                {
                    if (section is DungeonItemSection dungeonItemSection)
                        dungeonItemSection.Initialize();
                }
            }
        }

        public void Reset()
        {
            AutoTracker.Stop();
            Locations.Reset();
            Items.Reset();
            Connections.Clear();
        }
    }
}
