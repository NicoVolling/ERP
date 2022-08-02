using ERP.Windows.WF.Base;
using ERP.Windows.WF.Base.Controls;
using ERP.Windows.WF.Binding.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Windows.WF.BindableControlSet
{
    public class BindableTagLabel : TagLabel, IBindable
    {
        public BindableTagLabel()
        {
        }

        public event EventHandler ControlValueChanged;

        public event EventHandler FormatRequest;

        [Category("Bindung")]
        public bool Access
        {
            get => this.Visible;
            set => this.Visible = value;
        }

        [Category("Bindung")]
        public string BindingDestination { get; set; }

        public Type OriginType { get => typeof(string); }

        public bool ReadOnly
        { get => true; set { } }

        public InputStatus Status { get; set; }
        public string UserFriendlyName { get; set; }

        public new void Dispose()
        {
            base.Dispose();
            FormatRequest = null;
            ControlValueChanged = null;
        }

        public object GetControlValue()
        {
            return this.Text;
        }

        public Control GetMainControl()
        {
            return this;
        }

        public void OnBound(DataContext DataContext)
        {
        }

        public void SetControlValue(object Value)
        {
            this.Text = Value.ToString();
        }

        protected void Format()
        {
            FormatRequest?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnControlValueChanged()
        {
            ControlValueChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            OnControlValueChanged();
            base.OnTextChanged(e);
        }
    }
}