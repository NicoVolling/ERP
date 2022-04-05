using ERP.Client.WindowsForms.Controls.Base;

namespace ERP.Client.WindowsForms.Controls.Windows
{
    partial class BaseWindow
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopPanel = new System.Windows.Forms.Panel();
            this.TitleLabel = new ERP.Client.WindowsForms.Controls.Base.TagLabel();
            this.IconBox = new System.Windows.Forms.PictureBox();
            this.StatusLed = new ERP.Client.WindowsForms.Controls.Base.StatusLed();
            this.btn_minimize = new ERP.Client.WindowsForms.Controls.Base.RoundImageButton();
            this.btn_maximize = new ERP.Client.WindowsForms.Controls.Base.RoundImageButton();
            this.btn_close = new ERP.Client.WindowsForms.Controls.Base.RoundImageButton();
            this.ContentsPanel = new System.Windows.Forms.Panel();
            this.ResizePanel = new ERP.Client.WindowsForms.Controls.Base.ResizePanel();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.TopPanel.Controls.Add(this.TitleLabel);
            this.TopPanel.Controls.Add(this.IconBox);
            this.TopPanel.Controls.Add(this.StatusLed);
            this.TopPanel.Controls.Add(this.btn_minimize);
            this.TopPanel.Controls.Add(this.btn_maximize);
            this.TopPanel.Controls.Add(this.btn_close);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopPanel.Size = new System.Drawing.Size(402, 34);
            this.TopPanel.TabIndex = 0;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoWidth = false;
            this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.TitleLabel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.Location = new System.Drawing.Point(52, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Padding = new System.Windows.Forms.Padding(3);
            this.TitleLabel.Size = new System.Drawing.Size(248, 34);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.Text = "Title";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleLabel_MouseDown);
            this.TitleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleLabel_MouseMove);
            this.TitleLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TitleLabel_MouseUp);
            // 
            // IconBox
            // 
            this.IconBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.IconBox.Location = new System.Drawing.Point(18, 0);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(34, 34);
            this.IconBox.TabIndex = 5;
            this.IconBox.TabStop = false;
            this.IconBox.Visible = false;
            // 
            // StatusLed
            // 
            this.StatusLed.Dock = System.Windows.Forms.DockStyle.Left;
            this.StatusLed.Location = new System.Drawing.Point(3, 0);
            this.StatusLed.Name = "StatusLed";
            this.StatusLed.Padding = new System.Windows.Forms.Padding(4);
            this.StatusLed.Size = new System.Drawing.Size(15, 34);
            this.StatusLed.TabIndex = 4;
            this.StatusLed.Text = "statusLed1";
            // 
            // btn_minimize
            // 
            this.btn_minimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_minimize.Image = global::ERP.Client.WindowsForms.Base.Resources.Minimize;
            this.btn_minimize.Location = new System.Drawing.Point(300, 0);
            this.btn_minimize.Name = "btn_minimize";
            this.btn_minimize.Size = new System.Drawing.Size(34, 34);
            this.btn_minimize.TabIndex = 2;
            this.btn_minimize.Click += new System.EventHandler(this.btn_minimize_Click);
            // 
            // btn_maximize
            // 
            this.btn_maximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_maximize.Image = global::ERP.Client.WindowsForms.Base.Resources.Maximize;
            this.btn_maximize.Location = new System.Drawing.Point(334, 0);
            this.btn_maximize.Name = "btn_maximize";
            this.btn_maximize.Size = new System.Drawing.Size(34, 34);
            this.btn_maximize.TabIndex = 1;
            this.btn_maximize.Click += new System.EventHandler(this.btn_maximize_Click);
            // 
            // btn_close
            // 
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.Image = global::ERP.Client.WindowsForms.Base.Resources.Close;
            this.btn_close.Location = new System.Drawing.Point(368, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(34, 34);
            this.btn_close.TabIndex = 0;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentsPanel.Location = new System.Drawing.Point(0, 34);
            this.ContentsPanel.Name = "ContentsPanel";
            this.ContentsPanel.Size = new System.Drawing.Size(402, 211);
            this.ContentsPanel.TabIndex = 1;
            // 
            // ResizePanel
            // 
            this.ResizePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResizePanel.BackColor = System.Drawing.Color.Silver;
            this.ResizePanel.Location = new System.Drawing.Point(394, 237);
            this.ResizePanel.Name = "ResizePanel";
            this.ResizePanel.Resizeable = this;
            this.ResizePanel.Size = new System.Drawing.Size(8, 8);
            this.ResizePanel.TabIndex = 2;
            // 
            // BaseWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.ResizePanel);
            this.Controls.Add(this.ContentsPanel);
            this.Controls.Add(this.TopPanel);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "BaseWindow";
            this.Size = new System.Drawing.Size(402, 245);
            this.TopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel TopPanel;
        private Panel ContentsPanel;
        private Base.RoundImageButton btn_close;
        private Base.RoundImageButton btn_minimize;
        private Base.RoundImageButton btn_maximize;
        private Base.StatusLed StatusLed;
        private Base.TagLabel TitleLabel;
        private PictureBox IconBox;
        private ResizePanel ResizePanel;
    }
}
