namespace OpenTracker.Models.SaveLoad.Converters
{
    /// <summary>
    /// This is the class for converting save data between versions.
    /// </summary>
    public static class SaveDataConverter
    {
        /// <summary>
        /// Converts any save values from previous versions to new save values.
        /// </summary>
        /// <param name="saveData">
        /// The save data.
        /// </param>
        /// <returns>
        /// The converted save data.
        /// </returns>
        public static SaveData ConvertSaveData(SaveData saveData)
        {
            if (saveData.Version == null || saveData.Version.Major < 1 ||
                (saveData.Version.Major == 1 && (saveData.Version.Minor < 4 ||
                (saveData.Version.Minor == 4 && saveData.Version.Build <= 1))))
            {
                if (saveData.Locations != null)
                {
                    foreach (var location in saveData.Locations.Values)
                    {
                        MarkingConverter.ConvertFrom141(location);
                    }
                }
            }

            return saveData;
        }
    }
}
