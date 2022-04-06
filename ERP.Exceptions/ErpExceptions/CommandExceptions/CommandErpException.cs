using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
