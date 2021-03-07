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
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using OpenTracker.Models.SequenceBreaks;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This class contains logic managing saving and loading game data.
    /// </summary>
    public class SaveLoadManager : ISaveLoadManager
    {
        private readonly IMode _mode;
        private readonly IItemDictionary _items;
        private readonly ILocationDictionary _locations;
        private readonly IBossPlacementDictionary _bossPlacements;
        private readonly IPrizePlacementDictionary _prizePlacements;
        private readonly IConnectionCollection _connections;
        private readonly IDropdownDictionary _dropdowns;
        private readonly IPinnedLocationCollection _pinnedLocations;
        private readonly ISequenceBreakDictionary _sequenceBreaks;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _currentFilename;
        public string? CurrentFilePath
        {
            get => _currentFilename;
            private set
            {
                if (_currentFilename == value)
                {
                    return;
                }
                
                _currentFilename = value;
                _unsaved = false;
                OnPropertyChanged(nameof(CurrentFilePath));
            }
        }

        private bool _unsaved;
        public bool Unsaved
        {
            get => _unsaved;
            set
            {
                if (_unsaved == value)
                {
                    return;
                }
                
                _unsaved = value;
                OnPropertyChanged(nameof(Unsaved));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="items">
        /// The item dictionary.
        /// </param>
        /// <param name="locations">
        /// The location dictionary.
        /// </param>
        /// <param name="bossPlacements">
        /// The boss placement dictionary.
        /// </param>
        /// <param name="prizePlacements">
        /// The prize placement dictionary.
        /// </param>
        /// <param name="connections">
        /// The connection collection.
        /// </param>
        /// <param name="dropdowns">
        /// The dropdown dictionary.
        /// </param>
        /// <param name="pinnedLocations">
        /// The pinned location collection.
        /// </param>
        /// <param name="sequenceBreaks">
        /// The sequence break dictionary.
        /// </param>
        public SaveLoadManager(
            IMode mode, IItemDictionary items, ILocationDictionary locations, IBossPlacementDictionary bossPlacements,
            IPrizePlacementDictionary prizePlacements, IConnectionCollection connections, IDropdownDictionary dropdowns,
            IPinnedLocationCollection pinnedLocations, ISequenceBreakDictionary sequenceBreaks)
        {
            _mode = mode;
            _items = items;
            _locations = locations;
            _bossPlacements = bossPlacements;
            _prizePlacements = prizePlacements;
            _connections = connections;
            _dropdowns = dropdowns;
            _pinnedLocations = pinnedLocations;
            _sequenceBreaks = sequenceBreaks;
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
                Items = _items.Save(),
                Locations = _locations.Save(),
                BossPlacements = _bossPlacements.Save(),
                PrizePlacements = _prizePlacements.Save(),
                Connections = _connections.Save(),
                Dropdowns = _dropdowns.Save(),
                PinnedLocations = _pinnedLocations.Save()
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
            _items.Load(saveData.Items!);
            _locations.Load(saveData.Locations!);
            _bossPlacements.Load(saveData.BossPlacements!);
            _prizePlacements.Load(saveData.PrizePlacements!);
            _connections.Load(saveData.Connections!);
            _dropdowns.Load(saveData.Dropdowns!);
            _pinnedLocations.Load(saveData.PinnedLocations!);
        }

        /// <summary>
        /// Loads the game data from the specified file path.
        /// </summary>
        /// <param name="path">
        /// A string representing the file path.
        /// </param>
        public void Open(string path)
        {
            var saveData = JsonConversion.Load<SaveData>(path) ?? throw new NullReferenceException();
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

        /// <summary>
        /// Loads the sequence break data from the specified file path.
        /// </summary>
        /// <param name="path">
        /// A string representing the file path.
        /// </param>
        public void OpenSequenceBreaks(string path)
        {
            _sequenceBreaks.Load(JsonConversion.Load<Dictionary<SequenceBreakType, SequenceBreakSaveData>>(path));
        }

        /// <summary>
        /// Saves the sequence break data to the specified file path.
        /// </summary>
        /// <param name="path">
        /// A string representing the file path.
        /// </param>
        public void SaveSequenceBreaks(string path)
        {
            JsonConversion.Save(_sequenceBreaks.Save(), path);
        }
    }
}
