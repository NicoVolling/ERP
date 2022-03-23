using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BaseLib.Objects
{
    /// <summary>
    /// Is used to identify a command.
    /// </summary>
    public struct Command
    {
        /// <summary>
        /// The relative namespace of the command. The Namespace begins after parentnamespace (CommandCollection).
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// The name of the command.
        /// <para />
        /// Its equal to the methodname.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// The name of the commandcollection wich contains the command.
        /// <para />
        /// Without "CC_".
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Namespace">The relative namespace of the command. The Namespace begins after parentnamespace (CommandCollection).</param>
        /// <param name="Class">The name of the command. (Its equal to the methodname.)</param>
        /// <param name="Action">The name of the commandcollection wich contains the command.</param>
        public Command(string Namespace, string Class, string Action) 
        {
            this.Namespace = Namespace;
            this.Action = Action;
            this.Class = Class;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Command() 
        {
            this.Namespace = string.Empty;
            this.Action = string.Empty;
            this.Class = string.Empty;
        }
    }
}
