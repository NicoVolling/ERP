using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Exceptions.ErpExceptions
{
    public class ErpException : Exception
    {

        public ErpException(string Message) : base(Message) 
        {
        }

        public ErpException() : base() 
        {
        }
    }
}
