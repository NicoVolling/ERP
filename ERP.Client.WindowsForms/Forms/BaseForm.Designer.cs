namespace ERP.Client.WindowsForms
{
    partial class BaseForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.WindowPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // WindowPanel
            // 
            this.WindowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WindowPanel.Location = new System.Drawing.Point(0, 0);
            this.WindowPanel.Name = "WindowPanel";
            this.WindowPanel.Size = new System.Drawing.Size(800, 516);
            this.WindowPanel.TabIndex = 0;
            // 
            // BaseForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(800, 516);
            this.Controls.Add(this.WindowPanel);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private Panel WindowPanel;
    }
}