using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class MissingObjectErpException : ErpException
    {
        public MissingObjectErpException(Type Type, int ID) : base($"Object of type \"{Type.Name}\" not found: ID = {ID}")
        {
        }

        public MissingObjectErpException() : base("Object not found")
        {
        }
    }
}
