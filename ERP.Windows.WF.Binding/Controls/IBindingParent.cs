using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Windows.WF.Binding.Controls
{
    public interface IBindingParent
    {
        public event ControlEventHandler ControlAdded;

        public DataContext DataContext { get; set; }
    }
}