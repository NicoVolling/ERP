using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html
{
    public class WebComponent : WebPage
    {
        public WebComponent(string Tag)
        {
            this.Tag = Tag;
        }

        protected string AltBody { get; set; }

        protected string Attributes { get; set; }

        protected List<WebComponent> Body { get; } = new();

        protected string Tag { get; }

        public WebComponent AddAttribute(string Name, string Value)
        {
            Attributes += @$" {Name}=""{Value}""";
            return this;
        }

        public new WebComponent AddInnerComponent(WebComponent InnerComponent)
        {
            Body.Add(InnerComponent);
            return this;
        }

        public new WebComponent AddInnerComponents(IEnumerable<WebComponent> InnerComponents)
        {
            Body.AddRange(InnerComponents);
            return this;
        }

        public WebComponent AddInnerText(string Text)
        {
            AltBody = Text;
            return this;
        }

        public override string ToString()
        {
            return OnToString(Body, AltBody, Tag, Attributes);
        }

        protected string OnToString(List<WebComponent> Body, string AltBody, string Tag, string Attributes)
        {
            string body = Body.Any() ? string.Join("\r\n", Body.Select(o => o.ToString())) : AltBody;
            return $"<{Tag}{Attributes}>{body}</{Tag}>";
        }
    }
}