using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html.Components
{
    public class td : WebComponent
    {
        public td() : base(nameof(td))
        {
        }

        public td(string Tag) : base(Tag)
        {
        }

        public new td AddInnerComponent(WebComponent WebComponent)
        {
            return (td)base.AddInnerComponent(WebComponent);
        }

        public new td AddInnerText(string Text)
        {
            return (td)base.AddInnerText(Text);
        }
    }
}