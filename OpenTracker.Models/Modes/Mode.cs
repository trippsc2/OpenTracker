using System.ComponentModel;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.Modes
{
    /// <summary>
    /// This class contains game mode settings data.
    /// </summary>
    public class Mode : ReactiveObject, IMode
    {
        private ItemPlacement _itemPlacement = ItemPlacement.Advanced;
        public ItemPlacement ItemPlacement
        {
            get => _itemPlacement;
            set => this.RaiseAndSetIfChanged(ref _itemPlacement, value);
        }

        private bool _mapShuffle;
        public bool MapShuffle
        {
            get => _mapShuffle;
            set => this.RaiseAndSetIfChanged(ref _mapShuffle, value);
        }

        private bool _compassShuffle;
        public bool CompassShuffle
        {
            get => _compassShuffle;
            set => this.RaiseAndSetIfChanged(ref _compassShuffle, value);
        }

        private bool _smallKeyShuffle;
        public bool SmallKeyShuffle
        {
            get => _smallKeyShuffle;
            set => this.RaiseAndSetIfChanged(ref _smallKeyShuffle, value);
        }

        private bool _bigKeyShuffle;
        public bool BigKeyShuffle
        {
            get => _bigKeyShuffle;
            set => this.RaiseAndSetIfChanged(ref _bigKeyShuffle, value);
        }

        private WorldState _worldState = WorldState.StandardOpen;
        public WorldState WorldState
        {
            get => _worldState;
            set => this.RaiseAndSetIfChanged(ref _worldState, value);
        }

        private EntranceShuffle _entranceShuffle;
        public EntranceShuffle EntranceShuffle
        {
            get => _entranceShuffle;
            set => this.RaiseAndSetIfChanged(ref _entranceShuffle, value);
        }

        private bool _bossShuffle;
        public bool BossShuffle
        {
            get => _bossShuffle;
            set => this.RaiseAndSetIfChanged(ref _bossShuffle, value);
        }

        private bool _enemyShuffle;
        public bool EnemyShuffle
        {
            get => _enemyShuffle;
            set => this.RaiseAndSetIfChanged(ref _enemyShuffle, value);
        }

        private bool _guaranteedBossItems;
        public bool GuaranteedBossItems
        {
            get => _guaranteedBossItems;
            set => this.RaiseAndSetIfChanged(ref _guaranteedBossItems, value);
        }

        private bool _genericKeys;
        public bool GenericKeys
        {
            get => _genericKeys;
            set => this.RaiseAndSetIfChanged(ref _genericKeys, value);
        }

        private bool _takeAnyLocations;
        public bool TakeAnyLocations
        {
            get => _takeAnyLocations;
            set => this.RaiseAndSetIfChanged(ref _takeAnyLocations, value);
        }

        private bool _keyDropShuffle;
        public bool KeyDropShuffle
        {
            get => _keyDropShuffle;
            set => this.RaiseAndSetIfChanged(ref _keyDropShuffle, value);
        }

        private bool _shopShuffle;
        public bool ShopShuffle
        {
            get => _shopShuffle;
            set => this.RaiseAndSetIfChanged(ref _shopShuffle, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Mode()
        {
            PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(WorldState) when WorldState == WorldState.Inverted:
                    ItemPlacement = ItemPlacement.Advanced;
                    break;
                case nameof(GenericKeys) when GenericKeys:
                    SmallKeyShuffle = true;
                    break;
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
                TakeAnyLocations = TakeAnyLocations,
                KeyDropShuffle = KeyDropShuffle,
                ShopShuffle = ShopShuffle
            };
        }

        /// <summary>
        /// Loads the save data to this mode instance.
        /// </summary>
        /// <param name="saveData">
        /// The save data to be loaded.
        /// </param>
        public void Load(ModeSaveData? saveData)
        {
            if (saveData == null)
            {
                return;
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
            KeyDropShuffle = saveData.KeyDropShuffle;
            ShopShuffle = saveData.ShopShuffle;
        }
    }
}