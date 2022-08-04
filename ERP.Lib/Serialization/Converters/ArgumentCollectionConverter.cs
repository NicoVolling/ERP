using ERP.BaseLib.Objects;
using Newtonsoft.Json;

namespace ERP.BaseLib.Serialization.Converters
{
    /// <summary>
    /// A custom converter for <see cref="ArgumentCollection"/>
    /// </summary>
    public class ArgumentCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ArgumentCollection);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new ArgumentCollection(Json.Deserialize<List<Argument>>(reader.Value.ToString()));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ArgumentCollection AC = (ArgumentCollection)value;

            writer.WriteValue(AC.ToArray().Serialize());
        }
    }
}