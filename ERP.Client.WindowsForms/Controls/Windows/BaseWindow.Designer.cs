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
            this.btn_minimize = new ERP.Client.WindowsForms.Controls.Base.RoundImageButton();
            this.btn_maximize = new ERP.Client.WindowsForms.Controls.Base.RoundImageButton();
            this.btn_close = new ERP.Client.WindowsForms.Controls.Base.RoundImageButton();
            this.ContentsPanel = new System.Windows.Forms.Panel();
            this.ResizePanel = new ERP.Client.WindowsForms.Controls.Base.ResizePanel();
            this.TitleBar = new ERP.Client.WindowsForms.Controls.Base.BaseWindowTitleBar(this);
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.TopPanel.Controls.Add(this.TitleBar);
            this.TopPanel.Controls.Add(this.btn_minimize);
            this.TopPanel.Controls.Add(this.btn_maximize);
            this.TopPanel.Controls.Add(this.btn_close);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(1, 1);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopPanel.Size = new System.Drawing.Size(400, 34);
            this.TopPanel.TabIndex = 0;
            // 
            // btn_minimize
            // 
            this.btn_minimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_minimize.Image = global::ERP.Client.WindowsForms.Base.Resources.Minimize;
            this.btn_minimize.Location = new System.Drawing.Point(298, 0);
            this.btn_minimize.Name = "btn_minimize";
            this.btn_minimize.Size = new System.Drawing.Size(34, 34);
            this.btn_minimize.TabIndex = 2;
            this.btn_minimize.Click += new System.EventHandler(this.btn_minimize_Click);
            // 
            // btn_maximize
            // 
            this.btn_maximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_maximize.Image = global::ERP.Client.WindowsForms.Base.Resources.Maximize;
            this.btn_maximize.Location = new System.Drawing.Point(332, 0);
            this.btn_maximize.Name = "btn_maximize";
            this.btn_maximize.Size = new System.Drawing.Size(34, 34);
            this.btn_maximize.TabIndex = 1;
            this.btn_maximize.Click += new System.EventHandler(this.btn_maximize_Click);
            // 
            // btn_close
            // 
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.Image = global::ERP.Client.WindowsForms.Base.Resources.Close;
            this.btn_close.Location = new System.Drawing.Point(366, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(34, 34);
            this.btn_close.TabIndex = 0;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // ContentsPanel
            // 
            this.ContentsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ContentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentsPanel.Location = new System.Drawing.Point(1, 35);
            this.ContentsPanel.Name = "ContentsPanel";
            this.ContentsPanel.Size = new System.Drawing.Size(400, 209);
            this.ContentsPanel.TabIndex = 1;
            this.ContentsPanel.Padding = new System.Windows.Forms.Padding(8);
            // 
            // ResizePanel
            // 
            this.ResizePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResizePanel.BackColor = System.Drawing.Color.Silver;
            this.ResizePanel.Location = new System.Drawing.Point(393, 236);
            this.ResizePanel.Name = "ResizePanel";
            this.ResizePanel.Resizeable = this;
            this.ResizePanel.Size = new System.Drawing.Size(8, 8);
            this.ResizePanel.TabIndex = 2;
            // 
            // TitleBar
            // 
            this.TitleBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleBar.Location = new System.Drawing.Point(3, 0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(295, 34);
            this.TitleBar.TabIndex = 3;
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
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(402, 245);
            this.TopPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel TopPanel;
        private Panel ContentsPanel;
        private Base.RoundImageButton btn_close;
        private Base.RoundImageButton btn_minimize;
        private Base.RoundImageButton btn_maximize;
        private ResizePanel ResizePanel;
        private BaseWindowTitleBar TitleBar;
    }
}
