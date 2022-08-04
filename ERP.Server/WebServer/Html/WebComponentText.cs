using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html
{
    public class WebComponentText : WebComponent
    {
        public WebComponentText() : base("")
        {
        }

        public override string ToString()
        {
            return AltBody;
        }
    }
}
