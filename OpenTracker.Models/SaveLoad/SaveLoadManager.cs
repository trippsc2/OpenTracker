using OpenTracker.Models.Utils;
using OpenTracker.Utils;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the class for managing saving and loading game data.
    /// </summary>
    public class SaveLoadManager : Singleton<SaveLoadManager>, ISaveLoadManager
    {
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
        /// Loads the game data from the specified file path.
        /// </summary>
        /// <param name="path">
        /// A string representing the file path.
        /// </param>
        public void Open(string path)
        {
            var saveData = JsonConversion.Load<SaveData>(path) ??
                throw new NullReferenceException();
            saveData.Load();

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
            var saveData = new SaveData();
            JsonConversion.Save(saveData, path);

            CurrentFilePath = path;
        }
    }
}
