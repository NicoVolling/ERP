using ERP.Windows.WF.Binding.Controls;
using System.ComponentModel;

namespace ERP.Windows.WF.BindableControlSet
{
    public partial class BindableTextBox : BindableControlBase, IBindable
    {
        public BindableTextBox()
        {
            InitializeComponent();
        }

        public Type OriginType { get => typeof(string); }

        [Category("Darstellung")]
        public bool UserPasswordChar { get => textbox.UseSystemPasswordChar; set => textbox.UseSystemPasswordChar = value; }

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