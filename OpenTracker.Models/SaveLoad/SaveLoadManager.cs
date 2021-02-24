using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.SaveLoad.Converters;
using OpenTracker.Utils;
using System;
using System.ComponentModel;
using System.Reflection;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the class for managing saving and loading game data.
    /// </summary>
    public class SaveLoadManager : ISaveLoadManager
    {
        private readonly IMode _mode;
        private readonly IItemDictionary _itemDictionary;
        private readonly ILocationDictionary _locationDictionary;
        private readonly IBossPlacementDictionary _bossPlacementDictionary;
        private readonly IPrizePlacementDictionary _prizePlacementDictionary;
        private readonly IConnectionCollection _connectionCollection;
        private readonly IDropdownDictionary _dropdownDictionary;
        private readonly IPinnedLocationCollection _pinnedLocationCollection;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _currentFilename;
        public string? CurrentFilePath
        {
            get => _currentFilename;
            private set
            {
                if (_currentFilename != value)
                {
                    _currentFilename = value;
                    _unsaved = false;
                    OnPropertyChanged(nameof(CurrentFilePath));
                }
            }
        }

        private bool _unsaved;
        public bool Unsaved
        {
            get => _unsaved;
            set
            {
                if (_unsaved != value)
                {
                    _unsaved = value;
                    OnPropertyChanged(nameof(Unsaved));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="itemDictionary">
        /// The item dictionary.
        /// </param>
        /// <param name="locationDictionary">
        /// The location dictionary.
        /// </param>
        /// <param name="bossPlacementDictionary">
        /// The boss placement dictionary.
        /// </param>
        /// <param name="prizePlacementDictionary">
        /// The prize placement dictionary.
        /// </param>
        /// <param name="connectionCollection">
        /// The connection collection.
        /// </param>
        /// <param name="dropdownDictionary">
        /// The dropdown dictionary.
        /// </param>
        /// <param name="pinnedLocationCollection">
        /// The pinned location collection.
        /// </param>
        public SaveLoadManager(
            IMode mode, IItemDictionary itemDictionary, ILocationDictionary locationDictionary,
            IBossPlacementDictionary bossPlacementDictionary,
            IPrizePlacementDictionary prizePlacementDictionary,
            IConnectionCollection connectionCollection,
            IDropdownDictionary dropdownDictionary,
            IPinnedLocationCollection pinnedLocationCollection)
        {
            _mode = mode;
            _itemDictionary = itemDictionary;
            _locationDictionary = locationDictionary;
            _bossPlacementDictionary = bossPlacementDictionary;
            _prizePlacementDictionary = prizePlacementDictionary;
            _connectionCollection = connectionCollection;
            _dropdownDictionary = dropdownDictionary;
            _pinnedLocationCollection = pinnedLocationCollection;
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
        }

        /// <summary>
        /// Gets saved data from the tracker.
        /// </summary>
        /// <returns>
        /// Saved data from the tracker.
        /// </returns>
        private SaveData GetSaveData()
        {
            return new SaveData
            {
                Version = Assembly.GetExecutingAssembly().GetName().Version,
                Mode = _mode.Save(),
                Items = _itemDictionary.Save(),
                Locations = _locationDictionary.Save(),
                BossPlacements = _bossPlacementDictionary.Save(),
                PrizePlacements = _prizePlacementDictionary.Save(),
                Connections = _connectionCollection.Save(),
                Dropdowns = _dropdownDictionary.Save(),
                PinnedLocations = _pinnedLocationCollection.Save()
            };
        }

        /// <summary>
        /// Loads save data into the tracker.
        /// </summary>
        /// <param name="saveData">
        /// The save data to be loaded.
        /// </param>
        private void LoadSaveData(SaveData saveData)
        {
            saveData = SaveDataConverter.ConvertSaveData(saveData);

            _mode.Load(saveData.Mode);
            _itemDictionary.Load(saveData.Items!);
            _locationDictionary.Load(saveData.Locations!);
            _bossPlacementDictionary.Load(saveData.BossPlacements!);
            _prizePlacementDictionary.Load(saveData.PrizePlacements!);
            _connectionCollection.Load(saveData.Connections!);
            _dropdownDictionary.Load(saveData.Dropdowns!);
            _pinnedLocationCollection.Load(saveData.PinnedLocations!);
        }

        /// <summary>
        /// Loads the game data from the specified file path.
        /// </summary>
        /// <param name="path">
        /// A string representing the file path.
        /// </param>
        public void Open(string path)
        {
            var saveData = JsonConversion.Load<SaveData>(path) ??
                throw new NullReferenceException();
            LoadSaveData(saveData);

            CurrentFilePath = path;
        }

        /// <summary>
        /// Saves the game data to the specified file path.
        /// </summary>
        /// <param name="path">
        /// A string representing the file path.
        /// </param>
        public void Save(string path)
        {
            var saveData = GetSaveData();
            JsonConversion.Save(saveData, path);

            CurrentFilePath = path;
        }
    }
}
