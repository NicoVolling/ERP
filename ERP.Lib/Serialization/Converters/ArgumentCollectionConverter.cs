using ERP.BaseLib.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BaseLib.Serialization.Converters
{
    /// <summary>
    /// A custom converter for <see cref="ArgumentCollection"/>
    /// </summary>
    internal class ArgumentCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return new ArgumentCollection(Json.Deserialize<List<Argument>>(reader.Value.ToString()));
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            ArgumentCollection AC = (ArgumentCollection)value;

            writer.WriteValue(Json.Serialize(AC.ToArray()));
        }
    }
}
