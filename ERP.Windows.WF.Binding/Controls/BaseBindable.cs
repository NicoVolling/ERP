using ERP.Windows.WF.Base;

namespace ERP.Windows.WF.Binding.Controls
{
    public class BaseBindable : IBindable
    {
        public event EventHandler ControlValueChanged;

        public event EventHandler FormatRequest;

        public bool Access { get; set; }

        public string BindingDestination { get; set; }

        public bool IsDisposed { get; private set; }

        public Type OriginType => null;

        public bool ReadOnly { get; set; }

        public InputStatus Status { get; set; }

        public string UserFriendlyName { get; set; }

        public Object Value { get; set; }

        public void Dispose()
        {
            ControlValueChanged = null;
            FormatRequest = null;
            IsDisposed = true;
        }

        public object GetControlValue()
        {
            return Value;
        }

        public Control GetMainControl()
        {
            return null;
        }

        public void OnBound(DataContext DataContext)
        {
        }

        public void SetControlValue(object Value)
        {
            this.Value = Value;
            ControlValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}