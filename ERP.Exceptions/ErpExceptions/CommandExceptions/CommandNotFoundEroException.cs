using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions.CommandExceptions
{
    public class CommandNotFoundEroException : CommandErpException
    {
        public CommandNotFoundEroException(string Command) : base($"Could not find: \"{Command}\"")
        {
        }

        public CommandNotFoundEroException() : base("Could not find")
        {
        }
    }
}
