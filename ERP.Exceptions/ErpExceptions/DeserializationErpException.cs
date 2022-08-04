namespace ERP.Exceptions.ErpExceptions
{
    public class DeserializationErpException : JsonErpException
    {
        public DeserializationErpException(string Message) : base($"Deserialization: {Message}")
        {
        }

        public DeserializationErpException() : base()
        {
        }
    }
}