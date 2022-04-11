namespace ERP.Exceptions.ErpExceptions
{
    public class TypeMismatchErpException : ErpException
    {
        public TypeMismatchErpException(Type Mustbe, Type Current) : base($"Type mismatch: {Current.Name} is not {Mustbe.Name}")
        {
        }

        public TypeMismatchErpException() : base("Type mismatch")
        {
        }
    }
}