using ERP.BaseLib.Objects;
using ERP.Business.Objects;
using ERP.Business.Objects.Attributes;
using ERP.Parsing.Parser;
using ERP.Web.Razor.Components.Base;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace ERP.Web.Razor.Components.Bindables
{
    public partial class EmailInput : BindableComponent<string>
    {
        private string description;

        private bool readyOnly;

        public EmailInput() : base(typeof(string))
        {
        }

        [Parameter]
        public string Description { get => SGA?.UserFriendlyName ?? description; init => description = value; }

        [Parameter]
        public bool ReadOnly { get => (!Property?.CanWrite ?? true) || readyOnly; init => readyOnly = value; }

        [Parameter]
        public bool TextCenter { get; init; }

        protected override string FormatOptions => "yyyy-MM-dd";

        private string ReadOnlyHtml { get => ReadOnly ? "readonly" : null; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private void OnTextChanged(ChangeEventArgs e)
        {
            this.Value_Destination = e.Value.ToString();
        }
    }
}