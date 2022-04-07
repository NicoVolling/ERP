namespace ERP.Client.WindowsForms.Controls.BindableControls
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
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.StatusLed = new ERP.Client.WindowsForms.Controls.Base.StatusLed();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.StatusPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusPanel
            // 
            this.StatusPanel.Controls.Add(this.StatusLed);
            this.StatusPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.StatusPanel.Location = new System.Drawing.Point(307, 0);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Padding = new System.Windows.Forms.Padding(2);
            this.StatusPanel.Size = new System.Drawing.Size(76, 76);
            this.StatusPanel.TabIndex = 0;
            // 
            // StatusLed
            // 
            this.StatusLed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusLed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.StatusLed.Location = new System.Drawing.Point(2, 2);
            this.StatusLed.Name = "StatusLed";
            this.StatusLed.Size = new System.Drawing.Size(72, 72);
            this.StatusLed.TabIndex = 0;
            this.StatusLed.Text = "statusLed1";
            // 
            // ControlPanel
            // 
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(307, 76);
            this.ControlPanel.TabIndex = 1;
            // 
            // BindableControlBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.StatusPanel);
            this.Name = "BindableControlBase";
            this.Size = new System.Drawing.Size(383, 76);
            this.StatusPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Base.StatusLed StatusLed;
        private Panel StatusPanel;
        protected Panel ControlPanel;
    }
}
