using ERP.Client.WindowsForms.Controls.Base;
using ERP.Exceptions.ErpExceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Client.WindowsForms.Controls.Windows
{
    public partial class BaseWindow : UserControl
    {
        public ContentPanel? ContentPanel { 
            get => ContentsPanel.Controls.Cast<Control>().FirstOrDefault(o => o is ContentPanel) as ContentPanel; 
            protected set 
            { 
                if (value is null) 
                { 
                    ContentsPanel.Controls.Clear(); 
                } 
                else 
                {
                    ContentsPanel.Controls.Clear();
                    ContentsPanel.Controls.Add(value);
                    value.Dock = DockStyle.Fill;
                } 
            }
        }

        public bool HasFocus
        {
            get
            {
                return this.BackColor == Color.FromArgb(255, 255, 255);
            }
            set
            {
                if (value)
                {
                    foreach (BaseWindow BW in ParentForm.Windows)
                    {
                        BW.HasFocus = false;
                    }
                    this.BringToFront();
                }
                this.BackColor = value ? Color.FromArgb(255, 255, 255) : Color.FromArgb(40, 40, 40);
            }
        }

        [Category("Darstellung")]
        public Color StatusColor { get => this.TitleBar.StatusColor; set => this.TitleBar.StatusColor = value; }

        [Category("Darstellung")]
        public Image Icon { get => this.TitleBar.Icon; set { this.TitleBar.Icon = value; } }

        public override string Text { get => this.TitleBar.Text; set => this.TitleBar.Text = value; }

        public BaseForm ParentForm { get { if (parentForm == null) { throw new ErpException("Cannot access ParentForm without setting it"); } return parentForm; } set => parentForm = value; }

        private BaseForm parentForm;

        public BaseWindow(ContentPanel ContentPanel)
        {
            InitializeComponent();
            this.ContentPanel = ContentPanel;
            CreateEvents(this);
        }

        public void SetParent(BaseForm ParentForm) 
        {
            this.ParentForm = ParentForm;
        }

        private void CreateEvents(Control Parent) 
        {
            Parent.MouseDown += (s, e) => 
            {
                HasFocus = true;
            };
            Parent.GotFocus += (s, e) =>
            {
                HasFocus = true;
            };
            Parent.ControlAdded += (s, e) => 
            {
                CreateEvents(e.Control);
            };
            foreach(Control Control in Parent.Controls) 
            {
                CreateEvents(Control);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void Close() 
        {
            ParentForm.Close(this);
            ContentPanel.OnClosed();
        }

        private void btn_maximize_Click(object sender, EventArgs e)
        {
            SwitchMaximize();
        }

        public void SwitchMaximize() 
        {
            this.Dock = Dock == DockStyle.Fill ? DockStyle.None : DockStyle.Fill;
        }

        private void btn_minimize_Click(object sender, EventArgs e)
        {
            Visible = false;
            HasFocus = false;
            ContentPanel.OnMinimized(Visible);
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            btn_maximize.Image = Dock == DockStyle.Fill ? ERP.Client.WindowsForms.Base.Resources.No_Maximize : ERP.Client.WindowsForms.Base.Resources.Maximize; 
            ContentPanel.OnMaximized(Dock == DockStyle.Fill);
        }

    }
}
