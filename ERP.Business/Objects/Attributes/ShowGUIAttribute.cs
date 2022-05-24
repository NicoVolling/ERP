namespace ERP.Business.Objects.Attributes
{
    public class ShowGUIAttribute : Attribute
    {
        public ShowGUIAttribute(string UserFriendlyName, int ID, string FormatOptions = "", string Suffix = "")
        {
            this.UserFriendlyName = UserFriendlyName;
            this.ID = ID;
            this.FormatOptions = FormatOptions;
            this.Suffix = Suffix;
        }

        public ShowGUIAttribute()
        {
        }

        public string FormatOptions { get; set; }
        public int ID { get; set; }
        public string Suffix { get; set; }
        public string UserFriendlyName { get; set; }
    }
}