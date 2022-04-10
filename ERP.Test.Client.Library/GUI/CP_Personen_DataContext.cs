using ERP.Client.WindowsForms.Binding;
using ERP.Test.Public.Library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.Client.Library.GUI
{
    public class CP_Personen_DataContext : DataContext
    {
        private Person person;

        public Person Person
        {
            get { return person; }
            set { person = value; NotifyPropertyChanged(); }
        }

    }
}
