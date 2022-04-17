using ERP.Business.Client;
using ERP.Business.Objects;
using ERP.Business.Server;
using ERP.Client.WindowsForms.Controls.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Client.WindowsForms.Controls.BindableControls
{
    public class B_BO_Selection_ContentPanel : ContentPanel
    {
        private B_BO_Selection b_bO_Selection1;
        private System.Windows.Forms.Button btn_abort;
        private System.Windows.Forms.Button btn_accept;
        private System.Windows.Forms.Panel panel1;

        public B_BO_Selection_ContentPanel()
        {
            InitializeComponent();
            this.DataContext = Binding.DataContext.Empty;
        }

        public void SetBusinessObjectList(IBusinessObjectClient Client, IEnumerable<BusinessObjectIdentifier> BusinessObjectList)
        {
            b_bO_Selection1.SetBusinessObjectList(Client, BusinessObjectList);
        }

        private void btn_abort_Click(object sender, EventArgs e)
        {
            BaseWindow?.Close();
        }

        private void btn_accept_Click(object sender, EventArgs e)
        {
            if (b_bO_Selection1.SelectedObject is BusinessObject BO)
            {
                BaseWindow.Result = BO;
                BaseWindow?.Close();
            }
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_abort = new System.Windows.Forms.Button();
            this.btn_accept = new System.Windows.Forms.Button();
            this.b_bO_Selection1 = new ERP.Client.WindowsForms.Controls.BindableControls.B_BO_Selection();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.Controls.Add(this.btn_abort);
            this.panel1.Controls.Add(this.btn_accept);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 208);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(402, 37);
            this.panel1.TabIndex = 1;
            //
            // btn_abort
            //
            this.btn_abort.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_abort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_abort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_abort.Location = new System.Drawing.Point(3, 3);
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.Size = new System.Drawing.Size(113, 31);
            this.btn_abort.TabIndex = 1;
            this.btn_abort.Text = "Abbrechen";
            this.btn_abort.UseVisualStyleBackColor = true;
            this.btn_abort.Click += new System.EventHandler(this.btn_abort_Click);
            //
            // btn_accept
            //
            this.btn_accept.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_accept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_accept.Location = new System.Drawing.Point(286, 3);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(113, 31);
            this.btn_accept.TabIndex = 0;
            this.btn_accept.Text = "Übernehmen";
            this.btn_accept.UseVisualStyleBackColor = true;
            this.btn_accept.Click += new System.EventHandler(this.btn_accept_Click);
            //
            // b_bO_Selection1
            //
            this.b_bO_Selection1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.b_bO_Selection1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.b_bO_Selection1.ForeColor = System.Drawing.Color.White;
            this.b_bO_Selection1.Location = new System.Drawing.Point(0, 0);
            this.b_bO_Selection1.Name = "b_bO_Selection1";
            this.b_bO_Selection1.Size = new System.Drawing.Size(402, 208);
            this.b_bO_Selection1.TabIndex = 2;
            //
            // B_BO_Selection_ContentPanel
            //
            this.Controls.Add(this.b_bO_Selection1);
            this.Controls.Add(this.panel1);
            this.Name = "B_BO_Selection_ContentPanel";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}