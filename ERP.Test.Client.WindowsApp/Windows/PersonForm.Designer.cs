namespace ERP.Test.Client.WindowsApp.Windows
{
    partial class PersonForm
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
            this.components = new System.ComponentModel.Container();
            this.bindableTextBox1 = new ERP.Windows.WF.BindableControlSet.BindableTextBox();
            this.bindableTextBox2 = new ERP.Windows.WF.BindableControlSet.BindableTextBox();
            this.bindableTextBox3 = new ERP.Windows.WF.BindableControlSet.BindableTextBox();
            this.bindableControlPanel1 = new ERP.Windows.WF.Binding.Controls.BindableControlPanel();
            this.bindableTextBox4 = new ERP.Windows.WF.BindableControlSet.BindableTextBox();
            this.selectionDialogStarter1 = new ERP.Windows.WF.Binding.Components.SelectionDialogStarter(this.components);
            this.bindableControlPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindableTextBox1
            // 
            this.bindableTextBox1.Access = true;
            this.bindableTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindableTextBox1.BackColor = System.Drawing.Color.White;
            this.bindableTextBox1.BindingDestination = "Person.Firstname";
            this.bindableTextBox1.DescriptionWidth = 0;
            this.bindableTextBox1.ForeColor = System.Drawing.Color.White;
            this.bindableTextBox1.Location = new System.Drawing.Point(3, 36);
            this.bindableTextBox1.Name = "bindableTextBox1";
            this.bindableTextBox1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.bindableTextBox1.ReadOnly = false;
            this.bindableTextBox1.SearchButtonActive = false;
            this.bindableTextBox1.SelectionDialogStarter = null;
            this.bindableTextBox1.Size = new System.Drawing.Size(370, 31);
            this.bindableTextBox1.Status = ERP.Windows.WF.Base.InputStatus.Null;
            this.bindableTextBox1.StatusVisible = true;
            this.bindableTextBox1.TabIndex = 0;
            this.bindableTextBox1.UserFriendlyName = null;
            // 
            // bindableTextBox2
            // 
            this.bindableTextBox2.Access = true;
            this.bindableTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindableTextBox2.BackColor = System.Drawing.Color.White;
            this.bindableTextBox2.BindingDestination = "Person.Name";
            this.bindableTextBox2.DescriptionWidth = 0;
            this.bindableTextBox2.ForeColor = System.Drawing.Color.White;
            this.bindableTextBox2.Location = new System.Drawing.Point(3, 73);
            this.bindableTextBox2.Name = "bindableTextBox2";
            this.bindableTextBox2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.bindableTextBox2.ReadOnly = false;
            this.bindableTextBox2.SearchButtonActive = false;
            this.bindableTextBox2.SelectionDialogStarter = null;
            this.bindableTextBox2.Size = new System.Drawing.Size(370, 31);
            this.bindableTextBox2.Status = ERP.Windows.WF.Base.InputStatus.Null;
            this.bindableTextBox2.StatusVisible = true;
            this.bindableTextBox2.TabIndex = 0;
            this.bindableTextBox2.UserFriendlyName = null;
            // 
            // bindableTextBox3
            // 
            this.bindableTextBox3.Access = true;
            this.bindableTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindableTextBox3.BackColor = System.Drawing.Color.White;
            this.bindableTextBox3.BindingDestination = "Person.Birthday";
            this.bindableTextBox3.DescriptionWidth = 0;
            this.bindableTextBox3.ForeColor = System.Drawing.Color.White;
            this.bindableTextBox3.Location = new System.Drawing.Point(3, 110);
            this.bindableTextBox3.Name = "bindableTextBox3";
            this.bindableTextBox3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.bindableTextBox3.ReadOnly = false;
            this.bindableTextBox3.SearchButtonActive = false;
            this.bindableTextBox3.SelectionDialogStarter = null;
            this.bindableTextBox3.Size = new System.Drawing.Size(370, 31);
            this.bindableTextBox3.Status = ERP.Windows.WF.Base.InputStatus.Null;
            this.bindableTextBox3.StatusVisible = true;
            this.bindableTextBox3.TabIndex = 0;
            this.bindableTextBox3.UserFriendlyName = null;
            // 
            // bindableControlPanel1
            // 
            this.bindableControlPanel1.Controls.Add(this.bindableTextBox4);
            this.bindableControlPanel1.Controls.Add(this.bindableTextBox1);
            this.bindableControlPanel1.Controls.Add(this.bindableTextBox3);
            this.bindableControlPanel1.Controls.Add(this.bindableTextBox2);
            this.bindableControlPanel1.Location = new System.Drawing.Point(12, 12);
            this.bindableControlPanel1.Name = "bindableControlPanel1";
            this.bindableControlPanel1.Size = new System.Drawing.Size(379, 144);
            this.bindableControlPanel1.TabIndex = 1;
            // 
            // bindableTextBox4
            // 
            this.bindableTextBox4.Access = true;
            this.bindableTextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindableTextBox4.BackColor = System.Drawing.Color.White;
            this.bindableTextBox4.BindingDestination = "Person.ID";
            this.bindableTextBox4.DescriptionWidth = 0;
            this.bindableTextBox4.ForeColor = System.Drawing.Color.White;
            this.bindableTextBox4.Location = new System.Drawing.Point(3, 3);
            this.bindableTextBox4.Name = "bindableTextBox4";
            this.bindableTextBox4.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.bindableTextBox4.ReadOnly = true;
            this.bindableTextBox4.SearchButtonActive = true;
            this.bindableTextBox4.SelectionDialogStarter = this.selectionDialogStarter1;
            this.bindableTextBox4.Size = new System.Drawing.Size(370, 31);
            this.bindableTextBox4.Status = ERP.Windows.WF.Base.InputStatus.Null;
            this.bindableTextBox4.StatusVisible = false;
            this.bindableTextBox4.TabIndex = 0;
            this.bindableTextBox4.UserFriendlyName = "";
            this.bindableTextBox4.ButtonClick += new System.EventHandler<ERP.Business.Objects.BusinessObject>(this.bindableTextBox4_ButtonClick);
            // 
            // selectionDialogStarter1
            // 
            this.selectionDialogStarter1.BindingDestinationClient = "Client";
            this.selectionDialogStarter1.BindingDestinationList = "Objects";
            this.selectionDialogStarter1.DataContext = null;
            // 
            // PersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bindableControlPanel1);
            this.Name = "PersonForm";
            this.Text = "PersonForm";
            this.bindableControlPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ERP.Windows.WF.BindableControlSet.BindableTextBox bindableTextBox1;
        private ERP.Windows.WF.BindableControlSet.BindableTextBox bindableTextBox2;
        private ERP.Windows.WF.BindableControlSet.BindableTextBox bindableTextBox3;
        private ERP.Windows.WF.Binding.Controls.BindableControlPanel bindableControlPanel1;
        private ERP.Windows.WF.Binding.Components.SelectionDialogStarter selectionDialogStarter1;
        private ERP.Windows.WF.BindableControlSet.BindableTextBox bindableTextBox4;
    }
}