using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class CommandExecutionErpException : CommandErpException
    {
        public CommandExecutionErpException(string Command) : base($"Could not return a result: \"{Command}\"")
        {
        }

        public CommandExecutionErpException() : base("Could not return a result")
        {
        }
    }
}
