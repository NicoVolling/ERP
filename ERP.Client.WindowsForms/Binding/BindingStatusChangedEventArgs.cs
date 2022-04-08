using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Binding
{
    public class BindingStatusChangedEventArgs : EventArgs
    {
        public BindingStatus Status { get; init; }

        public BindingStatusChangedEventArgs(BindingStatus Status)
        {
            this.Status = Status;
        }
    }
}
