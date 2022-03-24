using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class ParameterNotSupportedErpException : CommandErpException
    {
        public ParameterNotSupportedErpException(Type Parametertype) : base($"Parametertype is not supported: {Parametertype.Name}")
        {
        }

        public ParameterNotSupportedErpException() : base("Parametertype is not supported") 
        {
        }
    }
}
