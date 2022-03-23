using ERP.Business.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.Objects
{
    public class Person : BusinessObject
    {
        public string Name { get; set; }

        public string FirstName { get; set; }
    }
}
