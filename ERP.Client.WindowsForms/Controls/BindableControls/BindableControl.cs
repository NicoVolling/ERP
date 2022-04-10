using ERP.BaseLib.Objecs;
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
    public partial class BindableControl : UserControl
    {
        public Func<Object> Get { get; private set; }
        public Action<Object> Set { get; private set; }

        private static double factor = -1;

        public Type TargetType { get; private set; }

        private bool error = false;

        public bool HasError { get => error; }

        private bool load = true;

        private BindingStatus status;

        private string description;
        private int? fixDescriptionWidth = null;

        public IParser Parser { get; private set; }

        private Type OrigingType { get; set; }

        public BindableControl()
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

        protected virtual IParser OnGetParser() { return new Parser<Object, Object>(o => o, o => o); }

        public static Color GetBindingStatusColor(BindingStatus Status)
        {
            var color = Status switch
            {
                BindingStatus.Unbound => Color.FromArgb(80, 80, 80),
                BindingStatus.NullOrDefault => Color.FromArgb(200, 200, 200),
                BindingStatus.Unsaved => Color.FromArgb(230, 200, 0),
                BindingStatus.Saved => Color.FromArgb(0, 230, 0),
                BindingStatus.Error => Color.FromArgb(230, 0, 0),
                _ => Color.Black,
            };
            return color;
        }

        public void Bind(Func<Object> Get, Action<Object> Set, INotifyPropertyChanged PropertyChangedNotifier, string PropertyName, Type TargetType)
        {
            this.TargetType = TargetType;
            this.Parser = OnGetParser();
            if (Parser.Type1 == TargetType) { OrigingType = Parser.Type2; }
            if (Parser.Type2 == TargetType) { OrigingType = Parser.Type1; }
            this.Set = (Object) =>
            {
                try
                {
                    Set(this.ParseToObject(Object));
                }
                catch
                {
                    Set(this.ParseToObject(null));
                }
            };
            this.Get = () =>
            {
                try
                {
                    return this.ParseFromObject(Get());
                }
                catch
                {
                    return this.ParseFromObject(null);
                }
            };
            PropertyChangedNotifier.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == PropertyName)
                {
                    this.LoadData();
                }
            };
            this.LoadData();
            if (this.Status != BindingStatus.NullOrDefault)
            {
                this.Status = BindingStatus.Unsaved;
            }
            this.OnBound();
        }

        protected virtual void OnBound()
        {
        }

        private void SetBindingStatus(BindingStatus Status)
        {
            Color Color = BindableControl.GetBindingStatusColor(Status);

            this.Enabled = Status != BindingStatus.Unbound;
            StatusLed.ForeColor = Color;
        }

        public BindingStatus Status
        {
            get => error ? BindingStatus.Error : status;
            protected set { status = value; SetBindingStatus(Status); OnStatusChanged(Status); }
        }

        public void Sync()
        {
            if (this is BindableControl Bindable)
            {
                Bindable.LoadData();
            }
        }

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

        public event EventHandler<BindingStatusChangedEventArgs>? StatusChanged;

        protected void OnStatusChanged(BindingStatus Status)
        {
            StatusChanged?.Invoke(this, new BindingStatusChangedEventArgs(Status));
        }

        [Category("Darstellung")]
        public string Description { get => description; set { description = value; lbl_Description.Text = $"{description}:"; } }

        [Category("Binding")]
        public string? BindingDestination { get => this.Tag?.ToString(); set => this.Tag = value; }

        [Category("Darstellung")]
        public int? FixedDescriptionWidth { get => fixDescriptionWidth; set { fixDescriptionWidth = value; lbl_Description_TextChanged(null, null); } }

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
                if (FixedDescriptionWidth.HasValue)
                {
                    lbl_Description.Size = new Size(FixedDescriptionWidth.Value, 0);
                }
                else
                {
                    lbl_Description.Size = new Size((int)lbl_Description.CreateGraphics().MeasureString(lbl_Description.Text, lbl_Description.Font).Width + 5, 0);
                }
            }
        }

        protected Object ParseFromObject(Object Value)
        {
            try
            {
                return Parser.Parse(Value, OrigingType);
            }
            catch
            {
                throw;
            }
        }

        protected Object ParseToObject(Object Value)
        {
            try
            {
                try
                {
                    Object Result = Parser.Parse(Value, TargetType);
                    error = false;
                    Status = Status;
                    return Result;
                }
                catch
                {
                    error = true;
                    Status = BindingStatus.Error;
                    return Parser.Parse(null, TargetType);
                }
            }
            catch
            {
                throw;
            }
        }

        public void Clear() 
        {
            Set(Parser.GetDefault(OrigingType));
        }
    }
}
