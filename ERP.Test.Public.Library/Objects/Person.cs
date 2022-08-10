using ERP.BaseLib.Serialization.Converters;
using ERP.Business.Objects;
using ERP.Business.Objects.Attributes;
using Newtonsoft.Json;

namespace ERP.Test.Public.Library.Objects
{
    public class Person : BusinessObject
    {
        private DateOnly birthday;
        private string firstname;
        private string name;

        [ShowGUI("Geburtstag", 3)]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateOnly Birthday
        {
            get => birthday;
            set { birthday = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(Salary)); }
        }

        [ShowGUI("Vorname", 1)]
        public string Firstname
        {
            get => firstname;
            set { firstname = value; NotifyPropertyChanged(); }
        }

        [ShowGUI("Nachname", 2)]
        public string Name
        {
            get => name;
            set { name = value; NotifyPropertyChanged(); }
        }

        [ShowGUI("Gehalt", 4, "N2", " €")]
        [JsonIgnore]
        public double Salary
        {
            get
            {
                if (Birthday != default)
                {
                    return Math.Round((Birthday.Year + (Birthday.Month * 12.3)) * 1.96 + (Birthday.Day * 2.984), 2);
                }
                return 0;
            }
        }

        [ShowGUI("Person", 0)]
        public override string ToStringValue => base.ToStringValue;

        public override string OnToString()
        {
            string bd = $" ({Birthday})";
            if (Birthday == default) { bd = string.Empty; }
            return $"{Firstname} {Name}{bd}";
        }
    }
}