using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class for the game mode settings.
    /// </summary>
    public class Mode : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool MapCompassShuffle =>
            DungeonItemShuffle >= DungeonItemShuffle.MapsCompasses;
        public bool SmallKeyShuffle =>
            DungeonItemShuffle >= DungeonItemShuffle.MapsCompassesSmallKeys ||
            WorldState == WorldState.Retro;
        public bool BigKeyShuffle =>
            DungeonItemShuffle >= DungeonItemShuffle.Keysanity;

        private ItemPlacement _itemPlacement;
        public ItemPlacement ItemPlacement
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

        private DungeonItemShuffle _dungeonItemShuffle;
        public DungeonItemShuffle DungeonItemShuffle
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

        private WorldState _worldState;
        public WorldState WorldState
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

        private bool _entranceShuffle;
        public bool EntranceShuffle
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

        private bool _bossShuffle;
        public bool BossShuffle
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

        private bool _enemyShuffle;
        public bool EnemyShuffle
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemPlacement">
        /// The Item Placement mode setting.
        /// </param>
        /// <param name="dungeonItemShuffle">
        /// The Dungeon Item Shuffle setting.
        /// </param>
        /// <param name="worldState">
        /// The World State setting.
        /// </param>
        /// <param name="entranceShuffle">
        /// A boolean representing whether Entrance Shuffle is enabled.
        /// </param>
        /// <param name="bossShuffle">
        /// A boolean representing whether Boss Shuffle is enabled.
        /// </param>
        /// <param name="enemyShuffle">
        /// A boolean representing whether Enemy Shuffle is enabled.
        /// </param>
        public Mode(ItemPlacement itemPlacement = ItemPlacement.Advanced,
            DungeonItemShuffle dungeonItemShuffle = DungeonItemShuffle.Standard,
            WorldState worldState = WorldState.StandardOpen, bool entranceShuffle = false,
            bool bossShuffle = false, bool enemyShuffle = false)
        {
            ItemPlacement = itemPlacement;
            DungeonItemShuffle = dungeonItemShuffle;
            WorldState = worldState;
            EntranceShuffle = entranceShuffle;
            BossShuffle = bossShuffle;
            EnemyShuffle = enemyShuffle;
        }

        /// <summary>
        /// Constructor that copies the settings of another Mode class.
        /// </summary>
        /// <param name="source">
        /// The source Mode class to be copied.
        /// </param>
        public Mode(Mode source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            ItemPlacement = source.ItemPlacement;
            DungeonItemShuffle = source.DungeonItemShuffle;
            WorldState = source.WorldState;
            EntranceShuffle = source.EntranceShuffle;
            BossShuffle = source.BossShuffle;
            EnemyShuffle = source.EnemyShuffle;
        }

        /// <summary>
        /// Raises the PropertyChanging event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changing property.
        /// </param>
        private void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
                PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(WorldState) && WorldState == Enums.WorldState.Inverted)
                ItemPlacement = Enums.ItemPlacement.Advanced;
        }

        /// <summary>
        /// Returns whether the ModeRequirement class is valid compared to the current
        /// Mode class settings.
        /// </summary>
        /// <param name="modeRequirement">
        /// The mode requirement to be checked.
        /// </param>
        /// <returns>
        /// A boolean representing whether the mode requirement is validated against
        /// the current mode settings.
        /// </returns>
        public bool Validate(ModeRequirement modeRequirement)
        {
            if (modeRequirement == null)
            {
                throw new ArgumentNullException(nameof(modeRequirement));
            }

            if (modeRequirement.ItemPlacement.HasValue &&
                ItemPlacement != modeRequirement.ItemPlacement)
            {
                return false;
            }

            if (modeRequirement.DungeonItemShuffle.HasValue)
            {
                switch (modeRequirement.DungeonItemShuffle.Value)
                {
                    case DungeonItemShuffle.Standard:
                    case DungeonItemShuffle.MapsCompasses:
                        {
                            if (DungeonItemShuffle != DungeonItemShuffle.Standard &&
                                DungeonItemShuffle != DungeonItemShuffle.MapsCompasses)
                            {
                                return false;
                            }
                        }
                        break;
                    case DungeonItemShuffle.MapsCompassesSmallKeys:
                    case DungeonItemShuffle.Keysanity:
                        {
                            if (DungeonItemShuffle < modeRequirement.DungeonItemShuffle.Value)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }

            if (modeRequirement.WorldState.HasValue)
            {
                switch (modeRequirement.WorldState.Value)
                {
                    case WorldState.StandardOpen:
                        {
                            if (WorldState != WorldState.StandardOpen &&
                                WorldState != WorldState.Retro)
                            {
                                return false;
                            }
                        }
                        break;
                    case WorldState.Inverted:
                    case WorldState.Retro:
                        {
                            if (WorldState != modeRequirement.WorldState.Value)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }

            if (modeRequirement.EntranceShuffle.HasValue &&
                EntranceShuffle != modeRequirement.EntranceShuffle)
            {
                return false;
            }

            if (modeRequirement.BossShuffle.HasValue &&
                BossShuffle != modeRequirement.BossShuffle)
            {
                return false;
            }

            if (modeRequirement.EnemyShuffle.HasValue &&
                EnemyShuffle != modeRequirement.EnemyShuffle)
            {
                return false;
            }

            return true;
        }
    }
}