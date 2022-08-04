using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Server.WebServer.Html.Components
{
    public class style : WebComponent
    {
        public style() : base(nameof(style))
        {
        }

        public static style DocumentationStyle
        {
            get
            {
                style st = new style();
                st.AddInnerText(@"
    table.minimalistBlack {
      font-family: ""Lucida Sans Unicode"", ""Lucida Grande"", sans-serif;
      border: 3px solid #000000;
      width: 100%;
      text-align: left;
      border-collapse: collapse;
    }
    table.minimalistBlack td, table.minimalistBlack th {
      border: 1px solid #000000;
      padding: 5px 4px;
    }
    table.minimalistBlack tbody td {
      font-size: 13px;
    }
    table.minimalistBlack thead {
      background: #CFCFCF;
      background: -moz-linear-gradient(top, #dbdbdb 0%, #d3d3d3 66%, #CFCFCF 100%);
      background: -webkit-linear-gradient(top, #dbdbdb 0%, #d3d3d3 66%, #CFCFCF 100%);
      background: linear-gradient(to bottom, #dbdbdb 0%, #d3d3d3 66%, #CFCFCF 100%);
      border-bottom: 3px solid #000000;
    }
    table.minimalistBlack thead th {
      font-size: 15px;
      font-weight: bold;
      color: #000000;
      text-align: left;
    }
    input {
      width: 97%;
    }
");
                return st;
            }
        }
    }
}