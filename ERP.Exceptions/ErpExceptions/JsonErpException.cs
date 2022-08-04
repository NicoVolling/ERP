namespace ERP.Exceptions.ErpExceptions
{
    public class JsonErpException : ErpException
    {
        public JsonErpException(string Message) : base($"Json: {Message}")
        {
        }

        public JsonErpException() : base()
        {
        }
    }
}