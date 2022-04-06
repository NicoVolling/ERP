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
            this.WindowPanel.Click += (s, e) => 
            {
                foreach (BaseWindow BW in Windows ) 
                {
                    BW.HasFocus = false;
                }
            };
        }

        public IEnumerable<BaseWindow> Windows { get => WindowPanel.Controls.Cast<Control>().Where(o => o is BaseWindow).Select(o => o as BaseWindow); }

        public BaseWindow OpenWindow(ContentPanel Panel) 
        {
            BaseWindow BW = new BaseWindow(this, Panel);
            Panel.Open(BW);
            WindowPanel.Controls.Add(BW);
            BW.BringToFront();

            BaseWindowTitleBar BWTB = new BaseWindowTitleBar(BW) { Dock = DockStyle.Top };
            BWTB.Icon = BW.Icon;
            BWTB.StatusColor = BW.StatusColor;
            BWTB.Text = BW.Text;
            BWTB.SetTaskBar();
            WindowListPanel.Controls.Add(BWTB);

            return BW;
        }

        internal void Close(BaseWindow Window) 
        {
            WindowPanel.Controls.Remove(Window);
            WindowListPanel.Controls.Remove(WindowListPanel.Controls.Cast<Control>().Where(o => o is BaseWindowTitleBar BWTB && BWTB.BaseWindow == Window).FirstOrDefault());
        }
    }
}