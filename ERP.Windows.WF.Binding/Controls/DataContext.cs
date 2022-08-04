using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ERP.Windows.WF.Binding.Controls
{
    public partial class DataContext : Component, INotifyPropertyChanged
    {
        private Guid sECURITY_CODE = Guid.Empty;

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

        public Guid SECURITY_CODE
        { get => sECURITY_CODE; set { sECURITY_CODE = value; NotifyPropertyChanged(nameof(SECURITY_CODE)); } }

        protected void NotifyPropertyChanged([CallerMemberName] string Sender = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Sender));
        }
    }
}