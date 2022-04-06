using ERP.Client.WindowsForms.Controls.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Controls.Base
{
    internal class BaseWindowTitleBar : UserControl
    {
        private TagLabel TitleLabel;
        private PictureBox IconBox;
        private StatusLed StatusLed;

        internal BaseWindow BaseWindow;

        public BaseWindowTitleBar(BaseWindow BaseWindow) 
        {
            InitializeComponent();
            this.BaseWindow = BaseWindow;
        }

        private void InitializeComponent()
        {
            this.TitleLabel = new ERP.Client.WindowsForms.Controls.Base.TagLabel();
            this.IconBox = new System.Windows.Forms.PictureBox();
            this.StatusLed = new ERP.Client.WindowsForms.Controls.Base.StatusLed();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoWidth = false;
            this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.TitleLabel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.Location = new System.Drawing.Point(49, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Padding = new System.Windows.Forms.Padding(3);
            this.TitleLabel.Size = new System.Drawing.Size(231, 34);
            this.TitleLabel.TabIndex = 6;
            this.TitleLabel.Text = "Title";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleLabel.MouseDown += TitleLabel_MouseDown;
            this.TitleLabel.MouseUp += TitleLabel_MouseUp;
            this.TitleLabel.MouseMove += TitleLabel_MouseMove;
            // 
            // IconBox
            // 
            this.IconBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.IconBox.Location = new System.Drawing.Point(15, 0);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(34, 34);
            this.IconBox.TabIndex = 8;
            this.IconBox.TabStop = false;
            this.IconBox.Visible = false;
            // 
            // StatusLed
            // 
            this.StatusLed.Dock = System.Windows.Forms.DockStyle.Left;
            this.StatusLed.Location = new System.Drawing.Point(0, 0);
            this.StatusLed.Name = "StatusLed";
            this.StatusLed.Padding = new System.Windows.Forms.Padding(4);
            this.StatusLed.Size = new System.Drawing.Size(15, 34);
            this.StatusLed.TabIndex = 7;
            this.StatusLed.Text = "statusLed1";
            // 
            // BaseWindowTitleBar
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.IconBox);
            this.Controls.Add(this.StatusLed);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "BaseWindowTitleBar";
            this.Size = new System.Drawing.Size(280, 34);
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).EndInit();
            this.ResumeLayout(false);

        }

        public void SetTaskBar() 
        {
            this.TitleLabel.MouseDown -= TitleLabel_MouseDown;
            this.TitleLabel.MouseUp -= TitleLabel_MouseUp;
            this.TitleLabel.MouseMove -= TitleLabel_MouseMove;
            this.TitleLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TitleLabel.Click += (s, e) => 
            { 
                if (BaseWindow.HasFocus)
                {
                    BaseWindow.HasFocus = false;
                    BaseWindow.Visible = false;
                }
                else 
                {
                    BaseWindow.HasFocus = true;
                    BaseWindow.Visible = true;
                }
            };
        }

        private bool title_mousedown = false;

        private Point title_mouseposition = new Point(0, 0);

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
            if (title_mousedown)
            {
                Point Destination = new Point(BaseWindow.Location.X + (e.X - title_mouseposition.X), BaseWindow.Location.Y + (e.Y - title_mouseposition.Y));

                if (BaseWindow.Dock == DockStyle.Fill && Destination.Y > 0)
                {
                    BaseWindow.Dock = DockStyle.None;
                }

                if (Destination.X < 0)
                {
                    Destination.X = 0;
                }
                else if (Destination.X > BaseWindow.Parent.Width - BaseWindow.Width)
                {
                    Destination.X = BaseWindow.Parent.Width - BaseWindow.Width;
                }
                if (Destination.Y < 0)
                {
                    Destination.Y = 0;
                    BaseWindow.Dock = DockStyle.Fill;
                }
                else if (Destination.Y > BaseWindow.Parent.Height - BaseWindow.Height)
                {
                    Destination.Y = BaseWindow.Parent.Height - BaseWindow.Height;
                }
                BaseWindow.Location = Destination;
            }
        }

        [Category("Darstellung")]
        public Color StatusColor { get => StatusLed.ForeColor; set => StatusLed.ForeColor = value; }

        [Category("Darstellung")]
        public Image Icon { get => IconBox.Image; set { IconBox.Image = value; if (IconBox.Image == null) { IconBox.Visible = false; } else { IconBox.Visible = true; } } }

        public override string Text { get => TitleLabel.Text; set => TitleLabel.Text = value; }
    }
}
