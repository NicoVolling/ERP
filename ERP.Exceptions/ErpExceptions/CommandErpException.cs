namespace ERP.Exceptions.ErpExceptions
{
    public class CommandErpException : ErpException
    {
        public CommandErpException(string Message) : base($"Command Error: {Message}")
        {
        }

        public CommandErpException() : base("Command Error")
        {
        }
    }
}