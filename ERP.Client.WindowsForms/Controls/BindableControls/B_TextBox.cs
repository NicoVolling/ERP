using ERP.BaseLib.Objecs;
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
    public partial class B_TextBox : BindableControlBase, IBindable
    {
        public Func<object> Get { get; set; }
        public Action<object> Set { get; set; }

        public B_TextBox()
        {
            InitializeComponent();
        }

        public void Bind(Func<object> Get, Action<object> Set, PropertyChangedNotifier PropertyChangedNotifier, string PropertyName)
        {
            IBindable.Bind(this, Get, Set, PropertyChangedNotifier, PropertyName);
            textBox1.TextChanged += (s, e) => 
            {
                SetBindingStatus(BindingStatus.Unsaved);
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
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                SetBindingStatus(BindingStatus.NullOrDefault);
            }
            else 
            {
                SetBindingStatus(BindingStatus.Saved);
            }
        }
    }
}
