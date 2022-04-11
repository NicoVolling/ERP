namespace ERP.Exceptions.ErpExceptions
{
    public class IDGenerationErpException : ErpException
    {
        public IDGenerationErpException(Type Type) : base($"ID Generation for Type {Type.Name} failed")
        {
        }

        public IDGenerationErpException() : base("ID Generation failed")
        {
        }
    }
}