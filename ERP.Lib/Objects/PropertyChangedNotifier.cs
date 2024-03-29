﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ERP.BaseLib.Objects
{
    public class PropertyChangedNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string Sender = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Sender));
        }
    }
}