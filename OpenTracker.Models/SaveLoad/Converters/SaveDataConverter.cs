namespace OpenTracker.Models.SaveLoad.Converters
{
    /// <summary>
    ///     This class contains the logic to convert save data between versions.
    /// </summary>
    public static class SaveDataConverter
    {
        /// <summary>
        ///     Converts any save values from previous versions to new save values.
        /// </summary>
        /// <param name="saveData">
        ///     The save data.
        /// </param>
        /// <returns>
        ///     The converted save data.
        /// </returns>
        public static SaveData ConvertSaveData(SaveData saveData)
        {
            return saveData;
        }
    }
}
