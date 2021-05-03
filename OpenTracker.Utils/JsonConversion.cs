using Newtonsoft.Json;
using System.IO;

namespace OpenTracker.Utils
{
    /// <summary>
    ///     This class contains the logic for converting objects to and from JSON.
    /// </summary>
    public class JsonConverter : IJsonConverter
    {
        public void Save<T>(T saveData, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            var json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public T Load<T>(string path)
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
