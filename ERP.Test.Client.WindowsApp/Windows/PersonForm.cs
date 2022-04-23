using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;
using ERP.Windows.WF.Binding.Controls;
using ERP.Windows.WF.Binding.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Test.Client.WindowsApp.Windows
{
    public partial class PersonForm : BindingForm
    {
        public PersonForm()
        {
            InitializeComponent();
        }

        public new PersonForm_DataContext DataContext { get => base.DataContext as PersonForm_DataContext; set => base.DataContext = value; }

        protected override DataContext GetDataContext()
        {
            return new PersonForm_DataContext();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataContext.Client = new PersonClient();
            DataContext.Objects = DataContext.Client.GetList();
        }

        private void bindableTextBox4_ButtonClick(object sender, Business.Objects.BusinessObject e)
        {
            if (e != null)
            {
                DataContext.Person = e as Person;
            }
        }
    }
}