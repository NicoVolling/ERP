using ERP.Client.WindowsForms.Binding;
using ERP.Client.WindowsForms.Binding.Parser;
using ERP.Client.WindowsForms.Controls.Base;
using System.ComponentModel;

namespace ERP.Client.WindowsForms.Controls.BindableControls
{
    public partial class BindableControl : BaseControl
    {
        private static double factor = -1;
        private string description;
        private int? fixDescriptionWidth = null;
        private bool isReadOnly;
        private bool load = true;
        private BindingStatus status;

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

        public event EventHandler ButtonClick;

        public event EventHandler<BindingStatusChangedEventArgs> StatusChanged;

        [Category("Bindung")]
        public string BindingDestination { get => this.Tag?.ToString(); set => this.Tag = value; }

        [Category("Darstellung"), DefaultValue(false)]
        public bool ButtonVisible { get => panel_btn.Visible; set => panel_btn.Visible = value; }

        [Category("Darstellung")]
        public string Description
        { get => description; set { description = value; lbl_Description.Text = $"{description}:"; } }

        [Category("Darstellung")]
        public int? FixedDescriptionWidth
        { get => fixDescriptionWidth; set { fixDescriptionWidth = value; lbl_Description_TextChanged(null, null); } }

        public Func<Object> Get { get; private set; }

        public bool HasError { get; private set; } = false;

        [Category("Verhalten")]
        public bool IsReadOnly
        { get => isReadOnly; set { isReadOnly = value; OnIsReadOnlyChanged(); } }

        public IParser Parser { get; private set; }

        public Action<Object> Set { get; private set; }

        [Category("Bindung")]
        public BindingStatus Status
        {
            get => HasError ? BindingStatus.Error : status;
            protected set { status = value; SetBindingStatus(Status); OnStatusChanged(Status); }
        }

        [Category("Darstellung"), DefaultValue(true)]
        public bool StatusVisible { get => StatusPanel.Visible; set => StatusPanel.Visible = value; }

        public Type TargetType { get; private set; }

        private Type OrigingType { get; set; }

        public static Color GetBindingStatusColor(BindingStatus Status, bool ReadOnly = false)
        {
            if (ReadOnly)
            {
                Color color = Status switch
                {
                    BindingStatus.Unbound => Color.FromArgb(50, 50, 50),
                    BindingStatus.NullOrDefault => Color.FromArgb(150, 150, 150),
                    BindingStatus.Unsaved => Color.FromArgb(190, 170, 0),
                    BindingStatus.Saved => Color.FromArgb(0, 190, 0),
                    BindingStatus.Error => Color.FromArgb(190, 0, 0),
                    _ => Color.Black,
                };
                return color;
            }
            else
            {
                Color color = Status switch
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
        }

        public void Bind(Func<Object> Get, Action<Object> Set, INotifyPropertyChanged PropertyChangedNotifier, string PropertyName, Type TargetType)
        {
            this.TargetType = TargetType;
            this.Parser = OnGetParser();
            if (Parser.Type1 == TargetType) { OrigingType = Parser.Type2; }
            if (Parser.Type2 == TargetType) { OrigingType = Parser.Type1; }
            if (OrigingType is null && Parser.Type1 == Parser.Type2) { OrigingType = Parser.Type1; }
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

        public void Clear()
        {
            OnClear();
        }

        public void LoadData()
        {
            if (load)
            {
                OnLoadData();
            }
        }

        public void SaveData()
        {
            load = false;
            OnSaveData();
            load = true;
        }

        public void Sync()
        {
            if (this is BindableControl Bindable)
            {
                Bindable.LoadData();
            }
        }

        protected virtual void OnBound()
        {
        }

        protected virtual void OnButtonClick()
        {
            ButtonClick?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnClear()
        {
            Set(Parser.GetDefault(OrigingType));
        }

        protected virtual IParser OnGetParser()
        { return new Parser<Object, Object>(o => o, o => o); }

        protected virtual void OnIsReadOnlyChanged()
        {
            this.ControlPanel.Enabled = !IsReadOnly;
            this.btn.Enabled = !IsReadOnly;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            OnSizeChanged(null);
        }

        protected virtual void OnLoadData()
        { }

        protected virtual void OnSaveData()
        { }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (factor > -1)
            {
                StatusPanel.Width = (int)((double)StatusPanel.Height / factor);
            }
        }

        protected void OnStatusChanged(BindingStatus Status)
        {
            StatusChanged?.Invoke(this, new BindingStatusChangedEventArgs(Status));
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
                    HasError = false;
                    Status = Status;
                    return Result;
                }
                catch
                {
                    HasError = true;
                    Status = BindingStatus.Error;
                    return Parser.Parse(null, TargetType);
                }
            }
            catch
            {
                throw;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            OnButtonClick();
        }

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

        private void SetBindingStatus(BindingStatus Status)
        {
            Color Color = BindableControl.GetBindingStatusColor(Status, IsReadOnly);

            this.Enabled = Status != BindingStatus.Unbound;
            StatusLed.ForeColor = Color;
        }
    }
}