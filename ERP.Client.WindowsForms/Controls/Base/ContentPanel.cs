using ERP.Client.WindowsForms.Controls.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Controls.Base
{
    public class ContentPanel : UserControl
    {
        protected BaseWindow BaseWindow { get; private set; }

        public ContentPanel() 
        {
        }

        internal void Open(BaseWindow BaseWindow) 
        {
            this.BaseWindow = BaseWindow;
            OnOpened();
        }

        public virtual void OnClosed() 
        {
        
        }

        public virtual void OnMinimized(bool Minimize) 
        {
        
        }

        public virtual void OnMaximized(bool Maximize) 
        {
        
        }

        protected virtual void OnOpened() 
        {
        
        }
    }
}
