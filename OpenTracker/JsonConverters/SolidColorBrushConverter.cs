using Avalonia.Media;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

namespace OpenTracker.JsonConverters
{
    public class SolidColorBrushConverter : JsonConverter<SolidColorBrush>
    {
        public override SolidColorBrush ReadJson(JsonReader reader, Type objectType, [AllowNull] SolidColorBrush existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return new SolidColorBrush(Color.Parse((string)reader.Value));
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] SolidColorBrush value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Color.ToString());
        }
    }
}
