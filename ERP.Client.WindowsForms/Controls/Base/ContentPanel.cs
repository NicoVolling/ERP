﻿using ERP.BaseLib.Objecs;
using ERP.Client.WindowsForms.Base;
using ERP.Client.WindowsForms.Binding;
using ERP.Client.WindowsForms.Controls.BindableControls;
using ERP.Client.WindowsForms.Controls.Windows;
using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Controls.Base
{
    public class ContentPanel : UserControl
    {
        protected BaseWindow BaseWindow { get; private set; }

        public DataContext _DataContext { get; set; }

        public ContentPanel()
        {
            InitializeComponent();
        }

        public static bool IsInDesignMode()
        {
            if (Application.ExecutablePath.IndexOf("DesignToolsServer.exe", StringComparison.OrdinalIgnoreCase) > -1)
            {
                return true;
            }
            return false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_DataContext == null && !IsInDesignMode()) { throw new ErpException("DataContext is null"); }
        }

        public void Open(BaseWindow BaseWindow)
        {
            this.BaseWindow = BaseWindow;
            Opened();
        }

        public void Closed()
        {
            OnClosed();
        }

        protected virtual void OnClosed()
        {
        }

        public void Minimized(bool Minimize)
        {
            OnMinimized(Minimize);
        }

        protected virtual void OnMinimized(bool Minimize)
        {
        }

        public void Maximized(bool Maximize)
        {
            OnMaximized(Maximize);
        }

        protected virtual void OnMaximized(bool Maximize)
        {
        }

        public void Opened()
        {
            OnOpened();
        }

        protected virtual void OnOpened()
        {
        }

        public BindingStatus Status 
        { 
            get 
            {
                int saved = 0;
                int unbound = 0;

                foreach(IBindable Bindable in Bindables) 
                {
                    if(Bindable.Status == BindingStatus.Error) 
                    {
                        return BindingStatus.Error;
                    } 
                    else if(Bindable.Status == BindingStatus.Unsaved) 
                    {
                        return BindingStatus.Unsaved;    
                    }
                    saved += Bindable.Status == BindingStatus.Saved ? 1 : 0;
                    unbound += Bindable.Status == BindingStatus.Unbound ? 1 : 0;
                }
                if(saved == Bindables.Count - unbound) 
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

        protected List<IBindable> Bindables { get; } = new List<IBindable>();

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (!IsInDesignMode())
            {
                base.OnControlAdded(e);
                Control Control = e.Control;
                if (Control is IBindable Bindable)
                {
                    if (Control.Tag is string Tag)
                    {
                        if (Tag.Split('.', StringSplitOptions.RemoveEmptyEntries) is IEnumerable<string> str && str.Count() > 0)
                        {
                            Bind(Bindable, str);
                            if (!Bindables.Contains(Bindable)) 
                            { 
                                Bindables.Add(Bindable);
                            }
                        }
                    }
                }
            }
        }

        public void SyncAll() 
        {
            foreach(IBindable Bindable in Bindables) 
            {
                Bindable.Sync();
            }
        }

        private void Bind(IBindable Bindable, IEnumerable<string> ObjectName)
        {
            var tmp = ObjectName.ToList();
            tmp.Insert(0, nameof(_DataContext));
            ObjectName = tmp;

            Action<Object> Set;
            Func<Object> Get;
            PropertyChangedNotifier PropertyChangedNotifier;
            string PropertyName;
            Type TargetType;

            GetAccessors(this, ObjectName, out Set, out Get, out PropertyChangedNotifier, out PropertyName, out TargetType);

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

        public event EventHandler<StatusChangedEventArgs>? StatusChanged;

        protected void OnStatusChanged(BindingStatus Status)
        {
            StatusChanged?.Invoke(this, new StatusChangedEventArgs(Status));
        }

        protected void GetAccessors(Object Parent, IEnumerable<string> ObjectName, out Action<Object> Set, out Func<Object> Get, out PropertyChangedNotifier PropertyChangedNotifier, out string PropertyName, out Type TargetType)
        {
            if (Parent.GetType().GetProperty(ObjectName.First()) is PropertyInfo PI)
            {
                if (PI.CanRead)
                {
                    if (ObjectName.Count() > 1)
                    {
                        Object? obj = PI.GetValue(Parent);
                        if (obj is PropertyChangedNotifier NewParent)
                        {
                            GetAccessors(NewParent, ObjectName.Skip(1), out Set, out Get, out PropertyChangedNotifier, out PropertyName, out TargetType);
                        }
                        else if (obj is null && Parent is PropertyChangedNotifier PCN)
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
    }
}
