using Newtonsoft.Json;

namespace ERP.BaseLib.Serialization.Converters
{
    /// <summary>
    /// A custom converter for <see cref="DateOnly"/>
    /// </summary>
    public class DateOnlyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateOnly);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateOnly.Parse(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateOnly)value).ToString());
        }
    }
}