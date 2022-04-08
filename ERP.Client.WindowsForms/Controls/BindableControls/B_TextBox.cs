using ERP.BaseLib.Objecs;
using ERP.BaseLib.Serialization;
using ERP.Client.WindowsForms.Base;
using ERP.Client.WindowsForms.Binding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Client.WindowsForms.Controls.BindableControls
{
    public partial class B_TextBox : BindableControl
    {

        public B_TextBox()
        {
            InitializeComponent();
        }

        protected override void OnBound()
        {
            textBox1.TextChanged += (s, e) =>
            {
                Status = BindingStatus.Unsaved;
                SaveData();
            };
        }

        protected override void OnSaveData()
        {
            Set(textBox1.Text);
        }

        protected override void OnLoadData()
        {
            textBox1.Text = Get()?.ToString();
            Status = string.IsNullOrEmpty(textBox1.Text) || textBox1.Text.Equals("-1") ? BindingStatus.NullOrDefault : BindingStatus.Saved;
        }

        protected override object OnParseFromObject(object Value)
        {
            try 
            { 
                if(Value == null) { return ""; }
                return base.OnParseFromObject(Value);
            }
            catch 
            {
                return "";
            }
        }

        protected override object OnParseToObject(object Value)
        {
            if(TargetType == typeof(int)) 
            {
                if(Value == null) { return -1; }
                return Json.Deserialize<int>(Value.ToString());
            }
            if(Value == null) { return ""; }
            return base.OnParseToObject(Value);
        }
    }
}
