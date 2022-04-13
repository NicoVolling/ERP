namespace ERP.Client.WindowsForms.Controls.BindableControls
{
    partial class BindableControl
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
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.StatusLed = new ERP.Client.WindowsForms.Controls.Base.StatusLed();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.lbl_Description = new System.Windows.Forms.Label();
            this.panel_btn = new System.Windows.Forms.Panel();
            this.btn = new ERP.Client.WindowsForms.Controls.Base.RoundImageButton();
            this.StatusPanel.SuspendLayout();
            this.panel_btn.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusPanel
            // 
            this.StatusPanel.Controls.Add(this.StatusLed);
            this.StatusPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.StatusPanel.Location = new System.Drawing.Point(351, 0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(2);
            this.StatusPanel.Size = new System.Drawing.Size(32, 76);
            this.StatusPanel.TabIndex = 0;
            // 
            // StatusLed
            // 
            this.StatusLed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusLed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.StatusLed.Location = new System.Drawing.Point(2, 2);
            this.StatusLed.Name = "StatusLed";
            this.StatusLed.Size = new System.Drawing.Size(28, 72);
            this.StatusLed.TabIndex = 0;
            this.StatusLed.TabStop = false;
            this.StatusLed.Text = "statusLed1";
            // 
            // ControlPanel
            // 
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPanel.Location = new System.Drawing.Point(100, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(206, 76);
            this.ControlPanel.TabIndex = 1;
            // 
            // lbl_Description
            // 
            this.lbl_Description.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Description.Location = new System.Drawing.Point(0, 0);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(100, 76);
            this.lbl_Description.TabIndex = 2;
            this.lbl_Description.Text = ":";
            this.lbl_Description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Description.TextChanged += new System.EventHandler(this.lbl_Description_TextChanged);
            // 
            // panel_btn
            // 
            this.panel_btn.Controls.Add(this.btn);
            this.panel_btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_btn.Location = new System.Drawing.Point(306, 0);
            this.panel_btn.Name = "panel_btn";
            this.panel_btn.Padding = new System.Windows.Forms.Padding(4, 4, 9, 4);
            this.panel_btn.Size = new System.Drawing.Size(45, 76);
            this.panel_btn.TabIndex = 3;
            // 
            // btn
            // 
            this.btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn.Image = global::ERP.Client.WindowsForms.Base.Resources.search;
            this.btn.Location = new System.Drawing.Point(4, 4);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(32, 68);
            this.btn.TabIndex = 0;
            this.btn.Text = "roundImageButton1";
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // BindableControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.panel_btn);
            this.Controls.Add(this.lbl_Description);
            this.Controls.Add(this.StatusPanel);
            this.Name = "BindableControl";
            this.Size = new System.Drawing.Size(383, 76);
            this.StatusPanel.ResumeLayout(false);
            this.panel_btn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Base.StatusLed StatusLed;
        private Panel StatusPanel;
        protected Panel ControlPanel;
        private Label lbl_Description;
        private Panel panel_btn;
        private Base.RoundImageButton btn;
    }
}
