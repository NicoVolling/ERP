using ERP.BaseLib.Objects;
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
    public partial class BindableComponent<T_Destination> : BaseComponent
    {
        private bool changed;
        private bool error;
        private PropertyInfo property;

        public BindableComponent(Type Type_Destionation)
        {
            this.Type_Destination = Type_Destionation;
        }

        public event EventHandler<EventArgs> ChangedChanged;

        public event EventHandler<EventArgs> DestinationValueChanged;

        public event EventHandler<EventArgs> ErrorChanged;

        public event EventHandler<EventArgs> OriginalValueChanged;

        public bool Changed
        {
            get => changed;
            private set
            {
                bool change = changed != value;
                changed = value;
                if (change)
                {
                    ChangedChanged?.Invoke(this, null);
                }
            }
        }

        public bool Error
        {
            get => error;
            private set
            {
                bool change = error != value;
                error = value;
                if (change)
                {
                    ErrorChanged?.Invoke(this, null);
                }
            }
        }

        [Parameter]
        [EditorRequired]
        public Func<Object> GetValue { get; set; }

        [Parameter]
        [EditorRequired]
        public PropertyInfo Property
        {
            protected get => property;
            init
            {
                property = value;
                SGA = Property.GetCustomAttribute<ShowGUIAttribute>();
                Type_Original = Property.PropertyType;
                PropertyName = property.Name;
            }
        }

        public string PropertyName { get; init; }

        [Parameter]
        [EditorRequired]
        public Action<Object> SetValue { get; set; }

        public ShowGUIAttribute SGA { get; init; }

        public Type Type_Destination { get; init; }

        public Type Type_Original { get; init; }

        public T_Destination Value_Destination
        {
            get
            {
                ShowGUIAttribute SGA = this.SGA;
                if (!string.IsNullOrEmpty(FormatOptions))
                {
                    SGA = Json.Deserialize<ShowGUIAttribute>(SGA.Serialize());
                    SGA.FormatOptions = FormatOptions;
                }
                Object obj = Parser.Parse(Value_Original, Type_Destination, SGA, out bool Error);
                this.Error = Error;
                if (!Error && obj is T_Destination Dest)
                {
                    return Dest;
                }
                return default;
            }
            set
            {
                DestinationValueChanged?.Invoke(this, null);
                Object obj = Parser.Parse(value, Type_Original, SGA, out bool Error);
                this.Error = Error;
                if (!this.Error)
                {
                    Value_Original = obj;
                }
                Changed = true;
            }
        }

        public Object Value_Original
        {
            get => GetValue();
            set { SetValue(value); OriginalValueChanged?.Invoke(this, null); }
        }

        protected virtual string FormatOptions { get; }

        private IParser Parser { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Parser = ParsingMaster.GetParser(Type_Original, Type_Destination);
        }
    }
}