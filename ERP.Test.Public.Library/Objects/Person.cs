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
        public string Name { get; set; }

        public string Firstname { get; set; }

        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return $"{Firstname} {Name}";
        }
    }
}
