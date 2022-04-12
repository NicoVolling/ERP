using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Business.Objects
{
    public class BusinessObjectIdentifier
    {
        public BusinessObjectIdentifier(int ID = -1, string Name = "")
        {
            this.ID = ID;
            this.Name = Name;
        }

        public static BusinessObjectIdentifier Empty { get => new(BusinessObject.Empty.ID, BusinessObject.Empty.ToString()); }

        public int ID { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BusinessObjectIdentifier BOI && BOI.ID == this.ID && BOI.Name == this.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}