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
            this.bindableTextBox5 = new ERP.Windows.WF.BindableControlSet.BindableTextBox();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
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
            this.bindableTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.bindableTextBox1.BindingDestination = "Person.Firstname";
            this.bindableTextBox1.DescriptionWidth = 0;
            this.bindableTextBox1.ForeColor = System.Drawing.Color.White;
            this.bindableTextBox1.Location = new System.Drawing.Point(3, 36);
            this.bindableTextBox1.Name = "bindableTextBox1";
            this.bindableTextBox1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.bindableTextBox1.ReadOnly = false;
            this.bindableTextBox1.SearchButtonActive = false;
            this.bindableTextBox1.SelectionDialogStarter = null;
            this.bindableTextBox1.Size = new System.Drawing.Size(391, 31);
            this.bindableTextBox1.Status = ERP.Windows.WF.Base.InputStatus.Null;
            this.bindableTextBox1.StatusVisible = true;
            this.bindableTextBox1.TabIndex = 0;
            this.bindableTextBox1.UserFriendlyName = null;
            this.bindableTextBox1.ValueChanged = false;
            // 
            // bindableTextBox2
            // 
            this.bindableTextBox2.Access = true;
            this.bindableTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindableTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.bindableTextBox2.BindingDestination = "Person.Name";
            this.bindableTextBox2.DescriptionWidth = 0;
            this.bindableTextBox2.ForeColor = System.Drawing.Color.White;
            this.bindableTextBox2.Location = new System.Drawing.Point(3, 73);
            this.bindableTextBox2.Name = "bindableTextBox2";
            this.bindableTextBox2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.bindableTextBox2.ReadOnly = false;
            this.bindableTextBox2.SearchButtonActive = false;
            this.bindableTextBox2.SelectionDialogStarter = null;
            this.bindableTextBox2.Size = new System.Drawing.Size(391, 31);
            this.bindableTextBox2.Status = ERP.Windows.WF.Base.InputStatus.Null;
            this.bindableTextBox2.StatusVisible = true;
            this.bindableTextBox2.TabIndex = 0;
            this.bindableTextBox2.UserFriendlyName = null;
            this.bindableTextBox2.ValueChanged = false;
            // 
            // bindableTextBox3
            // 
            this.bindableTextBox3.Access = true;
            this.bindableTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindableTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.bindableTextBox3.BindingDestination = "Person.Birthday";
            this.bindableTextBox3.DescriptionWidth = 0;
            this.bindableTextBox3.ForeColor = System.Drawing.Color.White;
            this.bindableTextBox3.Location = new System.Drawing.Point(3, 110);
            this.bindableTextBox3.Name = "bindableTextBox3";
            this.bindableTextBox3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.bindableTextBox3.ReadOnly = false;
            this.bindableTextBox3.SearchButtonActive = false;
            this.bindableTextBox3.SelectionDialogStarter = null;
            this.bindableTextBox3.Size = new System.Drawing.Size(391, 31);
            this.bindableTextBox3.Status = ERP.Windows.WF.Base.InputStatus.Null;
            this.bindableTextBox3.StatusVisible = true;
            this.bindableTextBox3.TabIndex = 0;
            this.bindableTextBox3.UserFriendlyName = null;
            this.bindableTextBox3.ValueChanged = false;
            // 
            // bindableControlPanel1
            // 
            this.bindableControlPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindableControlPanel1.Controls.Add(this.bindableTextBox5);
            this.bindableControlPanel1.Controls.Add(this.btn_new);
            this.bindableControlPanel1.Controls.Add(this.btn_delete);
            this.bindableControlPanel1.Controls.Add(this.btn_save);
            this.bindableControlPanel1.Controls.Add(this.bindableTextBox4);
            this.bindableControlPanel1.Controls.Add(this.bindableTextBox1);
            this.bindableControlPanel1.Controls.Add(this.bindableTextBox3);
            this.bindableControlPanel1.Controls.Add(this.bindableTextBox2);
            this.bindableControlPanel1.Location = new System.Drawing.Point(4, 5);
            this.bindableControlPanel1.Name = "bindableControlPanel1";
            this.bindableControlPanel1.Size = new System.Drawing.Size(400, 219);
            this.bindableControlPanel1.TabIndex = 1;
            // 
            // bindableTextBox5
            // 
            this.bindableTextBox5.Access = true;
            this.bindableTextBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindableTextBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.bindableTextBox5.BindingDestination = "Person.Salary";
            this.bindableTextBox5.DescriptionWidth = 0;
            this.bindableTextBox5.ForeColor = System.Drawing.Color.White;
            this.bindableTextBox5.Location = new System.Drawing.Point(3, 147);
            this.bindableTextBox5.Name = "bindableTextBox5";
            this.bindableTextBox5.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.bindableTextBox5.ReadOnly = false;
            this.bindableTextBox5.SearchButtonActive = false;
            this.bindableTextBox5.SelectionDialogStarter = null;
            this.bindableTextBox5.Size = new System.Drawing.Size(391, 31);
            this.bindableTextBox5.Status = ERP.Windows.WF.Base.InputStatus.Null;
            this.bindableTextBox5.StatusVisible = true;
            this.bindableTextBox5.TabIndex = 2;
            this.bindableTextBox5.UserFriendlyName = null;
            this.bindableTextBox5.ValueChanged = false;
            // 
            // btn_new
            // 
            this.btn_new.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_new.ForeColor = System.Drawing.Color.Black;
            this.btn_new.Location = new System.Drawing.Point(155, 186);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(75, 23);
            this.btn_new.TabIndex = 1;
            this.btn_new.Text = "Neu";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete.ForeColor = System.Drawing.Color.Black;
            this.btn_delete.Location = new System.Drawing.Point(236, 186);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 1;
            this.btn_delete.Text = "Löschen";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.ForeColor = System.Drawing.Color.Black;
            this.btn_save.Location = new System.Drawing.Point(317, 186);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Speichern";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // bindableTextBox4
            // 
            this.bindableTextBox4.Access = true;
            this.bindableTextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindableTextBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.bindableTextBox4.BindingDestination = "Person.ToString";
            this.bindableTextBox4.DescriptionWidth = 0;
            this.bindableTextBox4.ForeColor = System.Drawing.Color.White;
            this.bindableTextBox4.Location = new System.Drawing.Point(3, 3);
            this.bindableTextBox4.Name = "bindableTextBox4";
            this.bindableTextBox4.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.bindableTextBox4.ReadOnly = false;
            this.bindableTextBox4.SearchButtonActive = true;
            this.bindableTextBox4.SelectionDialogStarter = this.selectionDialogStarter1;
            this.bindableTextBox4.Size = new System.Drawing.Size(391, 31);
            this.bindableTextBox4.Status = ERP.Windows.WF.Base.InputStatus.Null;
            this.bindableTextBox4.StatusVisible = false;
            this.bindableTextBox4.TabIndex = 0;
            this.bindableTextBox4.UserFriendlyName = "";
            this.bindableTextBox4.ValueChanged = false;
            this.bindableTextBox4.BeforeButtonClick += new System.EventHandler(this.bindableTextBox4_BeforeButtonClick);
            this.bindableTextBox4.ButtonClicked += new System.EventHandler<ERP.Business.Objects.BusinessObject>(this.bindableTextBox4_ButtonClick);
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
            this.ClientSize = new System.Drawing.Size(406, 228);
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
        private Button btn_new;
        private Button btn_delete;
        private Button btn_save;
        private ERP.Windows.WF.BindableControlSet.BindableTextBox bindableTextBox5;
    }
}