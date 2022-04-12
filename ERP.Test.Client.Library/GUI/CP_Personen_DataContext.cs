using ERP.Business.Objects;
using ERP.Client.WindowsForms.Binding;
using ERP.Test.Public.Library.Objects;

namespace ERP.Test.Client.Library.GUI
{
    public class CP_Personen_DataContext : DataContext
    {
        private List<BusinessObjectIdentifier> businessObjectList = new();
        private Person person;

        public List<BusinessObjectIdentifier> BusinessObjectList
        {
            get => businessObjectList;
            set { businessObjectList = value; NotifyPropertyChanged(nameof(BusinessObjectList)); }
        }

        public Person Person
        {
            get { return person; }
            set { person = value; NotifyPropertyChanged(nameof(Person)); }
        }
    }
}