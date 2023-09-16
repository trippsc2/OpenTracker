namespace OpenTracker.Models.SaveLoad.Converters;

/// <summary>
/// This class contains the logic to convert save data between versions.
/// </summary>
public static class SaveDataConverter
{
    /// <summary>
    /// Converts any save values that have changed from previous versions to new values.
    /// </summary>
    /// <param name="saveData">
    ///     The starting <see cref="SaveData"/>.
    /// </param>
    /// <returns>
    ///     The converted <see cref="SaveData"/>.
    /// </returns>
    public static SaveData ConvertSaveData(SaveData saveData)
    {
        return saveData;
    }
}