namespace ERP.Business.Objects.Attributes
{
    public class ShowGUIAttribute : Attribute
    {
        public ShowGUIAttribute(string UserFriendlyName, int ID, string FormatOptions = "")
        {
            this.UserFriendlyName = UserFriendlyName;
            this.ID = ID;
            this.FormatOptions = FormatOptions;
        }

        public string FormatOptions { get; }
        public int ID { get; }
        public string UserFriendlyName { get; }
    }
}