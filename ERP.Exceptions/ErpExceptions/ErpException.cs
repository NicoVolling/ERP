namespace ERP.Exceptions.ErpExceptions
{
    public class ErpException : Exception
    {
        public ErpException(string Message) : base(Message)
        {
        }

        public ErpException() : base()
        {
        }
    }
}