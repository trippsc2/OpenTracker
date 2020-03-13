using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Mode : INotifyPropertyChanged
    {
        private ItemPlacement? _itemPlacement;
        public ItemPlacement? ItemPlacement
        {
            get => _itemPlacement;
            set
            {
                if (_itemPlacement != value)
                {
                    _itemPlacement = value;
                    OnPropertyChanged(nameof(ItemPlacement));
                }
            }
        }

        private DungeonItemShuffle? _dungeonItemShuffle;
        public DungeonItemShuffle? DungeonItemShuffle
        {
            get => _dungeonItemShuffle;
            set
            {
                if (_dungeonItemShuffle != value)
                {
                    _dungeonItemShuffle = value;
                    OnPropertyChanged(nameof(DungeonItemShuffle));
                }
            }
        }

        private WorldState? _worldState;
        public WorldState? WorldState
        {
            get => _worldState;
            set
            {
                if (_worldState != value)
                {
                    _worldState = value;
                    OnPropertyChanged(nameof(WorldState));
                }
            }
        }

        private bool? _entranceShuffle;
        public bool? EntranceShuffle
        {
            get => _entranceShuffle;
            set
            {
                if (_entranceShuffle != value)
                {
                    _entranceShuffle = value;
                    OnPropertyChanged(nameof(EntranceShuffle));
                }
            }
        }

        private bool? _bossShuffle;
        public bool? BossShuffle
        {
            get => _bossShuffle;
            set
            {
                if (_bossShuffle != value)
                {
                    _bossShuffle = value;
                    OnPropertyChanged(nameof(BossShuffle));
                }
            }
        }

        private bool? _enemyShuffle;
        public bool? EnemyShuffle
        {
            get => _enemyShuffle;
            set
            {
                if (_enemyShuffle != value)
                {
                    _enemyShuffle = value;
                    OnPropertyChanged(nameof(EnemyShuffle));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Validate(Mode gameMode)
        {
            if (gameMode.ItemPlacement != null &&
                ItemPlacement != null &&
                ItemPlacement != gameMode.ItemPlacement)
                return false;

            if (gameMode.DungeonItemShuffle != null &&
                DungeonItemShuffle != null &&
                DungeonItemShuffle != gameMode.DungeonItemShuffle)
                return false;

            if (gameMode.WorldState != null &&
                WorldState != null &&
                WorldState != gameMode.WorldState)
                return false;

            if (gameMode.EntranceShuffle != null &&
                EntranceShuffle != null &&
                EntranceShuffle != gameMode.EntranceShuffle)
                return false;

            if (gameMode.BossShuffle != null &&
                BossShuffle != null &&
                BossShuffle != gameMode.BossShuffle)
                return false;

            if (gameMode.EnemyShuffle != null &&
                EnemyShuffle != null &&
                EnemyShuffle != gameMode.EnemyShuffle)
                return false;

            return true;
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}