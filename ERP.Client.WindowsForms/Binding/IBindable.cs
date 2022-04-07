using ERP.BaseLib.Objecs;
using ERP.Client.WindowsForms.Base;
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

        public void Bind(Func<Object> Get, Action<Object> Set, PropertyChangedNotifier PropertyChangedNotifier, string PropertyName);

        public static void Bind(IBindable Bindable, Func<Object> Get, Action<Object> Set, PropertyChangedNotifier PropertyChangedNotifier, string PropertyName) 
        {
            Bindable.Set = Set;
            Bindable.Get = Get;
            PropertyChangedNotifier.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == PropertyName)
                {
                    Bindable.LoadData();
                }
            };
            Bindable.LoadData();
        }

        public void Sync();

        public void SaveData();

        public void LoadData();
    }
}
