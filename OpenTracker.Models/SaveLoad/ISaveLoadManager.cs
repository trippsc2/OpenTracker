using System.ComponentModel;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This interface contains logic managing saving and loading game data.
    /// </summary>
    public interface ISaveLoadManager : INotifyPropertyChanged
    {
        string? CurrentFilePath { get; }
        bool Unsaved { get; set; }

        void Open(string path);
        void Save(string path);
    }
}