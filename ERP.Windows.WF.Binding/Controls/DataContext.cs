using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ERP.Windows.WF.Binding.Controls
{
    public partial class DataContext : Component, INotifyPropertyChanged
    {
        public DataContext()
        {
            InitializeComponent();
        }

        public DataContext(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string Sender = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Sender));
        }
    }
}