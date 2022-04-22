using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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