namespace ERP.Windows.WF.Binding.Controls
{
    partial class BindableControlBase
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.lbl_access = new System.Windows.Forms.Label();
            this.panel_button = new System.Windows.Forms.Panel();
            this.btn_search = new ERP.Windows.WF.Base.Controls.RoundImageButton();
            this.lbl_description = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StatusLed = new ERP.Windows.WF.Base.Controls.StatusLed();
            this.MainPanel.SuspendLayout();
            this.panel_button.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MainPanel.Controls.Add(this.ControlPanel);
            this.MainPanel.Controls.Add(this.lbl_access);
            this.MainPanel.Controls.Add(this.panel_button);
            this.MainPanel.Controls.Add(this.lbl_description);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(3, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(258, 31);
            this.MainPanel.TabIndex = 0;
            // 
            // ControlPanel
            // 
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPanel.Location = new System.Drawing.Point(86, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(125, 31);
            this.ControlPanel.TabIndex = 2;
            // 
            // lbl_access
            // 
            this.lbl_access.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_access.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_access.ForeColor = System.Drawing.Color.Salmon;
            this.lbl_access.Location = new System.Drawing.Point(86, 0);
            this.lbl_access.Name = "lbl_access";
            this.lbl_access.Size = new System.Drawing.Size(125, 31);
            this.lbl_access.TabIndex = 4;
            this.lbl_access.Text = "Zugriff verweigert";
            this.lbl_access.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_access.Visible = false;
            // 
            // panel_button
            // 
            this.panel_button.Controls.Add(this.btn_search);
            this.panel_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_button.Location = new System.Drawing.Point(211, 0);
            this.panel_button.Name = "panel_button";
            this.panel_button.Size = new System.Drawing.Size(29, 31);
            this.panel_button.TabIndex = 3;
            // 
            // btn_search
            // 
            this.btn_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_search.Image = global::ERP.Windows.WF.Binding.Properties.Resources.search;
            this.btn_search.Location = new System.Drawing.Point(0, 0);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(29, 31);
            this.btn_search.TabIndex = 0;
            this.btn_search.Text = "roundImageButton1";
            // 
            // lbl_description
            // 
            this.lbl_description.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_description.Location = new System.Drawing.Point(0, 0);
            this.lbl_description.Name = "lbl_description";
            this.lbl_description.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbl_description.Size = new System.Drawing.Size(86, 31);
            this.lbl_description.TabIndex = 1;
            this.lbl_description.Text = "Description:";
            this.lbl_description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.StatusLed);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(240, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(18, 31);
            this.panel1.TabIndex = 0;
            // 
            // StatusLed
            // 
            this.StatusLed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusLed.Location = new System.Drawing.Point(4, 4);
            this.StatusLed.Name = "StatusLed";
            this.StatusLed.Size = new System.Drawing.Size(10, 23);
            this.StatusLed.TabIndex = 0;
            this.StatusLed.Text = "statusLed1";
            // 
            // BindableControlBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.MainPanel);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "BindableControlBase";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Size = new System.Drawing.Size(261, 31);
            this.MainPanel.ResumeLayout(false);
            this.panel_button.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panel1;
        private Base.Controls.StatusLed StatusLed;
        private Panel MainPanel;
        private Label lbl_description;
        protected Panel ControlPanel;
        private Panel panel_button;
        private Base.Controls.RoundImageButton btn_search;
        private Label lbl_access;
    }
}
