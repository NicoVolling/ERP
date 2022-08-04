namespace ERP.Exceptions.ErpExceptions
{
    public class MissingArgumentErpException : CommandErpException
    {
        public MissingArgumentErpException(string Argumentname) : base($"Missing Argument \"{Argumentname}\"")
        {
        }

        public MissingArgumentErpException() : base()
        {
        }
    }
}