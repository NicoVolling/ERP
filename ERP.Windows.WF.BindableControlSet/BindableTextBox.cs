using ERP.Windows.WF.Base;
using ERP.Windows.WF.Binding.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Windows.WF.BindableControlSet
{
    public partial class BindableTextBox : BindableControlBase, IBindable
    {
        public BindableTextBox()
        {
            InitializeComponent();
        }

        public Type OriginType { get => typeof(string); }

        public object GetControlValue()
        {
            return textbox.Text;
        }

        public Control GetMainControl()
        {
            return textbox;
        }

        public void SetControlValue(object Value)
        {
            textbox.Text = Value.ToString();
        }

        private void textbox_TextChanged(object sender, EventArgs e)
        {
            OnControlValueChanged();
        }
    }
}