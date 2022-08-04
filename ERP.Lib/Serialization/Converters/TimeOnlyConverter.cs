using Newtonsoft.Json;

namespace ERP.BaseLib.Serialization.Converters
{
    /// <summary>
    /// A custom converter for <see cref="DateOnly"/>
    /// </summary>
    public class TimeOnlyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeOnly);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return TimeOnly.Parse(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((TimeOnly)value).ToString());
        }
    }
}