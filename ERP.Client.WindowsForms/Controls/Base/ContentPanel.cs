using ERP.BaseLib.Objecs;
using ERP.Client.WindowsForms.Binding;
using ERP.Client.WindowsForms.Controls.BindableControls;
using ERP.Client.WindowsForms.Controls.Windows;
using ERP.Client.WindowsForms.Messaging;
using ERP.Exceptions.ErpExceptions;
using System.ComponentModel;
using System.Reflection;

namespace ERP.Client.WindowsForms.Controls.Base
{
    public class ContentPanel : UserControl
    {
        public ContentPanel()
        {
            InitializeComponent();
        }

        public event EventHandler ErrorChanged;

        public event EventHandler<BindingStatusChangedEventArgs> StatusChanged;

        public DataContext DataContext { get; set; }
        public bool HasError { get; private set; }

        public BindingStatus Status
        {
            get
            {
                int saved = 0;
                int unbound = 0;

                foreach (BindableControl Bindable in Bindables)
                {
                    if (Bindable.Status == BindingStatus.Error)
                    {
                        return BindingStatus.Error;
                    }
                    else if (Bindable.Status == BindingStatus.Unsaved)
                    {
                        return BindingStatus.Unsaved;
                    }
                    saved += Bindable.Status == BindingStatus.Saved ? 1 : 0;
                    unbound += Bindable.Status == BindingStatus.Unbound ? 1 : 0;
                }
                if (saved == Bindables.Count - unbound)
                {
                    return BindingStatus.Saved;
                }
                else if (unbound == Bindables.Count)
                {
                    return BindingStatus.Unbound;
                }
                return BindingStatus.NullOrDefault;
            }
        }

        protected BaseWindow BaseWindow { get; private set; }
        private List<BindableControl> Bindables { get; } = new List<BindableControl>();

        public static bool IsInDesignMode()
        {
            if (Application.ExecutablePath.IndexOf("DesignToolsServer.exe", StringComparison.OrdinalIgnoreCase) > -1)
            {
                return true;
            }
            return false;
        }

        public void ClearAllBinables()
        {
            foreach (BindableControl Bindable in Bindables)
            {
                Bindable.Clear();
            }
        }

        public void Closed()
        {
            OnClosed();
        }

        public void Maximized(bool Maximize)
        {
            OnMaximized(Maximize);
        }

        public void Minimized(bool Minimize)
        {
            OnMinimized(Minimize);
        }

        public void Open(BaseWindow BaseWindow)
        {
            this.BaseWindow = BaseWindow;
            Opened();
        }

        public void Opened()
        {
            OnOpened();
        }

        public void ShowMessage(string Title, string Message)
        {
            BaseWindow BW = new BaseWindow(new MessagingContentPanel() { Message = Message, DataContext = DataContext.Empty }) { Icon = ERP.Client.WindowsForms.Base.Resources.Icon, Text = Title };
            this.BaseWindow.ParentBaseForm.OpenWindow(BW);
        }

        public void SyncAll()
        {
            foreach (BindableControl Bindable in Bindables)
            {
                Bindable.Sync();
            }
        }

        protected void ControllAddedHandling(Control Control)
        {
            if (!IsInDesignMode())
            {
                if (Control is BindableControl Bindable)
                {
                    if (Bindable.BindingDestination is string BindingDestination)
                    {
                        if (BindingDestination.Split('.', StringSplitOptions.RemoveEmptyEntries) is IEnumerable<string> str && str.Any())
                        {
                            Bind(Bindable, str);
                            if (!Bindables.Contains(Bindable))
                            {
                                Bindables.Add(Bindable);
                            }
                        }
                    }
                }
                foreach (Control Control1 in Control.Controls)
                {
                    ControllAddedHandling(Control1);
                }
                Control.ControlAdded += (s, e) =>
                {
                    ControllAddedHandling(e.Control);
                };
            }
        }

        protected void GetAccessors(Object Parent, IEnumerable<string> ObjectName, out Action<Object> Set, out Func<Object> Get, out INotifyPropertyChanged PropertyChangedNotifier, out string PropertyName, out Type TargetType)
        {
            if (Parent.GetType().GetProperties().Where(o => o.Name.Equals(ObjectName.First())).FirstOrDefault(o => o.DeclaringType != typeof(ContentPanel)) is PropertyInfo PI)
            {
                if (PI.CanRead)
                {
                    if (ObjectName.Count() > 1)
                    {
                        Object obj = PI.GetValue(Parent);
                        if (obj is INotifyPropertyChanged NewParent)
                        {
                            GetAccessors(NewParent, ObjectName.Skip(1), out Set, out Get, out PropertyChangedNotifier, out PropertyName, out TargetType);
                        }
                        else if (obj is null && Parent is INotifyPropertyChanged PCN)
                        {
                            Set = null;
                            Get = null;
                            PropertyChangedNotifier = PCN;
                            PropertyName = ObjectName.First();
                            TargetType = PI.PropertyType;
                        }
                        else
                        {
                            throw new ErpException("Couldnt get Value, incorrect type or null");
                        }
                    }
                    else
                    {
                        if (Parent is PropertyChangedNotifier PCN)
                        {
                            Get = () => PI.GetValue(PCN);
                            if (PI.CanWrite)
                            {
                                Set = (Value) => { PI.SetValue(PCN, Value); };
                            }
                            else
                            {
                                throw new ErpException("Coudlnt set Value");
                            }
                            PropertyChangedNotifier = PCN;
                            PropertyName = ObjectName.First();
                            TargetType = PI.PropertyType;
                        }
                        else
                        {
                            throw new ErpException("Parent is not in correct type");
                        }
                    }
                }
                else
                {
                    throw new ErpException("Couldnt read Value");
                }
            }
            else
            {
                throw new ErpException("Couldnt find Value");
            }
        }

        protected virtual void OnClosed()
        {
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            ControllAddedHandling(e.Control);
        }

        protected virtual void OnDataContextChanged(string PropertyName)
        {
        }

        protected virtual void OnErrorChanged()
        {
            ErrorChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DataContext == null && !IsInDesignMode()) { throw new ErpException("DataContext is null"); }
            if (!IsInDesignMode()) { DataContext.PropertyChanged += (s, e) => { OnDataContextChanged(e.PropertyName); }; }
        }

        protected virtual void OnMaximized(bool Maximize)
        {
        }

        protected virtual void OnMinimized(bool Minimize)
        {
        }

        protected virtual void OnOpened()
        {
        }

        protected void OnStatusChanged(BindingStatus Status)
        {
            StatusChanged?.Invoke(this, new BindingStatusChangedEventArgs(Status));
            ProofError();
        }

        private void Bind(BindableControl Bindable, IEnumerable<string> ObjectName)
        {
            List<string> tmp = ObjectName.ToList();
            tmp.Insert(0, nameof(DataContext));
            ObjectName = tmp;

            GetAccessors(this, ObjectName, out Action<object> Set, out Func<object> Get, out INotifyPropertyChanged PropertyChangedNotifier, out string PropertyName, out Type TargetType);

            if (Set == null && Get == null && PropertyChangedNotifier != null && PropertyName != null && TargetType != null)
            {
                PropertyChangedNotifier.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == PropertyName)
                    {
                        Bind(Bindable, ObjectName.Skip(1));
                    }
                };
            }
            else if (Set != null && Get != null && PropertyChangedNotifier != null && PropertyName != null && TargetType != null)
            {
                Bindable.Bind(Get, Set, PropertyChangedNotifier, PropertyName, TargetType);
                Bindable.StatusChanged += (s, e) => { OnStatusChanged(this.Status); };
            }
            else
            {
                throw new ErpException("Binding failed");
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // ContentPanel
            //
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(402, 245);
            this.Name = "ContentPanel";
            this.Size = new System.Drawing.Size(402, 245);
            this.ResumeLayout(false);
        }

        private void ProofError()
        {
            bool err = Bindables.Any(o => o.HasError);
            if (HasError != err)
            {
                HasError = err;
                OnErrorChanged();
            }
        }
    }
}