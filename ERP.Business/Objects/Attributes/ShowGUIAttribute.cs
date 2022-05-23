namespace ERP.Business.Objects.Attributes
{
    public class ShowGUIAttribute : Attribute
    {
        public ShowGUIAttribute(string UserFriendlyName, int ID)
        {
            this.UserFriendlyName = UserFriendlyName;
            this.ID = ID;
        }

        public int ID { get; }

        public string UserFriendlyName { get; }
    }
}