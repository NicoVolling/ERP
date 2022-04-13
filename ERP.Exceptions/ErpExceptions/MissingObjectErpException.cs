namespace ERP.Exceptions.ErpExceptions
{
    public class MissingObjectErpException : ErpException
    {
        public MissingObjectErpException(Type Type, Guid ID) : base($"Object of type \"{Type.Name}\" not found: ID = {ID}")
        {
        }

        public MissingObjectErpException() : base("Object not found")
        {
        }
    }
}