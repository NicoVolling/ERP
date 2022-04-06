using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class JsonErpException : ErpException
    {
        public JsonErpException(string Message) : base($"Json: {Message}") 
        {
        }

        public JsonErpException() : base() 
        {
        }
    }
}
