using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html
{
    public class WebPage
    {
        public WebPage()
        {
        }

        private List<WebComponent> Body { get; } = new();

        public WebPage AddInnerComponent(WebComponent InnerComponent)
        {
            Body.Add(InnerComponent);
            return this;
        }

        public override string ToString()
        {
            string body = string.Join("\r\n", Body.Select(o => o.ToString()));
            return $"<!doctype html>{body}";
        }
    }
}