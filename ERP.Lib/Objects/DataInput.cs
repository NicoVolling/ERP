using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ERP.BaseLib.Objects
{
    /// <summary>
    /// This class provides several Data needed to send commands to the server and executes them.
    /// </summary>
    public sealed class DataInput
    {
        /// <summary>
        /// The relative namespace of the command. The Namespace begins after "[...].List"
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// The name of the command.
        /// <para />
        /// Its equal to the methodname.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// The name of the commandcollection wich contains the command.
        /// <para />
        /// Without "CC_".
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// The User is needed for accessing protected commands.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// This is a list of all Arguments (Parameters) that are needed for executing the command.
        /// </summary>
        public ArgumentCollection Arguments { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Namespace">The relative namespace of the command. The Namespace begins after "[...].List"</param>
        /// <param name="Class">The name of the commandcollection wich contains the command. (Without "CC_")</param>
        /// <param name="Command">The name of the command. (Its equal to the methodname)</param>
        public DataInput(string Namespace, string Class, string Command)
        {
            this.Namespace = Namespace;
            this.Command = Command;
            this.Class = Class;
            this.Arguments = new ArgumentCollection();
            this.User = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="User">The User is needed for accessing protected commands.</param>
        /// <param name="Namespace">The relative namespace of the command. The Namespace begins after "[...].List"</param>
        /// <param name="Class">The name of the commandcollection wich contains the command. (Without "CC_")</param>
        /// <param name="Command">The name of the command. (Its equal to the methodname)</param>
        public DataInput(User User, string Namespace, string Class, string Command)
        {
            this.Namespace = Namespace;
            this.Command = Command;
            this.Class = Class;
            this.Arguments = new ArgumentCollection();
            this.User = User;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Namespace">The relative namespace of the command. The Namespace begins after "[...].List"</param>
        /// <param name="Class">The name of the commandcollection wich contains the command. (Without "CC_")</param>
        /// <param name="Command">The name of the command. (Its equal to the methodname)</param>
        /// <param name="Arguments">This is a list of all Arguments (Parameters) that are needed for executing the command.</param>
        public DataInput(string Namespace, string Class, string Command, ArgumentCollection Arguments) 
        {
            this.Namespace = Namespace;
            this.Command = Command;
            this.Class = Class;
            this.Arguments = Arguments;
            this.User = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="User">The User is needed for accessing protected commands.</param>
        /// <param name="Namespace">The relative namespace of the command. The Namespace begins after "[...].List"</param>
        /// <param name="Class">The name of the commandcollection wich contains the command. (Without "CC_")</param>
        /// <param name="Command">The name of the command. (Its equal to the methodname)</param>
        /// <param name="Arguments">This is a list of all Arguments (Parameters) that are needed for executing the command.</param>
        public DataInput(User User, string Namespace, string Class, string Command, ArgumentCollection Arguments)
        {
            this.Namespace = Namespace;
            this.Command = Command;
            this.Class = Class;
            this.Arguments = Arguments;
            this.User = User;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Namespace">The relative namespace of the command. The Namespace begins after "[...].List"</param>
        /// <param name="Class">The name of the commandcollection wich contains the command. (Without "CC_")</param>
        /// <param name="Command">The name of the command. (Its equal to the methodname)</param>
        /// <param name="Arguments">This is a list of all Arguments (Parameters) that are needed for executing the command.</param>
        public DataInput(string Namespace, string Class, string Command, params Argument[] Arguments) 
        {
            this.Namespace = Namespace;
            this.Command = Command;
            this.Class = Class;
            this.Arguments = Arguments;
            this.User = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="User">The User is needed for accessing protected commands.</param>
        /// <param name="Namespace">The relative namespace of the command. The Namespace begins after "[...].List"</param>
        /// <param name="Class">The name of the commandcollection wich contains the command. (Without "CC_")</param>
        /// <param name="Command">The name of the command. (Its equal to the methodname)</param>
        /// <param name="Arguments">This is a list of all Arguments (Parameters) that are needed for executing the command.</param>
        public DataInput(User User, string Namespace, string Class, string Command, params Argument[] Arguments)
        {
            this.Namespace = Namespace;
            this.Command = Command;
            this.Class = Class;
            this.Arguments = Arguments;
            this.User = User;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DataInput() 
        {
            this.Command = string.Empty;
            this.Namespace = string.Empty;
            this.Class = string.Empty;
            this.Arguments = new ArgumentCollection();
            this.User = null;
        }
    }
}
