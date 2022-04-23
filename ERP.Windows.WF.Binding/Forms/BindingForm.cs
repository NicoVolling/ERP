using ERP.Windows.WF.Base;
using ERP.Windows.WF.Binding.Controls;
using ERP.Windows.WF.Binding.Supervisor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Windows.WF.Binding.Forms
{
    public partial class BindingForm : BaseForm, IBindingParent
    {
        public BindingForm()
        {
            DataContext = GetDataContext();
            if (DataContext != null)
            {
                this.BindingMaster = new BindingMaster(this);
            }
            InitializeComponent();
        }

        public BindingMaster BindingMaster { get; protected set; }

        public DataContext DataContext { get; set; }

        protected virtual DataContext GetDataContext()
        {
            return null;
        }
    }
}