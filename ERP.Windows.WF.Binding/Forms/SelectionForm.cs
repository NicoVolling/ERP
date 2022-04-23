using ERP.Business.Objects;
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
    public partial class SelectionForm : BindingForm
    {
        public string BindingDestination;
        public string BindingDestinationClient;

        private DataContext dataContext;

        public SelectionForm(string BindingDestination, string BindingDestinationClient, DataContext DataContext)
        {
            this.BindingDestination = BindingDestination;
            this.BindingDestinationClient = BindingDestinationClient;
            this.dataContext = DataContext;
            this.DataContext = dataContext;
            this.BindingMaster = new BindingMaster(this);
            InitializeComponent();
        }

        public BusinessObject SelectedObject { get; private set; }

        protected override DataContext GetDataContext()
        {
            return dataContext;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            bindableDataGridView1.BindingDestination = BindingDestination;
            bindableDataGridView1.BindingDestinationClient = BindingDestinationClient;
            base.OnControlAdded(e);
        }

        private void bindableDataGridView1_Accept(object sender, EventArgs e)
        {
            btn_accept_Click(null, EventArgs.Empty);
        }

        private void btn_abort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btn_accept_Click(object sender, EventArgs e)
        {
            SelectedObject = bindableDataGridView1.SelectedObject;
            if (bindableDataGridView1.SelectedObject != null)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}