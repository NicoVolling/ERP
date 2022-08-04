using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html.Components
{
    public class th : td
    {
        public th() : base(nameof(th))
        {
        }

        public new th AddInnerComponent(WebComponent WebComponent)
        {
            return (th)base.AddInnerComponent(WebComponent);
        }

        public new th AddInnerText(string Text)
        {
            return (th)base.AddInnerText(Text);
        }
    }
}