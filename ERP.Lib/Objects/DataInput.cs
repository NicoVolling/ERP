namespace ERP.BaseLib.Objects
{
    /// <summary>
    /// This class provides several Data needed to send commands to the server and executes them.
    /// </summary>
    public sealed class DataInput
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Command">Is used to identify a command.</param>
        public DataInput(Command Command)
        {
            this.Command = Command;
            this.Arguments = new ArgumentCollection();
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
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DataInput()
        {
            this.Command = default;
            this.Arguments = new ArgumentCollection();
        }

        /// <summary>
        /// This is a list of all Arguments (Parameters) that are needed for executing the command.
        /// </summary>
        public ArgumentCollection Arguments { get; set; }

        /// <summary>
        /// Is used to identify a command.
        /// </summary>
        public Command Command { get; set; }

        public override string ToString()
        {
            return $"{Command}";
        }
    }
}