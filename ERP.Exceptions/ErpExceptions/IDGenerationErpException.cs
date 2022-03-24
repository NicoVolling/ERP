using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class IDGenerationErpException : ErpException
    {
        public IDGenerationErpException(Type Type) : base($"ID Generation for Type {Type.Name} failed")
        {
        }

        public IDGenerationErpException() : base("ID Generation failed")
        {
        }
    }
}
