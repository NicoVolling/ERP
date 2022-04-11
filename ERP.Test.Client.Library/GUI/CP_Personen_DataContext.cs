using ERP.Client.WindowsForms.Binding;
using ERP.Test.Public.Library.Objects;

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