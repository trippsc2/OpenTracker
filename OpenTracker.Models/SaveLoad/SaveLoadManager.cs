using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.Modes;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.SaveLoad.Converters;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    ///     This class contains logic managing saving and loading game data.
    /// </summary>
    public class SaveLoadManager : ReactiveObject, ISaveLoadManager
    {
        private readonly IJsonConverter _jsonConverter;
        
        private readonly IMode _mode;
        private readonly IItemDictionary _items;
        private readonly ILocationDictionary _locations;
        private readonly IBossPlacementDictionary _bossPlacements;
        private readonly IPrizePlacementDictionary _prizePlacements;
        private readonly Lazy<IMapConnectionCollection> _connections;
        private readonly IDropdownDictionary _dropdowns;
        private readonly IPinnedLocationCollection _pinnedLocations;
        private readonly ISequenceBreakDictionary _sequenceBreaks;
        
        private string? _currentFilePath;
        public string? CurrentFilePath
        {
            get => _currentFilePath;
            private set => this.RaiseAndSetIfChanged(ref _currentFilePath, value);
        }

        private bool _unsaved;
        public bool Unsaved
        {
            get => _unsaved;
            set => this.RaiseAndSetIfChanged(ref _unsaved, value);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="jsonConversion">
        ///     The JSON converter.
        /// </param>
        /// <param name="mode">
        ///     The mode settings.
        /// </param>
        /// <param name="items">
        ///     The item dictionary.
        /// </param>
        /// <param name="locations">
        ///     The location dictionary.
        /// </param>
        /// <param name="bossPlacements">
        ///     The boss placement dictionary.
        /// </param>
        /// <param name="prizePlacements">
        ///     The prize placement dictionary.
        /// </param>
        /// <param name="connections">
        ///     The connection collection.
        /// </param>
        /// <param name="dropdowns">
        ///     The dropdown dictionary.
        /// </param>
        /// <param name="pinnedLocations">
        ///     The pinned location collection.
        /// </param>
        /// <param name="sequenceBreaks">
        ///     The sequence break dictionary.
        /// </param>
        public SaveLoadManager(
            IJsonConverter jsonConversion, IMode mode, IItemDictionary items, ILocationDictionary locations,
            IBossPlacementDictionary bossPlacements, IPrizePlacementDictionary prizePlacements,
            IMapConnectionCollection.Factory connections, IDropdownDictionary dropdowns,
            IPinnedLocationCollection pinnedLocations, ISequenceBreakDictionary sequenceBreaks)
        {
            _mode = mode;
            _items = items;
            _locations = locations;
            _bossPlacements = bossPlacements;
            _prizePlacements = prizePlacements;
            _connections = new Lazy<IMapConnectionCollection>(() => connections());
            _dropdowns = dropdowns;
            _pinnedLocations = pinnedLocations;
            _sequenceBreaks = sequenceBreaks;
            _jsonConverter = jsonConversion;

            PropertyChanged += OnPropertyChanged;
        }

        public void Open(string path)
        {
            var saveData = _jsonConverter.Load<SaveData>(path) ?? throw new NullReferenceException();
            LoadSaveData(saveData);

            CurrentFilePath = path;
        }

        public void Save(string path)
        {
            var saveData = GetSaveData();
            _jsonConverter.Save(saveData, path);

            CurrentFilePath = path;
        }

        public void OpenSequenceBreaks(string path)
        {
            _sequenceBreaks.Load(_jsonConverter.Load<Dictionary<SequenceBreakType, SequenceBreakSaveData>>(path));
        }

        public void SaveSequenceBreaks(string path)
        {
            _jsonConverter.Save(_sequenceBreaks.Save(), path);
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentFilePath))
            {
                Unsaved = false;
            }
        }

        /// <summary>
        ///     Gets saved data from the tracker.
        /// </summary>
        /// <returns>
        ///     Saved data from the tracker.
        /// </returns>
        private SaveData GetSaveData()
        {
            return new()
            {
                Version = Assembly.GetExecutingAssembly().GetName().Version,
                Mode = _mode.Save(),
                Items = _items.Save(),
                Locations = _locations.Save(),
                BossPlacements = _bossPlacements.Save(),
                PrizePlacements = _prizePlacements.Save(),
                Connections = _connections.Value.Save(),
                Dropdowns = _dropdowns.Save(),
                PinnedLocations = _pinnedLocations.Save()
            };
        }

        /// <summary>
        ///     Loads save data into the tracker.
        /// </summary>
        /// <param name="saveData">
        ///     The save data to be loaded.
        /// </param>
        private void LoadSaveData(SaveData saveData)
        {
            saveData = SaveDataConverter.ConvertSaveData(saveData);

            _mode.Load(saveData.Mode);
            _items.Load(saveData.Items);
            _locations.Load(saveData.Locations);
            _bossPlacements.Load(saveData.BossPlacements);
            _prizePlacements.Load(saveData.PrizePlacements);
            _connections.Value.Load(saveData.Connections);
            _dropdowns.Load(saveData.Dropdowns);
            _pinnedLocations.Load(saveData.PinnedLocations);
        }
    }
}
