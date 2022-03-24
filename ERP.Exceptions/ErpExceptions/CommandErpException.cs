using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class CommandErpException : ErpException
    {
        public CommandErpException(string Message) : base($"Command Error: {Message}")
        {
        }

        public CommandErpException() : base("Command Error") 
        {
        }
    }
}
