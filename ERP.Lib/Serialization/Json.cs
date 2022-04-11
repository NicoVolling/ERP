using Newtonsoft.Json;

namespace ERP.BaseLib.Serialization
{
    /// <summary>
    /// This class provides functions for serialization and deserialization of objects.
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Converts the string to a object.
        /// </summary>
        /// <typeparam name="T">Type of Targetobject.</typeparam>
        /// <param name="Raw">String.</param>
        /// <returns>Targetobject.</returns>
        public static T Deserialize<T>(string Raw) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(Raw);
        }

        /// <summary>
        /// Converts the string to a object.
        /// </summary>
        /// <param name="Raw">String.</param>
        /// <param name="Type">Type of Targetobject.</param>
        /// <returns>Targetobject.</returns>
        public static Object Deserialize(string Raw, Type Type)
        {
            return JsonConvert.DeserializeObject(Raw, Type);
        }

        /// <summary>
        /// Converts the object to a string.
        /// </summary>
        /// <param name="Object">Object.</param>
        /// <returns>String.</returns>
        public static string Serialize(Object Object)
        {
            return JsonConvert.SerializeObject(Object);
        }
    }
}