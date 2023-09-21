namespace OpenTracker.Utils;

/// <summary>
///     This interface contains the logic for converting objects to and from JSON.
/// </summary>
public interface IJsonConverter
{
    /// <summary>
    ///     Saves the provided save data as a JSON file at the specified file path.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the save data.
    /// </typeparam>
    /// <param name="saveData">
    ///     The save data to be saved.
    /// </param>
    /// <param name="path">
    ///     The file path to which the data is to be saved.
    /// </param>
    void Save<T>(T saveData, string path);

    /// <summary>
    ///     Returns save data from a JSON file at the specified file location.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the save data.
    /// </typeparam>
    /// <param name="path">
    ///     The file path to which the data is to be saved.
    /// </param>
    /// <returns>
    ///     The save data.
    /// </returns>
    T Load<T>(string path);
}