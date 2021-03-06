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

        /// <summary>
        /// Loads the sequence break data from the specified file path.
        /// </summary>
        /// <param name="path">
        /// A string representing the file path.
        /// </param>
        void OpenSequenceBreaks(string path);

        /// <summary>
        /// Saves the sequence break data to the specified file path.
        /// </summary>
        /// <param name="path">
        /// A string representing the file path.
        /// </param>
        void SaveSequenceBreaks(string path);
    }
}