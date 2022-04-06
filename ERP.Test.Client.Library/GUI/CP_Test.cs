using ERP.Client.WindowsForms.Controls.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.Client.Library.GUI
{
    public class CP_Test : ContentPanel
    {
        private System.Windows.Forms.Label label1;

        public CP_Test()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 150);
            this.label1.TabIndex = 0;
            this.label1.Text = "MOIN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CP_Test
            // 
            this.Controls.Add(this.label1);
            this.Name = "CP_Test";
            this.ResumeLayout(false);

        }

        protected override void OnOpened()
        {
            base.OnOpened();
            BaseWindow.Text = "Test-Fenster";
            BaseWindow.StatusColor = Color.Green;
        }
    }
}
