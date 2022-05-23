using ERP.Windows.WF.Base;

namespace ERP.Windows.WF.Binding.Controls
{
    public interface IBindable : IDisposable
    {
        public event EventHandler ControlValueChanged;

        public event EventHandler FormatRequest;

        public bool Access { get; set; }

        public string BindingDestination { get; set; }

        public bool IsDisposed { get; }

        public Type OriginType { get; }

        public InputStatus Status { get; set; }

        public string UserFriendlyName { get; set; }

        public Object GetControlValue();

        public Control GetMainControl();

        public void OnBound(DataContext DataContext);

        public void SetControlValue(Object Value);
    }
}