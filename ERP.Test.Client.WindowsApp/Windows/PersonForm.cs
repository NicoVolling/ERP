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

        protected override DataContext GetDataContext()
        {
            return new PersonForm_DataContext();
        }
    }
}