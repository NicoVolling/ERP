using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class ArgumentAlreadyExistsErpException : ErpException
    {
        public ArgumentAlreadyExistsErpException(string Argumentname) : base($"Argument already exists: \"{Argumentname}\"")
        {
        }
    }
}
