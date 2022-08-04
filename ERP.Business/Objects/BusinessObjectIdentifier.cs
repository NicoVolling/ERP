namespace ERP.Business.Objects
{
    public class BusinessObjectIdentifier
    {
        public BusinessObjectIdentifier(Guid ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public BusinessObjectIdentifier()
        {
            this.ID = Guid.Empty;
            this.Name = "";
        }

        public static BusinessObjectIdentifier Empty { get => new(BusinessObject.Empty.ID, BusinessObject.Empty.ToString()); }

        public Guid ID { get; set; }

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