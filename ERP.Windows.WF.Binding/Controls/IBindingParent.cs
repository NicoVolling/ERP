namespace ERP.Windows.WF.Binding.Controls
{
    public interface IBindingParent
    {
        public event ControlEventHandler ControlAdded;

        public DataContext DataContext { get; set; }
    }
}