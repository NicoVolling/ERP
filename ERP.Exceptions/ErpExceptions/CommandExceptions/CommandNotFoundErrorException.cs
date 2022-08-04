namespace ERP.Exceptions.ErpExceptions.CommandExceptions
{
    public class CommandNotFoundErrorException : CommandErpException
    {
        public CommandNotFoundErrorException(string Command) : base($"Could not find: \"{Command}\"")
        {
        }

        public CommandNotFoundErrorException() : base("Could not find")
        {
        }
    }
}