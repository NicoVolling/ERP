namespace ERP.Business.Objects
{
    public class BusinessObjectMeta
    {
        public BusinessObjectMeta()
        {
            this.ID = Guid.Empty;
            this.Name = "";
        }

        public Guid ID { get; set; }

        public DateTime LastChanged { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BusinessObjectMeta BOI && BOI.ID == this.ID && BOI.Name == this.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}