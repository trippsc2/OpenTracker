using OpenTracker.Models.Dictionaries;
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
        public BossDictionary Bosses { get; }
        public BossPlacementDictionary BossPlacements { get; }
        public RequirementDictionary Requirements { get; }
        public RequirementNodeDictionary RequirementNodes { get; }
        public LocationDictionary Locations { get; }
        public ObservableCollection<(MapLocation, MapLocation)> Connections { get; }

        /// <summary>
        /// Basic constructor
        /// </summary>
        public Game()
        {
            AutoTracker = new AutoTracker();
            Mode = new Mode();

            Items = new ItemDictionary(this);
            Bosses = new BossDictionary(this);
            BossPlacements = new BossPlacementDictionary(this);
            Requirements = new RequirementDictionary(this);
            RequirementNodes = new RequirementNodeDictionary(this);
            Locations = new LocationDictionary(this);
            Connections = new ObservableCollection<(MapLocation, MapLocation)>();

            RequirementNodes.Initialize();
            Locations.Initialize();
            Requirements.Initialize();
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
