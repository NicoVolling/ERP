using ERP.Client.WindowsForms.Base;
using ERP.Client.WindowsForms.Controls.Base;
using ERP.Client.WindowsForms.Controls.Windows;

namespace ERP.Client.WindowsForms
{
    public partial class BaseForm : Form
    {

        public BaseForm(string ApplicationTitle, Icon MainIcon)
        {
            InitializeComponent();
            this.Text = ApplicationTitle;
            this.Icon = MainIcon;
            this.DoubleBuffered = true;
        }

        public BaseWindow OpenWindow(ContentPanel Panel) 
        {
            BaseWindow BW = new BaseWindow(this, Panel);
            BW.StatusColor = Color.Red;
            WindowPanel.Controls.Add(BW);
            BW.BringToFront();
            return BW;
        }

        internal void Close(BaseWindow Window) 
        {
            WindowPanel.Controls.Remove(Window);
        }
    }
}