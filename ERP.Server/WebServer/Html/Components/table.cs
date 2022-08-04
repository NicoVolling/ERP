using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html.Components
{
    public class table : WebComponent
    {
        private tr columns;

        private List<tr> rows = new();

        public table() : base(nameof(table))
        {
        }

        public void AddColumns(tr Columns)
        {
            columns = Columns;
        }

        public void AddDataRow(tr DataRow)
        {
            rows.Add(DataRow);
        }

        public new table AddAttribute(string Name, string Value) => (table)base.AddAttribute(Name, Value);

        public override string ToString()
        {
            List<WebComponent> body = new();
            body.AddRange(Body);
            if (columns != null)
            {
                body.Add(new thead().AddInnerComponent(columns));
            }
            if (rows.Any())
            {
                body.Add(new tbody().AddInnerComponents(rows));
            }
            return OnToString(body, AltBody, Tag, Attributes);
        }
    }
}