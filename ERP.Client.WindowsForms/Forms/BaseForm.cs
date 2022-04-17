using ERP.Client.WindowsForms.Controls.Base;
using ERP.Client.WindowsForms.Controls.BindableControls;
using ERP.Client.WindowsForms.Controls.Windows;
using ERP.Exceptions.ErpExceptions;

namespace ERP.Client.WindowsForms
{
    public partial class BaseForm : Form
    {
        public BaseForm(string ApplicationTitle, Icon MainIcon, Dictionary<string, Dictionary<string, Func<BaseWindow>>> WindowMenus)
        {
            InitializeComponent();
            this.MenuStrip.Renderer = new renderer();
            this.Text = ApplicationTitle;
            this.Icon = MainIcon;
            this.DoubleBuffered = true;
            this.WindowPanel.Click += (s, e) =>
            {
                foreach (BaseWindow BW in Windows)
                {
                    BW.HasFocus = false;
                }
            };

            foreach (KeyValuePair<string, Dictionary<string, Func<BaseWindow>>> kvp in WindowMenus)
            {
                ToolStripMenuItem TSMI = new ToolStripMenuItem();
                TSMI.Text = kvp.Key;
                TSMI.ForeColor = this.ForeColor;
                MenuStrip.Items.Add(TSMI);

                foreach (KeyValuePair<string, Func<BaseWindow>> kvp1 in kvp.Value)
                {
                    try
                    {
                        ToolStripMenuItem TSMI1 = new ToolStripMenuItem();
                        TSMI1.Text = kvp1.Key;
                        TSMI1.BackColor = MenuStrip.BackColor;
                        TSMI1.ForeColor = this.ForeColor;
                        TSMI1.Click += (s, e) =>
                        {
                            OpenWindow(kvp1.Value());
                        };
                        TSMI.DropDownItems.Add(TSMI1);
                    }
                    catch
                    {
                        throw new ErpException("Could not open Window");
                    }
                }
            }
        }

        public IEnumerable<BaseWindow> Windows { get => WindowPanel.Controls.Cast<Control>().Where(o => o is BaseWindow).Select(o => o as BaseWindow); }

        public void OpenWindow(BaseWindow BaseWindow, BaseWindow Blocked = null)
        {
            BaseWindow.SetParent(this);
            WindowPanel.Controls.Add(BaseWindow);
            BaseWindow.BringToFront();

            BaseWindowTitleBar BWTB = new BaseWindowTitleBar(BaseWindow) { Dock = DockStyle.Top };
            BWTB.Icon = BaseWindow.Icon;
            BWTB.Text = BaseWindow.Text;
            BaseWindow.StatusChanged += (s, e) => { BWTB.StatusColor = BindableControl.GetBindingStatusColor(e.Status); };
            BWTB.SetTaskBar();
            WindowListPanel.Controls.Add(BWTB);

            if (Blocked is BaseWindow BWBlocked)
            {
                BWBlocked.ContentPanel.Enabled = false;
                BWBlocked.HasFocusChanged += (s, e) => { if (BWBlocked.HasFocus) { BaseWindow.HasFocus = true; } };

                BaseWindow.Closed += (s, e) => { BWBlocked.ContentPanel.Enabled = true; };
            }

            BaseWindow.ContentPanel.Open(BaseWindow);
        }

        internal void Close(BaseWindow Window)
        {
            WindowPanel.Controls.Remove(Window);
            WindowListPanel.Controls.Remove(WindowListPanel.Controls.Cast<Control>().Where(o => o is BaseWindowTitleBar BWTB && BWTB.BaseWindow == Window).FirstOrDefault());
        }
    }
}