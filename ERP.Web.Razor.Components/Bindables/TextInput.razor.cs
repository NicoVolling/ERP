using ERP.BaseLib.Objects;
using ERP.Business.Objects;
using ERP.Business.Objects.Attributes;
using ERP.Parsing.Parser;
using ERP.Web.Razor.Components.Base;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ERP.Web.Razor.Components.Bindables
{
    public partial class TextInput : BindableComponent<string>
    {
        private string description;

        private bool readOnly;

        public TextInput() : base(typeof(string))
        {
        }

        [Parameter]
        public string Description { get => SGA?.UserFriendlyName ?? description; init => description = value; }

        [Parameter]
        public bool ReadOnly { get => (!Property?.CanWrite ?? true) || readOnly; init => readOnly = value; }

        [Parameter]
        public bool ShowIcons { get; set; }

        [Parameter]
        public bool UsePasswordChar { get; set; }

        [Parameter]
        public Regex Verification { get; set; }

        private string ReadOnlyHtml { get => ReadOnly ? "readonly" : null; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override bool Verify()
        {
            return Verification?.IsMatch(Value_Destination) != false;
        }

        private void OnTextChanged(ChangeEventArgs e)
        {
            this.Value_Destination = e.Value.ToString();
        }
    }
}