using ERP.BaseLib.Objecs;

namespace ERP.Client.WindowsForms.Binding
{
    public abstract class DataContext : PropertyChangedNotifier
    {
        public static DataContext Empty { get => new DataContextEmpty(); }

        private class DataContextEmpty : DataContext
        { }
    }
}