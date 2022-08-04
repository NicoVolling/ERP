namespace ERP.Exceptions.ErpExceptions
{
    public class CommandExecutionErpException : CommandErpException
    {
        public CommandExecutionErpException(string Command) : base($"Could not return a result: \"{Command}\"")
        {
        }

        public CommandExecutionErpException() : base("Could not return a result")
        {
        }
    }
}