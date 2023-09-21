using System.ComponentModel;

namespace OpenTracker.Models.SaveLoad;

/// <summary>
/// This interface contains logic managing saving and loading game data.
/// </summary>
public interface ISaveLoadManager : INotifyPropertyChanged
{
    /// <summary>
    /// A <see cref="string"/> representing the current file name.
    /// </summary>
    string? CurrentFilePath { get; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether changes are unsaved.
    /// </summary>
    bool Unsaved { get; set; }

    /// <summary>
    /// Loads the game data from the specified file path.
    /// </summary>
    /// <param name="path">
    ///     A <see cref="string"/> representing the file path.
    /// </param>
    void Open(string path);

    /// <summary>
    /// Saves the game data to the specified file path.
    /// </summary>
    /// <param name="path">
    ///     A <see cref="string"/> representing the file path.
    /// </param>
    void Save(string path);

    /// <summary>
    /// Loads the sequence break data from the specified file path.
    /// </summary>
    /// <param name="path">
    ///     A <see cref="string"/> representing the file path.
    /// </param>
    void OpenSequenceBreaks(string path);

    /// <summary>
    /// Saves the sequence break data to the specified file path.
    /// </summary>
    /// <param name="path">
    ///     A <see cref="string"/> representing the file path.
    /// </param>
    void SaveSequenceBreaks(string path);
}