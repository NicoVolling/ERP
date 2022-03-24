using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class SerializationErpException : JsonErpException
    {
        public SerializationErpException(string Message) : base($"Serialization: {Message}")
        {
        }

        public SerializationErpException() : base()
        {
        }
    }
}
