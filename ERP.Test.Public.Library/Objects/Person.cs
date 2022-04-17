using ERP.Business.Objects;
using ERP.Business.Objects.Attributes;

namespace ERP.Test.Public.Library.Objects
{
    public class Person : BusinessObject
    {
        private DateTime birthday;
        private string firstname;
        private string name;

        [ShowGUI("Geburtstag", 2)]
        public DateTime Birthday
        {
            get => birthday;
            set { birthday = value; NotifyPropertyChanged(); }
        }

        [ShowGUI("Vorname", 0)]
        public string Firstname
        {
            get => firstname;
            set { firstname = value; NotifyPropertyChanged(); }
        }

        [ShowGUI("Nachname", 1)]
        public string Name
        {
            get => name;
            set { name = value; NotifyPropertyChanged(); }
        }

        public override string OnToString()
        {
            return $"{Firstname} {Name}";
        }
    }
}