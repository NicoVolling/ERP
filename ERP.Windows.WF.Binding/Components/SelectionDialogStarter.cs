using ERP.Business.Objects;
using ERP.Windows.WF.Binding.Controls;
using ERP.Windows.WF.Binding.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Windows.WF.Binding.Components
{
    public partial class SelectionDialogStarter : Component
    {
        public SelectionDialogStarter()
        {
            InitializeComponent();
        }

        public SelectionDialogStarter(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        [Category("Bindung")]
        public string BindingDestinationClient { get; set; }

        [Category("Bindung")]
        public string BindingDestinationList { get; set; }

        [Category("Bindung")]
        public DataContext DataContext { get; set; }

        public BusinessObject OpenDialog()
        {
            SelectionForm SF = new(BindingDestinationList, BindingDestinationClient, DataContext);
            if (SF.ShowDialog() is DialogResult DR && DR == DialogResult.OK)
            {
                return SF.SelectedObject;
            }
            else
            {
                return null;
            }
        }
    }
}