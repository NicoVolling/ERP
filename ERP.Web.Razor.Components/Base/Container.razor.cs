using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Web.Razor.Components.Base
{
    public partial class Container : BaseComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}