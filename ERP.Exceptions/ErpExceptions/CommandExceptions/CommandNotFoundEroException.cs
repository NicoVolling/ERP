namespace ERP.Exceptions.ErpExceptions.CommandExceptions
{
    public class CommandNotFoundEroException : CommandErpException
    {
        public CommandNotFoundEroException(string Command) : base($"Could not find: \"{Command}\"")
        {
        }

        public CommandNotFoundEroException() : base("Could not find")
        {
        }
    }
}