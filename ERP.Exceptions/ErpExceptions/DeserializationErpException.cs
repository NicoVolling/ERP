using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class DeserializationErpException : JsonErpException
    {
        public DeserializationErpException(string Message) : base($"Deserialization: {Message}")
        {
        }

        public DeserializationErpException() : base()
        {
        }
    }
}
