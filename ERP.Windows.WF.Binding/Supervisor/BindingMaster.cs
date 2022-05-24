using ERP.Business.Objects.Attributes;
using ERP.Exceptions.ErpExceptions;
using ERP.Parsing.Parser;
using ERP.Windows.WF.Base;
using ERP.Windows.WF.Binding.Controls;
using System.ComponentModel;
using System.Reflection;

namespace ERP.Windows.WF.Binding.Supervisor
{
    public class BindingMaster
    {
        public BindingMaster(IBindingParent BindingParent)
        {
            this.DataContext = BindingParent.DataContext;
            BindingParent.ControlAdded += Control_ControlAdded;
        }

        public List<IBindable> Bindables { get; } = new List<IBindable>();

        private DataContext DataContext { get; }

        public static void Bind(IBindable Bindable, DataContext DataContext, IEnumerable<string> ObjectName = null)
        {
            if (ObjectName == null)
            {
                ObjectName = Bindable.BindingDestination.Split(".", StringSplitOptions.RemoveEmptyEntries);
            }

            GetAccessors(DataContext, ObjectName, out Action<object> Set, out Func<object> Get, out INotifyPropertyChanged PropertyChangedNotifier, out string PropertyName, out Type TargetType, out ShowGUIAttribute ShowGUIAttribute);

            if (Set == null && Get == null && PropertyChangedNotifier != null && PropertyName != null && TargetType != null)
            {
                PropertyChangedNotifier.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == PropertyName)
                    {
                        Bind(Bindable, DataContext, ObjectName);
                    }
                };
            }
            else if (Set != null && Get != null && PropertyChangedNotifier != null && PropertyName != null && TargetType != null)
            {
                BindControl(Bindable, Get, Set, PropertyChangedNotifier, PropertyName, TargetType, DataContext, ShowGUIAttribute);
            }
            else
            {
                throw new ErpException("Binding failed");
            }
            Bindable.OnBound(DataContext);
        }

        public static void BindControl(IBindable Bindable, Func<Object> Get, Action<Object> Set, INotifyPropertyChanged PropertyChangedNotifier, string PropertyName, Type TargetType, DataContext DataContext, ShowGUIAttribute ShowGUIAttribute)
        {
            Bindable.UserFriendlyName = ShowGUIAttribute?.UserFriendlyName;

            IParser Parser = IParser.GetParser(Bindable.OriginType, TargetType);

            Func<Object> OnGet = () =>
            {
                try
                {
                    bool Error;
                    Object getResult = Parser.Parse(Get(), Bindable.OriginType, ShowGUIAttribute?.FormatOptions, out Error);
                    if (!Error)
                    {
                        if (Parser.IsDefault(getResult))
                        {
                            Bindable.Status = InputStatus.Null;
                        }
                        else
                        {
                            Bindable.Status = Base.InputStatus.OK;
                        }
                        return getResult;
                    }
                    throw new ErpException("Parsingerror");
                }
                catch
                {
                    throw;
                }
            };

            Action<Object> OnSet = (Object) =>
            {
                try
                {
                    bool Error;
                    Object setResult = Parser.Parse(Object, TargetType, ShowGUIAttribute?.FormatOptions, out Error);
                    if (!Error)
                    {
                        if (Parser.IsDefault(setResult))
                        {
                            Bindable.Status = InputStatus.Null;
                        }
                        else
                        {
                            Bindable.Status = InputStatus.OK;
                        }
                        Set(setResult);
                    }
                    else
                    {
                        Bindable.Status = InputStatus.Error;
                    }
                }
                catch
                {
                    throw;
                }
            };

            bool mayGet = true;
            bool maySet = true;

            Bindable.ControlValueChanged += (s, e) =>
            {
                if (maySet)
                {
                    Object ControlValue = Bindable.GetControlValue();
                    mayGet = false;
                    if (Bindable is BindableControlBase BCB)
                    {
                        BCB.ValueChanged = true;
                    }
                    OnSet(ControlValue);
                    mayGet = true;
                }
            };

            Action<object, PropertyChangedEventArgs> PropertyChanged = (s, e) =>
            {
                if (Bindable.IsDisposed)
                {
                    PropertyChanged = null; return;
                }
                if (e.PropertyName == PropertyName)
                {
                    if (mayGet)
                    {
                        maySet = false;
                        Object Value = OnGet();
                        maySet = true;
                        Bindable.SetControlValue(Value);
                        if (Bindable is BindableControlBase BCB)
                        {
                            BCB.ValueChanged = false;
                        }
                    }
                }
            };

            PropertyChangedNotifier.PropertyChanged += (s, e) =>
            {
                PropertyChanged?.Invoke(s, e);
            };

            Bindable.FormatRequest += (s, e) =>
            {
                Bindable.SetControlValue(OnGet());
            };

            PropertyChanged(null, new PropertyChangedEventArgs(PropertyName));
        }

        public static bool IsInDesignMode()
        {
            if (Application.ExecutablePath.IndexOf("DesignToolsServer.exe", StringComparison.OrdinalIgnoreCase) > -1)
            {
                return true;
            }
            return false;
        }

        protected void ControllAddedHandling(Control Control)
        {
            if (!IsInDesignMode())
            {
                if (Control is IBindable Bindable)
                {
                    if (Bindable.BindingDestination is string BindingDestination)
                    {
                        if (BindingDestination.Split('.', StringSplitOptions.RemoveEmptyEntries) is IEnumerable<string> str && str.Any())
                        {
                            if (!Bindables.Contains(Bindable))
                            {
                                Bindables.Add(Bindable);
                                Bind(Bindable, DataContext, str);
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

        private static void GetAccessors(Object Parent, IEnumerable<string> ObjectName,
                    out Action<Object> Set,
            out Func<Object> Get,
            out INotifyPropertyChanged PropertyChangedNotifier,
            out string PropertyName,
            out Type TargetType,
            out ShowGUIAttribute ShowGUIAttribute)
        {
            if (Parent.GetType().GetProperties().Where(o => o.Name.Equals(ObjectName.First())).FirstOrDefault() is PropertyInfo PI)
            {
                if (PI.CanRead)
                {
                    if (ObjectName.Count() > 1)
                    {
                        Object obj = PI.GetValue(Parent);
                        if (obj is INotifyPropertyChanged NewParent)
                        {
                            GetAccessors(NewParent, ObjectName.Skip(1), out Set, out Get, out PropertyChangedNotifier, out PropertyName, out TargetType, out ShowGUIAttribute);
                        }
                        else if (obj is null && Parent is INotifyPropertyChanged PCN)
                        {
                            Set = null;
                            Get = null;
                            PropertyChangedNotifier = PCN;
                            PropertyName = ObjectName.First();
                            TargetType = PI.PropertyType;
                            ShowGUIAttribute = null;
                        }
                        else
                        {
                            throw new ErpException("Couldnt get Value, incorrect type or null");
                        }
                    }
                    else
                    {
                        if (Parent is INotifyPropertyChanged PCN)
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
                            if (PI.GetCustomAttribute(typeof(ShowGUIAttribute)) is ShowGUIAttribute SGA)
                            {
                                ShowGUIAttribute = SGA;
                            }
                            else
                            {
                                ShowGUIAttribute = null;
                            }
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

        private void Control_ControlAdded(object sender, ControlEventArgs e)
        {
            ControllAddedHandling(e.Control);
        }
    }
}