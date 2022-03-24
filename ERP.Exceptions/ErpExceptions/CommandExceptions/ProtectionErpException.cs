using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
