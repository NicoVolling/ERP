using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html.Components
{
    public class tr : WebComponent
    {
        private List<td> columns = new();

        public tr() : base(nameof(tr))
        {
        }

        public new tr AddAttribute(string Name, string Value) => (tr)base.AddAttribute(Name, Value);

        public tr AddColumn(td Column)
        {
            columns.Add(Column);
            return this;
        }

        public override string ToString()
        {
            List<WebComponent> body = new();
            body.AddRange(Body);
            body.AddRange(columns);
            return OnToString(body, AltBody, Tag, Attributes);
        }
    }
}