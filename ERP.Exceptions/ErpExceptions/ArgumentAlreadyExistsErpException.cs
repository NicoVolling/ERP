namespace ERP.Exceptions.ErpExceptions
{
    public class ArgumentAlreadyExistsErpException : ErpException
    {
        public ArgumentAlreadyExistsErpException(string Argumentname) : base($"Argument already exists: \"{Argumentname}\"")
        {
        }
    }
}