using ERP.Windows.WF.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Windows.WF.Binding.Controls
{
    public interface IBindable
    {
        public event EventHandler ControlValueChanged;

        public event EventHandler FormatRequest;

        public string BindingDestination { get; set; }

        public Type OriginType { get; }

        public InputStatus Status { get; set; }

        public string UserFriendlyName { get; set; }

        public Object GetControlValue();

        public Control GetMainControl();

        public void OnBound();

        public void SetControlValue(Object Value);
    }
}