using Newtonsoft.Json;
using OpenTracker.Models.Utils;
using System;
using System.ComponentModel;
using System.IO;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the class for managing saving and loading game data.
    /// </summary>
    public class SaveLoadManager : Singleton<SaveLoadManager>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _currentFilename;
        public string CurrentFilePath
        {
            get => _currentFilename;
            private set
            {
                if (_currentFilename != value)
                {
                    _currentFilename = value;
                    OnPropertyChanged(nameof(CurrentFilePath));
                    Unsaved = false;
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static void Open(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            string jsonContent = File.ReadAllText(path);
            SaveData saveData = JsonConvert.DeserializeObject<SaveData>(jsonContent);
            saveData.Load();
            
            Instance.CurrentFilePath = path;
        }

        public static void Save(string path = null)
        {
            if (path == null)
            {
                if (Instance.CurrentFilePath == null)
                {
                    throw new ArgumentNullException(nameof(path));
                }

                path = Instance.CurrentFilePath;
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            SaveData saveData = new SaveData();
            saveData.Save();
            string json = JsonConvert.SerializeObject(saveData);

            File.WriteAllText(path, json);

            Instance.CurrentFilePath = path;
        }
    }
}
