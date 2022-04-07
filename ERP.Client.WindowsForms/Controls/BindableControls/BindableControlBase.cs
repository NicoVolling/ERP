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
    public partial class BindableControlBase : UserControl
    {
        public BindableControlBase()
        {
            InitializeComponent();
            OnSizeChanged(null);
        }

        protected void SetBindingStatus(BindingStatus Status) 
        {
            Color Color = Color.FromArgb(80, 80, 80);

            switch (Status)
            {
                case BindingStatus.Unbound:
                    Color = Color.FromArgb(80, 80, 80);
                    break;
                case BindingStatus.NullOrDefault:
                    Color = Color.FromArgb(130, 130, 130);
                    break;
                case BindingStatus.Unsaved:
                    Color = Color.FromArgb(230, 200, 0);
                    break;
                case BindingStatus.Saved:
                    Color = Color.FromArgb(0, 230, 0);
                    break;
                case BindingStatus.Error:
                    Color = Color.FromArgb(230, 0, 0);
                    break;
                default:
                    Color = Color.Black;
                    break;
            }

            this.Enabled = Status != BindingStatus.Unbound;
            StatusLed.ForeColor = Color;
        }

        private BindingStatus status = BindingStatus.Unbound;

        protected enum BindingStatus 
        {
            Unbound,
            NullOrDefault,
            Unsaved,
            Saved,
            Error
        }

        public void Sync() 
        {
            if(this is IBindable Bindable) 
            {
                Bindable.LoadData();
            }
        }

        bool load = true;

        public void SaveData() 
        {
            load = false;
            OnSaveData();
            load = true;
        }

        protected virtual void OnSaveData() { }

        public void LoadData() 
        {
            if(load) 
            {
                OnLoadData();
            }
        }

        protected virtual void OnLoadData() { }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            StatusPanel.Width = StatusPanel.Height;
        }
    }
}
