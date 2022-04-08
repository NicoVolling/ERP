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
    public partial class B_TextBox : BindableControlBase, IBindable
    {
        public Func<object> Get { get; set; }
        public Action<object> Set { get; set; }

        public B_TextBox()
        {
            InitializeComponent();
        }

        public void Bind(Func<object> Get, Action<object> Set, PropertyChangedNotifier PropertyChangedNotifier, string PropertyName, Type TargetType)
        {
            IBindable.Bind(this, Get, Set, PropertyChangedNotifier, PropertyName, TargetType);
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
