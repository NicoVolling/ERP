using ERP.BaseLib.Serialization;
using ERP.BaseLib.Serialization.Converters;
using ERP.Exceptions.ErpExceptions;
using Newtonsoft.Json;
using System.Collections;

namespace ERP.BaseLib.Objects
{
    /// <summary>
    /// A collection of arguments is needed in <see cref="DataInput"/>
    /// </summary>
    [JsonConverter(typeof(ArgumentCollectionConverter))]
    public class ArgumentCollection : IEnumerable<Argument>
    {
        private List<Argument> _arguments = new List<Argument>();

        /// <summary>
        /// Constructor
        /// </summary>
        public ArgumentCollection()
        {
        }

        internal ArgumentCollection(IEnumerable<Argument> ArgumentList)
        {
            _arguments = ArgumentList.ToList();
        }

        public static explicit operator ArgumentCollection(Dictionary<string, string> Dictionary)
        {
            return Dictionary.Select(o => new Argument(o.Key, o.Value)).ToArray();
        }

        public static explicit operator Dictionary<string, string>(ArgumentCollection ArgumentCollection)
        {
            return ArgumentCollection.ToDictionary(o => o.Name, o => o.Value);
        }

        public static implicit operator ArgumentCollection(Argument[] ArgumentArray)
        {
            return new ArgumentCollection(ArgumentArray);
        }

        /// <summary>
        /// Adds an argument to the end of the list.
        /// </summary>
        /// <param name="Argument"></param>
        /// <exception cref="Exception">Thrown when the Argument already exists</exception>
        public void Add(Argument Argument)
        {
            if (_arguments.Contains(Argument))
            {
                throw new ArgumentAlreadyExistsErpException(Argument.Name);
            }
        }

        /// <summary>
        /// Removes all arguments from the list.
        /// </summary>
        public void Clear() => _arguments.Clear();

        /// <summary>
        /// Determines wether an argument is in the list.
        /// </summary>
        /// <param name="Argument"></param>
        /// <returns>Returns true if names of any argument matches.</returns>
        public bool Contains(Argument Argument) => _arguments.Any(o => o.Name == Argument.Name);

        public IEnumerator<Argument> GetEnumerator()
        {
            return _arguments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Remove all arguments that matches the name of the given argument from the list.
        /// </summary>
        /// <param name="Argument"></param>
        public void Remove(Argument Argument) => _arguments.RemoveAll(o => o.Name == Argument.Name);

        /// <summary>
        /// Converts to a Dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ToDictionary()
        {
            return (Dictionary<string, string>)this;
        }

        public override string ToString()
        {
            return _arguments.Serialize();
        }
    }
}