using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class TypeMismatchErpException : ErpException
    {
        public TypeMismatchErpException(Type Mustbe, Type Current) : base($"Type mismatch: {Current.Name} is not {Mustbe.Name}") 
        {
        }

        public TypeMismatchErpException() : base("Type mismatch") 
        {
        }
    }
}
