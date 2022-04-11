using ERP.Business.Objects;

namespace ERP.Test.Public.Library.Objects
{
    public class Person : BusinessObject
    {
        private DateTime birthday;
        private string firstname;
        private string name;

        public DateTime Birthday
        { get => birthday; set { birthday = value; NotifyPropertyChanged(); } }

        public string Firstname
        { get => firstname; set { firstname = value; NotifyPropertyChanged(); } }

        public string Name
        { get => name; set { name = value; NotifyPropertyChanged(); } }

        public override string ToString()
        {
            return $"{Firstname} {Name}";
        }
    }
}