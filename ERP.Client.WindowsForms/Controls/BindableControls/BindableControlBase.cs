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
        private static double factor = -1;

        public Type TargetType { get; set; }

        private bool Error = false;

        public BindableControlBase()
        {
            InitializeComponent();
            if (factor == -1)
            {
                factor = (double)StatusPanel.Height / (double)StatusPanel.Width;
            }
            OnSizeChanged(null);
            lbl_Description_TextChanged(null, null);
            Status = BindingStatus.Unsaved;
        }

        private void SetBindingStatus(BindingStatus Status)
        {
            Color Color = IBindable.GetBindingStatusColor(Status);

            this.Enabled = Status != BindingStatus.Unbound;
            StatusLed.ForeColor = Color;
        }

        public BindingStatus Status 
        { 
            get => Error ? BindingStatus.Error : status; 
            set { status = value; OnStatusChanged(Status); SetBindingStatus(Status); } 
        }

        public void Sync()
        {
            if (this is IBindable Bindable)
            {
                Bindable.LoadData();
            }
        }

        bool load = true;
        private BindingStatus status;
        private string description;

        public void SaveData()
        {
            load = false;
            OnSaveData();
            load = true;
        }

        protected virtual void OnSaveData() { }

        public void LoadData()
        {
            if (load)
            {
                OnLoadData();
            }
        }

        protected virtual void OnLoadData() { }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (factor > -1)
            {
                StatusPanel.Width = (int)((double)StatusPanel.Height / factor);
            }
        }

        public event EventHandler<StatusChangedEventArgs>? StatusChanged;

        protected void OnStatusChanged(BindingStatus Status)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(Status));
        }

        [Category("Darstellung")]
        public string Description { get => description; set { description = value; lbl_Description.Text = $"{description}:"; } }

        private void lbl_Description_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Description))
            {
                lbl_Description.Visible = false;
                lbl_Description.Size = new Size(0, 0);
            }
            else
            {
                lbl_Description.Visible = true;
                lbl_Description.Size = new Size((int)lbl_Description.CreateGraphics().MeasureString(lbl_Description.Text, lbl_Description.Font).Width + 5, 0);
            }
        }

        public Object ParseFromObject(Object Value) 
        {
            try
            {
                return OnParseFromObject(Value);
            }
            catch
            {
                throw;
            }
        }

        public Object ParseToObject(Object Value) 
        {
            try
            {
                try
                {
                    Object Result = OnParseToObject(Value);
                    Error = false;
                    Status = Status;
                    return Result;
                }
                catch
                {
                    Error = true;
                    Status = BindingStatus.Error;
                    return OnParseToObject(null);
                }
            }
            catch
            {
                throw;
            }
        }

        protected virtual Object OnParseFromObject(Object Value) { return Value; }
        protected virtual Object OnParseToObject(Object Value) { return Value; }
    }
}
