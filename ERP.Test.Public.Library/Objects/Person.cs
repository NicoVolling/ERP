using ERP.BaseLib.Serialization.Converters;
using ERP.Business.Objects;
using ERP.Business.Objects.Attributes;
using ERP.Parsing.Parser;
using Newtonsoft.Json;

namespace ERP.Test.Public.Library.Objects
{
    public class Person : BusinessObject
    {
        private DateOnly birthday;
        private string firstname;
        private string name;
        private double salary;

        [ShowGUI(false, "Geburtstag", 2)]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly Birthday
        {
            get => birthday;
            set { birthday = value; NotifyPropertyChanged(); }
        }

        [ShowGUI(true, "Vorname", 0)]
        public string Firstname
        {
            get => firstname;
            set { firstname = value; NotifyPropertyChanged(); }
        }

        [ShowGUI(true, "Nachname", 1)]
        public string Name
        {
            get => name;
            set { name = value; NotifyPropertyChanged(); }
        }

        [ShowGUI(false, "Gehalt", 3, "N2")]
        public double Salary
        {
            get => salary;
            set { salary = value; NotifyPropertyChanged(); }
        }

        public override string OnToString()
        {
            return $"{Firstname} {Name}";
        }
    }
}