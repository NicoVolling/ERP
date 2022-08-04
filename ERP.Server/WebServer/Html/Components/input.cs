using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html.Components
{
    public class input : WebComponent
    {
        public input() : base(nameof(input))
        {
        }

        public new input AddAttribute(string Name, string Value)
        {
            return (input)base.AddAttribute(Name, Value);
        }
    }
}