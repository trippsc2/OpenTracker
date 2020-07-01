using OpenTracker.Models.Dictionaries;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the data class containing all game data.
    /// </summary>
    public class Game
    {
        public AutoTracker AutoTracker { get; }
        public Mode Mode { get; }
        public ItemDictionary Items { get; }
        public BossPlacementDictionary BossPlacements { get; }
        public RequirementNodeDictionary RequirementNodes { get; }
        public LocationDictionary Locations { get; }
        public ObservableCollection<(MapLocation, MapLocation)> Connections { get; }
        public Dictionary<SequenceBreakType, SequenceBreak> SequenceBreaks { get; }
        public Dictionary<RequirementType, IRequirement> Requirements { get; }

        /// <summary>
        /// Basic constructor
        /// </summary>
        public Game()
        {
            AutoTracker = new AutoTracker();
            Mode = new Mode();

            Items = new ItemDictionary(this);
            BossPlacements = new BossPlacementDictionary(this);
            RequirementNodes = new RequirementNodeDictionary(this);
            Connections = new ObservableCollection<(MapLocation, MapLocation)>();
            SequenceBreaks = new Dictionary<SequenceBreakType, SequenceBreak>();

            foreach (SequenceBreakType type in Enum.GetValues(typeof(SequenceBreakType)))
            {
                SequenceBreaks.Add(type, new SequenceBreak());
            }

            Requirements = new Dictionary<RequirementType, IRequirement>();
            RequirementFactory.GetAllRequirements(this, Requirements);
            Locations = new LocationDictionary(this);

            RequirementNodes.Initialize();
            Locations.Initialize();
        }

        /// <summary>
        /// Resets all game data to their starting values, except for the game mode.
        /// </summary>
        public void Reset()
        {
            AutoTracker.Stop();
            BossPlacements.Reset();
            Locations.Reset();
            Items.Reset();
            Connections.Clear();
        }
    }
}
