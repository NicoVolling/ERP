using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class ReflectionErpException : ErpException
    {
        public ReflectionErpException(string Message) : base($"Reflection error: {Message}")
        {
        }

        public ReflectionErpException() : base("Reflection error")
        {
        }
    }
}
