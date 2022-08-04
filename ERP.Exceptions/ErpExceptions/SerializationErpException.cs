namespace ERP.Exceptions.ErpExceptions
{
    public class SerializationErpException : JsonErpException
    {
        public SerializationErpException(string Message) : base($"Serialization: {Message}")
        {
        }

        public SerializationErpException() : base()
        {
        }
    }
}