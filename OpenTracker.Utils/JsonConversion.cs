using Newtonsoft.Json;
using System.IO;

namespace OpenTracker.Utils
{
    /// <summary>
    /// This is the class for converting objects to JSON files.
    /// </summary>
    public static class JsonConversion
    {
        /// <summary>
        /// Saves the provided save data as a JSON file at the specified file path.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the save data.
        /// </typeparam>
        /// <param name="saveData">
        /// The save data to be saved.
        /// </param>
        /// <param name="path">
        /// The file path to which the data is to be saved.
        /// </param>
        public static void Save<T>(T saveData, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            var json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Returns save data from a JSON file at the specified file location.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the save data.
        /// </typeparam>
        /// <param name="path">
        /// The file path to which the data is to be saved.
        /// </param>
        /// <returns>
        /// The save data.
        /// </returns>
        public static T Load<T>(string path)
        {
            if (!File.Exists(path))
            {
                return default!;
            }

            var content = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
