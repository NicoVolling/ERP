namespace ERP.Exceptions.ErpExceptions
{
    public class ProtectionErpException : CommandErpException
    {
        public ProtectionErpException(int PermissionLevel) : base($"Protected: Permissionlevel = {PermissionLevel}")
        {
        }

        public ProtectionErpException() : base("Protected")
        {
        }
    }
}