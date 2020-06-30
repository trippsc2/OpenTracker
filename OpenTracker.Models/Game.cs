using OpenTracker.Models.Dictionaries;
using System.Collections.ObjectModel;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the data class containing all game data.
    /// </summary>
    public class Game
    {
        private static readonly object _syncLock = new object();

        private static volatile Game _instance;
        public static Game Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Game();
                    }
                }

                return _instance;
            }
        }

        public AutoTracker AutoTracker { get; private set; }
        public Mode Mode { get; private set; }
        public ItemDictionary Items { get; private set; }
        public BossDictionary Bosses { get; private set; }
        public BossPlacementDictionary BossPlacements { get; private set; }
        public RequirementDictionary Requirements { get; private set; }
        public RequirementNodeDictionary RequirementNodes { get; private set; }
        public LocationDictionary Locations { get; private set; }
        public ObservableCollection<(MapLocation, MapLocation)> Connections { get; private set; }

        /// <summary>
        /// Basic constructor
        /// </summary>
        private Game()
        {
        }

        public void Initialize()
        {
            AutoTracker = new AutoTracker();
            Mode = new Mode();

            Items = new ItemDictionary();
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
