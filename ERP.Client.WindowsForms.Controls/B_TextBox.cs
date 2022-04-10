using ERP.BaseLib.Objecs;
using ERP.BaseLib.Serialization;
using ERP.Client.WindowsForms.Base;
using ERP.Client.WindowsForms.Binding;
using ERP.Client.WindowsForms.Binding.Parser;
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

        protected override IParser OnGetParser()
        {
            return IParser.GetParser(typeof(string), TargetType);
        }

        bool textchanged = true;

        protected override void OnBound()
        {
            textBox1.TextChanged += (s, e) =>
            {
                if (textchanged)
                {
                    textchanged = false;

                    if (!Parser.IsDefault(ParseToObject(textBox1.Text), TargetType) || textBox1.Text.Length == 0 || HasError)
                    {
                        Status = BindingStatus.Unsaved;
                        SaveData();
                    }
                    else
                    {
                        textchanged = true;
                        textBox1.Text = Parser.GetDefault(typeof(string)).ToString();
                    }
                    textchanged = true;
                }
            };
        }

        public TextBox TextBox { get => textBox1; }

        protected override void OnSaveData()
        {
            Set(textBox1.Text);
        }

        protected override void OnLoadData()
        {
            textBox1.Text = Get()?.ToString();
            Status = string.IsNullOrEmpty(textBox1.Text) ? BindingStatus.NullOrDefault : BindingStatus.Saved;
        }
    }
}
