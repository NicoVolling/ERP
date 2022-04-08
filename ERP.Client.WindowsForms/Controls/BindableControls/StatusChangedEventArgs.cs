using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Controls.BindableControls
{
    public class StatusChangedEventArgs : EventArgs
    {
        public BindingStatus Status { get; init; }

        public StatusChangedEventArgs(BindingStatus Status)
        {
            this.Status = Status;
        }
    }
}
