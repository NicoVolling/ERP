using ERP.Client.WindowsForms.Base;
using ERP.Client.WindowsForms.Controls.Base;
using ERP.Client.WindowsForms.Controls.Windows;
using ERP.Exceptions.ErpExceptions;

namespace ERP.Client.WindowsForms
{
    public partial class BaseForm : Form
    {

        public BaseForm(string ApplicationTitle, Icon MainIcon, Dictionary<string, Dictionary<string, BaseWindow>> WindowMenus)
        {
            InitializeComponent();
            this.MenuStrip.Renderer = new renderer();
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

            foreach(var kvp in WindowMenus) 
            {
                ToolStripMenuItem TSMI = new ToolStripMenuItem();
                TSMI.Text = kvp.Key;
                TSMI.ForeColor = this.ForeColor;
                MenuStrip.Items.Add(TSMI);

                foreach(var kvp1 in kvp.Value) 
                {
                    try 
                    {
                        ToolStripMenuItem TSMI1 = new ToolStripMenuItem();
                        TSMI1.Text = kvp1.Key;
                        TSMI1.BackColor = MenuStrip.BackColor;
                        TSMI1.ForeColor = this.ForeColor;
                        TSMI1.Click += (s, e) =>
                        {
                            Object? instance = Activator.CreateInstance(kvp1.Value.ContentPanel.GetType());
                            if (instance is ContentPanel CP)
                            {
                                BaseWindow BW = new BaseWindow(CP);
                                BW.Icon = kvp1.Value.Icon;
                                BW.Text = kvp1.Value.Text;
                                BW.StatusColor = kvp1.Value.StatusColor;

                                OpenWindow(BW);
                            }
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

        public BaseWindow OpenWindow(ContentPanel ContentPanel) 
        {
            BaseWindow BaseWindow = new BaseWindow(ContentPanel);
            ContentPanel.Open(BaseWindow);
            
            OpenWindow(BaseWindow);

            return BaseWindow;
        }

        public void OpenWindow(BaseWindow BaseWindow) 
        {
            BaseWindow.SetParent(this);
            WindowPanel.Controls.Add(BaseWindow);
            BaseWindow.BringToFront();

            BaseWindowTitleBar BWTB = new BaseWindowTitleBar(BaseWindow) { Dock = DockStyle.Top };
            BWTB.Icon = BaseWindow.Icon;
            BWTB.StatusColor = BaseWindow.StatusColor;
            BWTB.Text = BaseWindow.Text;
            BWTB.SetTaskBar();
            WindowListPanel.Controls.Add(BWTB);
        }

        internal void Close(BaseWindow Window) 
        {
            WindowPanel.Controls.Remove(Window);
            WindowListPanel.Controls.Remove(WindowListPanel.Controls.Cast<Control>().Where(o => o is BaseWindowTitleBar BWTB && BWTB.BaseWindow == Window).FirstOrDefault());
        }
    }
}