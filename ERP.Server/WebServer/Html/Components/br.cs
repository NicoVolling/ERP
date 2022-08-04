using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html.Components
{
    public class br : WebComponent
    {
        public br() : base(nameof(br))
        {
        }

        public override string ToString()
        {
            return $"<br />";
        }
    }
}