﻿using System;
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
        /// Is used to identify a command.
        /// </summary>
        public Command Command { get; set; }

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
        /// <param name="Command">Is used to identify a command.</param>
        public DataInput(Command Command)
        {
            this.Command = Command;
            this.Arguments = new ArgumentCollection();
            this.User = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="User">The User is needed for accessing protected commands.</param>
        /// <param name="Command">Is used to identify a command.</param>
        public DataInput(User User, Command Command)
        {
            this.Command = Command;
            this.Arguments = new ArgumentCollection();
            this.User = User;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Command">Is used to identify a command.</param>
        /// <param name="Arguments">This is a list of all Arguments (Parameters) that are needed for executing the command.</param>
        public DataInput(Command Command, ArgumentCollection Arguments) 
        {
            this.Command = Command;
            this.Arguments = Arguments;
            this.User = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="User">The User is needed for accessing protected commands.</param>
        /// <param name="Command">Is used to identify a command.</param>
        /// <param name="Arguments">This is a list of all Arguments (Parameters) that are needed for executing the command.</param>
        public DataInput(User User, Command Command, ArgumentCollection Arguments)
        {
            this.Command = Command;
            this.Arguments = Arguments;
            this.User = User;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Command">Is used to identify a command.</param>
        /// <param name="Arguments">This is a list of all Arguments (Parameters) that are needed for executing the command.</param>
        public DataInput(Command Command, params Argument[] Arguments) 
        {
            this.Command = Command;
            this.Arguments = Arguments;
            this.User = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="User">The User is needed for accessing protected commands.</param>
        /// <param name="Command">Is used to identify a command.</param>
        /// <param name="Arguments">This is a list of all Arguments (Parameters) that are needed for executing the command.</param>
        public DataInput(User User, Command Command, params Argument[] Arguments)
        {
            this.Command = Command;
            this.Arguments = Arguments;
            this.User = User;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DataInput() 
        {
            this.Command = default(Command);
            this.Arguments = new ArgumentCollection();
            this.User = null;
        }
    }
}
