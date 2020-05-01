using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Mode : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        private ItemPlacement? _itemPlacement;
        public ItemPlacement? ItemPlacement
        {
            get => _itemPlacement;
            set
            {
                if (_itemPlacement != value)
                {
                    OnPropertyChanging(nameof(ItemPlacement));
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
                    OnPropertyChanging(nameof(DungeonItemShuffle));
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
                    OnPropertyChanging(nameof(WorldState));
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
                    OnPropertyChanging(nameof(EntranceShuffle));
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
                    OnPropertyChanging(nameof(BossShuffle));
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
                    OnPropertyChanging(nameof(EnemyShuffle));
                    _enemyShuffle = value;
                    OnPropertyChanged(nameof(EnemyShuffle));
                }
            }
        }

        public Mode()
        {
        }

        public Mode(Mode source)
        {
            ItemPlacement = source.ItemPlacement;
            DungeonItemShuffle = source.DungeonItemShuffle;
            WorldState = source.WorldState;
            EntranceShuffle = source.EntranceShuffle;
            BossShuffle = source.BossShuffle;
            EnemyShuffle = source.EnemyShuffle;
        }

        private void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
                PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(WorldState) && WorldState == Enums.WorldState.Inverted)
                ItemPlacement = Enums.ItemPlacement.Advanced;
        }

        public bool Validate(Mode gameMode)
        {
            if (gameMode.ItemPlacement != null &&
                ItemPlacement != null &&
                ItemPlacement != gameMode.ItemPlacement)
                return false;

            if (gameMode.DungeonItemShuffle != null && DungeonItemShuffle != null)
            {
                switch (gameMode.DungeonItemShuffle.Value)
                {
                    case Enums.DungeonItemShuffle.Standard:
                    case Enums.DungeonItemShuffle.MapsCompasses:
                        if (DungeonItemShuffle.Value != Enums.DungeonItemShuffle.Standard &&
                            DungeonItemShuffle.Value != Enums.DungeonItemShuffle.MapsCompasses)
                            return false;
                        break;
                    case Enums.DungeonItemShuffle.MapsCompassesSmallKeys:
                    case Enums.DungeonItemShuffle.Keysanity:
                        if (DungeonItemShuffle.Value < gameMode.DungeonItemShuffle.Value)
                            return false;
                        break;
                }
            }

            if (gameMode.WorldState != null && WorldState != null)
            {
                switch (gameMode.WorldState.Value)
                {
                    case Enums.WorldState.StandardOpen:
                        if (WorldState.Value != Enums.WorldState.StandardOpen &&
                            WorldState.Value != Enums.WorldState.Retro)
                            return false;
                        break;
                    case Enums.WorldState.Inverted:
                    case Enums.WorldState.Retro:
                        if (WorldState.Value != gameMode.WorldState.Value)
                            return false;
                        break;
                }
            }

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
    }
}