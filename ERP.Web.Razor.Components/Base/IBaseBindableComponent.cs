using ERP.BaseLib.Serialization;
using ERP.Business.Objects.Attributes;
using ERP.Parsing.Parser;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Web.Razor.Components.Base
{
    public interface IBaseBindableComponent
    {
        public event EventHandler<EventArgs> ChangedChanged;

        public event EventHandler<EventArgs> DestinationValueChanged;

        public event EventHandler<EventArgs> ErrorChanged;

        public event EventHandler<EventArgs> OriginalValueChanged;

        public bool Changed { get; }

        public bool Error { get; }

        [Parameter]
        [EditorRequired]
        public Func<Object> GetValue { get; set; }

        [Parameter]
        [EditorRequired]
        public PropertyInfo Property { get; init; }

        public string PropertyName { get; init; }

        [Parameter]
        [EditorRequired]
        public Action<Object> SetValue { get; set; }

        public ShowGUIAttribute SGA { get; init; }

        public Type Type_Destination { get; init; }

        public Type Type_Original { get; init; }

        public Object Value_Original { get; set; }

        public void Reset();
    }
}