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

        public bool MapCompassShuffle =>
            DungeonItemShuffle >= DungeonItemShuffle.MapsCompasses;
        public bool SmallKeyShuffle =>
            DungeonItemShuffle >= DungeonItemShuffle.MapsCompassesSmallKeys ||
            WorldState == WorldState.Retro;
        public bool BigKeyShuffle =>
            DungeonItemShuffle >= DungeonItemShuffle.Keysanity;

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

        private DungeonItemShuffle _dungeonItemShuffle = DungeonItemShuffle.Standard;
        public DungeonItemShuffle DungeonItemShuffle
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

        private bool _entranceShuffle = false;
        public bool EntranceShuffle
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

        private bool _bossShuffle = false;
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

        private bool _enemyShuffle = false;
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
                DungeonItemShuffle = DungeonItemShuffle,
                WorldState = WorldState,
                EntranceShuffle = EntranceShuffle,
                BossShuffle = BossShuffle,
                EnemyShuffle = EnemyShuffle
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
            DungeonItemShuffle = saveData.DungeonItemShuffle;
            WorldState = saveData.WorldState;
            EntranceShuffle = saveData.EntranceShuffle;
            BossShuffle = saveData.BossShuffle;
            EnemyShuffle = saveData.EnemyShuffle;
        }
    }
}