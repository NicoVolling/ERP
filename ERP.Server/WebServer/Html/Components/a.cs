using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html.Components
{
    public class a : WebComponent
    {
        public a() : base(nameof(a))
        {
        }

        public a SetLink(string Link)
        {
            Attributes = string.Empty;
            AddAttribute("href", Link);
            return this;
        }
    }
}