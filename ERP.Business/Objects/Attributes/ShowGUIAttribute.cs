namespace ERP.Business.Objects.Attributes
{
    public class ShowGUIAttribute : Attribute
    {
        public ShowGUIAttribute(bool Required, string UserFriendlyName, int ID, string FormatOptions = "")
        {
            this.UserFriendlyName = UserFriendlyName;
            this.ID = ID;
            this.FormatOptions = FormatOptions;
            this.Required = Required;
        }

        public string FormatOptions { get; }
        public int ID { get; }
        public bool Required { get; }
        public string UserFriendlyName { get; }
    }
}