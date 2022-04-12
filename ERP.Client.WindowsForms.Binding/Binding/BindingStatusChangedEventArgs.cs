namespace ERP.Client.WindowsForms.Binding
{
    public class BindingStatusChangedEventArgs : EventArgs
    {
        public BindingStatusChangedEventArgs(BindingStatus Status)
        {
            this.Status = Status;
        }

        public BindingStatus Status { get; init; }
    }
}