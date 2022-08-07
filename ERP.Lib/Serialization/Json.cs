using ERP.BaseLib.Serialization.Converters;
using ERP.Exceptions.ErpExceptions;
using Newtonsoft.Json;
using System.Reflection;

namespace ERP.BaseLib.Serialization
{
    /// <summary>
    /// This class provides functions for serialization and deserialization of objects.
    /// </summary>
    public static class Json
    {
        private static JsonSerializerSettings Settings;

        static Json()
        {
            Settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>()
                {
                    new ArgumentCollectionConverter(),
                    new DateOnlyConverter(),
                    new TimeOnlyConverter()
                }
            };
        }

        ///<summary>
        ///This method applies changes of one object to another object of the same type.
        ///Throws TypeMismatchErpException if the types of the objects do not match.
        ///</summary>
        ///<param name= "Object">Object to which the changes should be applied.</param>
        ///<param name= "ChangedObject">Object whose changes should be applied.</param>
        ///<exception cref= "TypeMismatchErpException">if the types of the objects do not match.</exception>
        public static void ApplyChanges(this Object Object, Object ChangedObject)
        {
            if (ChangedObject.GetType() != Object.GetType())
            {
                throw new TypeMismatchErpException(Object.GetType(), ChangedObject.GetType());
            }

            foreach (PropertyInfo Property in Object.GetType().GetProperties())
            {
                if (Property.CanRead && Property.CanWrite && Property.GetGetMethod() is MethodInfo && Property.GetSetMethod() is MethodInfo)
                {
                    Property.SetValue(Object, Property.GetValue(ChangedObject));
                }
            }
        }

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

        ///<summary>
        ///Deserializes the given Raw string into an object of type T,
        ///then applies the changes of that object to the original object.
        ///</summary>
        ///<param name= "Object">The original object to which the changes should be applied.</param>
        ///<param name= "Raw">A string containing the serialized object with the changes.</param>
        public static void DeserializeAndApplyChanges(this Object Object, string Raw)
        {
            Object BO = Json.Deserialize(Raw, Object.GetType());
            Object.ApplyChanges(BO);
        }

        /// <summary>
        /// Converts the object to a string.
        /// </summary>
        /// <param name="Object">Object.</param>
        /// <returns>String.</returns>
        public static string Serialize(this Object Object)
        {
            return JsonConvert.SerializeObject(Object, Formatting.Indented, Settings);
        }
    }
}