using ERP.Client.WindowsForms.Binding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Controls.Base
{
    public class BaseControl : UserControl
    {
        public ContentPanel ContentPanel { get; private set; }

        public void SetContentPanel(ContentPanel ContentPanel)
        {
            this.ContentPanel = ContentPanel;
        }
    }
}