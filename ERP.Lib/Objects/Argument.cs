using ERP.BaseLib.Serialization;

namespace ERP.BaseLib.Objects
{
    /// <summary>
    /// A argument is needed to pass to a <see cref="DataInput"/>.
    /// <para />
    /// </summary>
    public sealed class Argument
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">Name of the Argument</param>
        /// <param name="Value">The Serialized Value of argument</param>
        public Argument(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">Name of the Argument</ param>
        /// <param name="Value">Value of argument</param>
        public Argument(string Name, Object Value)
        {
            this.Name = Name;
            this.Value = Value.Serialize();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Argument()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }

        /// <summary>
        /// Name of argument
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Serialized Value of argument
        /// </summary>
        public string Value { get; set; }
    }
}