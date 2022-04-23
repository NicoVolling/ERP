namespace ERP.Windows.WF.Binding.Forms
{
    partial class SelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bindableDataGridView1 = new ERP.Windows.WF.Binding.Controls.BindableDataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_abort = new ERP.Windows.WF.Base.Controls.RoundImageButton();
            this.btn_accept = new ERP.Windows.WF.Base.Controls.RoundImageButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindableDataGridView1
            // 
            this.bindableDataGridView1.Access = true;
            this.bindableDataGridView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.bindableDataGridView1.BindingDestination = null;
            this.bindableDataGridView1.BindingDestinationClient = null;
            this.bindableDataGridView1.BOIList = null;
            this.bindableDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bindableDataGridView1.ForeColor = System.Drawing.Color.White;
            this.bindableDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.bindableDataGridView1.Name = "bindableDataGridView1";
            this.bindableDataGridView1.ReadOnly = false;
            this.bindableDataGridView1.Size = new System.Drawing.Size(530, 388);
            this.bindableDataGridView1.Status = ERP.Windows.WF.Base.InputStatus.Null;
            this.bindableDataGridView1.TabIndex = 0;
            this.bindableDataGridView1.UserFriendlyName = null;
            this.bindableDataGridView1.Accept += new System.EventHandler(this.bindableDataGridView1_Accept);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_abort);
            this.panel1.Controls.Add(this.btn_accept);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 388);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 62);
            this.panel1.TabIndex = 1;
            // 
            // btn_abort
            // 
            this.btn_abort.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_abort.Image = global::ERP.Windows.WF.Binding.Properties.Resources.multiply;
            this.btn_abort.Location = new System.Drawing.Point(0, 0);
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.Size = new System.Drawing.Size(71, 62);
            this.btn_abort.TabIndex = 1;
            this.btn_abort.Text = "roundImageButton2";
            this.btn_abort.Click += new System.EventHandler(this.btn_abort_Click);
            // 
            // btn_accept
            // 
            this.btn_accept.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_accept.Image = global::ERP.Windows.WF.Binding.Properties.Resources.check;
            this.btn_accept.Location = new System.Drawing.Point(459, 0);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(71, 62);
            this.btn_accept.TabIndex = 0;
            this.btn_accept.Text = "roundImageButton1";
            this.btn_accept.Click += new System.EventHandler(this.btn_accept_Click);
            // 
            // SelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 450);
            this.Controls.Add(this.bindableDataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "SelectionForm";
            this.Text = "SelectionForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.BindableDataGridView bindableDataGridView1;
        private Panel panel1;
        private Base.Controls.RoundImageButton btn_accept;
        private Base.Controls.RoundImageButton btn_abort;
    }
}