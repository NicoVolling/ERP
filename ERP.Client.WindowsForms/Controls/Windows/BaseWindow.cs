using ERP.Client.WindowsForms.Controls.Base;
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

        [Category("Darstellung")]
        public Color StatusColor { get => StatusLed.ForeColor; set => StatusLed.ForeColor = value; }

        [Category("Darstellung")]
        public Image Icon { get => IconBox.Image; set { IconBox.Image = value; if (IconBox.Image == null) { IconBox.Visible = false; } else { IconBox.Visible = true; } } }

        public override string Text { get => TitleLabel.Text; set => TitleLabel.Text = value; }

        private bool title_mousedown = false;

        private Point title_mouseposition = new Point(0, 0);

        private BaseForm ParentForm;

        public BaseWindow(BaseForm ParentForm, ContentPanel ContentPanel)
        {
            InitializeComponent();
            this.ParentForm = ParentForm;
            this.ContentPanel = ContentPanel;
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

        bool isMinimized;

        private void btn_minimize_Click(object sender, EventArgs e)
        {
            isMinimized = !isMinimized;
            ContentPanel.OnMinimized(isMinimized);
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            btn_maximize.Image = Dock == DockStyle.Fill ? ERP.Client.WindowsForms.Base.Resources.No_Maximize : ERP.Client.WindowsForms.Base.Resources.Maximize; 
            ContentPanel.OnMaximized(Dock == DockStyle.Fill);
        }
        private void TitleLabel_MouseDown(object sender, MouseEventArgs e)
        {
            title_mousedown = true;
            title_mouseposition = e.Location;
        }

        private void TitleLabel_MouseUp(object sender, MouseEventArgs e)
        {
            title_mousedown = false;
        }

        private void TitleLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if(title_mousedown) 
            {
                Point Destination = new Point(this.Location.X + (e.X - title_mouseposition.X), this.Location.Y + (e.Y - title_mouseposition.Y));

                if (this.Dock == DockStyle.Fill && Destination.Y > 0)
                {
                    this.Dock = DockStyle.None;
                }

                if (Destination.X < 0) 
                {
                    Destination.X = 0;
                }
                else if(Destination.X > this.Parent.Width - this.Width) 
                {
                    Destination.X = this.Parent.Width - this.Width;
                }
                if(Destination.Y < 0) 
                {
                    Destination.Y = 0;
                    this.Dock = DockStyle.Fill;
                }
                else if(Destination.Y > this.Parent.Height - this.Height) 
                {
                    Destination.Y = this.Parent.Height - this.Height;
                }
                this.Location = Destination;
            }
        }
    }
}
