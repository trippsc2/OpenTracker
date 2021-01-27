using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Modes
{
    /// <summary>
    /// This is the class for the game mode settings.
    /// </summary>
    public class Mode : Singleton<Mode>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ItemPlacement _itemPlacement = ItemPlacement.Advanced;
        public ItemPlacement ItemPlacement
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

        private bool _mapShuffle;
        public bool MapShuffle
        {
            get => _mapShuffle;
            set
            {
                if (_mapShuffle != value)
                {
                    _mapShuffle = value;
                    OnPropertyChanged(nameof(MapShuffle));
                }
            }
        }

        private bool _compassShuffle;
        public bool CompassShuffle
        {
            get => _compassShuffle;
            set
            {
                if (_compassShuffle != value)
                {
                    _compassShuffle = value;
                    OnPropertyChanged(nameof(CompassShuffle));
                }
            }
        }

        private bool _smallKeyShuffle;
        public bool SmallKeyShuffle
        {
            get => _smallKeyShuffle;
            set
            {
                if (_smallKeyShuffle != value)
                {
                    _smallKeyShuffle = value;
                    OnPropertyChanged(nameof(SmallKeyShuffle));
                }
            }
        }

        private bool _bigKeyShuffle;
        public bool BigKeyShuffle
        {
            get => _bigKeyShuffle;
            set
            {
                if (_bigKeyShuffle != value)
                {
                    _bigKeyShuffle = value;
                    OnPropertyChanged(nameof(BigKeyShuffle));
                }
            }
        }

        private WorldState _worldState = WorldState.StandardOpen;
        public WorldState WorldState
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

        private EntranceShuffle _entranceShuffle;
        public EntranceShuffle EntranceShuffle
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

        private bool _bossShuffle;
        public bool BossShuffle
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

        private bool _enemyShuffle;
        public bool EnemyShuffle
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

        private bool _guaranteedBossItems;
        public bool GuaranteedBossItems
        {
            get => _guaranteedBossItems;
            set
            {
                if (_guaranteedBossItems != value)
                {
                    _guaranteedBossItems = value;
                    OnPropertyChanged(nameof(GuaranteedBossItems));
                }
            }
        }

        private bool _genericKeys;
        public bool GenericKeys
        {
            get => _genericKeys;
            set
            {
                if (_genericKeys != value)
                {
                    _genericKeys = value;
                    OnPropertyChanged(nameof(GenericKeys));
                }
            }
        }

        private bool _takeAnyLocations;
        public bool TakeAnyLocations
        {
            get => _takeAnyLocations;
            set
            {
                if (_takeAnyLocations != value)
                {
                    _takeAnyLocations = value;
                    OnPropertyChanged(nameof(TakeAnyLocations));
                }
            }
        }

        private bool _keyDropShuffle;
        public bool KeyDropShuffle
        {
            get => _keyDropShuffle;
            set
            {
                if (_keyDropShuffle != value)
                {
                    _keyDropShuffle = value;
                    OnPropertyChanged(nameof(KeyDropShuffle));
                }
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(WorldState) && WorldState == WorldState.Inverted)
            {
                ItemPlacement = ItemPlacement.Advanced;
            }

            if (propertyName == nameof(GenericKeys) && GenericKeys)
            {
                SmallKeyShuffle = true;
            }
        }

        /// <summary>
        /// Returns save data for this mode instance.
        /// </summary>
        /// <returns>
        /// Save data for this mode instance.
        /// </returns>
        public ModeSaveData Save()
        {
            return new ModeSaveData()
            {
                ItemPlacement = ItemPlacement,
                MapShuffle = MapShuffle,
                CompassShuffle = CompassShuffle,
                SmallKeyShuffle = SmallKeyShuffle,
                BigKeyShuffle = BigKeyShuffle,
                WorldState = WorldState,
                EntranceShuffle = EntranceShuffle,
                BossShuffle = BossShuffle,
                EnemyShuffle = EnemyShuffle,
                GuaranteedBossItems = GuaranteedBossItems,
                GenericKeys = GenericKeys,
                TakeAnyLocations = TakeAnyLocations
            };
        }

        /// <summary>
        /// Loads the save data to this mode instance.
        /// </summary>
        /// <param name="saveData">
        /// The save data to be loaded.
        /// </param>
        public void Load(ModeSaveData saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            ItemPlacement = saveData.ItemPlacement;
            MapShuffle = saveData.MapShuffle;
            CompassShuffle = saveData.CompassShuffle;
            SmallKeyShuffle = saveData.SmallKeyShuffle;
            BigKeyShuffle = saveData.BigKeyShuffle;
            WorldState = saveData.WorldState;
            EntranceShuffle = saveData.EntranceShuffle;
            BossShuffle = saveData.BossShuffle;
            EnemyShuffle = saveData.EnemyShuffle;
            GuaranteedBossItems = saveData.GuaranteedBossItems;
            GenericKeys = saveData.GenericKeys;
            TakeAnyLocations = saveData.TakeAnyLocations;
        }
    }
}