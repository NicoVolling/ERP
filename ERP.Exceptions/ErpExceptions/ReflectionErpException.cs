namespace ERP.Exceptions.ErpExceptions
{
    public class ReflectionErpException : ErpException
    {
        public ReflectionErpException(string Message) : base($"Reflection error: {Message}")
        {
        }

        public ReflectionErpException() : base("Reflection error")
        {
        }
    }
}