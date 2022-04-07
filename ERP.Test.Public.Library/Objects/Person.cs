using ERP.Business.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.Public.Library.Objects
{
    public class Person : BusinessObject
    {
        private string name;
        private string firstname;
        private DateTime birthday;

        public string Name { get => name; set { name = value; NotifyPropertyChanged(); } }

        public string Firstname { get => firstname; set { firstname = value; NotifyPropertyChanged(); } }

        public DateTime Birthday { get => birthday; set { birthday = value; NotifyPropertyChanged(); } }

        public override string ToString()
        {
            return $"{Firstname} {Name}";
        }
    }
}
