namespace ERP.Exceptions.ErpExceptions
{
    public class ParameterNotSupportedErpException : CommandErpException
    {
        public ParameterNotSupportedErpException(Type Parametertype) : base($"Parametertype is not supported: {Parametertype.Name}")
        {
        }

        public ParameterNotSupportedErpException() : base("Parametertype is not supported")
        {
        }
    }
}