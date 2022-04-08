using ERP.BaseLib.Objecs;
using ERP.Client.WindowsForms.Base;
using ERP.Client.WindowsForms.Controls.BindableControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Binding
{
    public interface IBindable
    {
        public Func<Object> Get { get; set; }
        public Action<Object> Set { get; set; }

        public Type TargetType { get; set; }

        public void Bind(Func<Object> Get, Action<Object> Set, PropertyChangedNotifier PropertyChangedNotifier, string PropertyName, Type TargetType);

        public static void Bind(IBindable Bindable, Func<Object> Get, Action<Object> Set, PropertyChangedNotifier PropertyChangedNotifier, string PropertyName, Type TargetType) 
        {
            Bindable.TargetType = TargetType;
            Bindable.Set = (Object) => 
            {
                try
                {
                    Set(Bindable.ParseToObject(Object));
                }
                catch
                {
                    Set(Bindable.ParseToObject(null));
                }
            };
            Bindable.Get = () => 
            {
                try
                {
                    return Bindable.ParseFromObject(Get());
                }
                catch
                {
                    return Bindable.ParseFromObject(null);
                }
            };
            PropertyChangedNotifier.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == PropertyName)
                {
                    Bindable.LoadData();
                }
            };
            Bindable.LoadData();
            if (Bindable.Status != BindingStatus.NullOrDefault)
            {
                Bindable.Status = BindingStatus.Unsaved;
            }
        }

        public BindingStatus Status { get; set; }

        public void Sync();

        public void SaveData();

        public void LoadData();

        public event EventHandler<StatusChangedEventArgs>? StatusChanged;

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

        public Object ParseFromObject(Object Value);

        public Object ParseToObject(Object Value);
    }
}
