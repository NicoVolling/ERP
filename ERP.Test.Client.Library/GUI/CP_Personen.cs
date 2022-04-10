using ERP.Client.WindowsForms.Controls.Base;
using ERP.Client.WindowsForms.Controls.BindableControls;
using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Test.Client.Library.GUI
{
    public class CP_Personen : ContentPanel
    {
        private Button btn_save;
        private Button btn_load;
        private Button btn_delete;
        private ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox b_TextBox2;
        private ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox b_TextBox3;
        private Button btn_create;
        private BindableCollectionPanel bindableCollectionPanel1;
        private B_TextBox b_TextBox4;
        private Button btn_clear;
        private ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox b_TextBox1;

        public new CP_Personen_DataContext DataContext { get => base.DataContext as CP_Personen_DataContext; set => base.DataContext = value; }

        public CP_Personen()
        {
            DataContext = new CP_Personen_DataContext();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.b_TextBox1 = new ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.b_TextBox2 = new ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox();
            this.b_TextBox3 = new ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox();
            this.btn_create = new System.Windows.Forms.Button();
            this.bindableCollectionPanel1 = new ERP.Client.WindowsForms.Controls.BindableControls.BindableCollectionPanel();
            this.b_TextBox4 = new ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.bindableCollectionPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_TextBox1
            // 
            this.b_TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_TextBox1.BindingDestination = "Person.ID";
            this.b_TextBox1.Description = "ID";
            this.b_TextBox1.FixedDescriptionWidth = 80;
            this.b_TextBox1.Location = new System.Drawing.Point(13, 15);
            this.b_TextBox1.Name = "b_TextBox1";
            this.b_TextBox1.Size = new System.Drawing.Size(365, 29);
            this.b_TextBox1.TabIndex = 0;
            this.b_TextBox1.Tag = "Person.ID";
            // 
            // btn_save
            // 
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(324, 219);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_load
            // 
            this.btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load.Location = new System.Drawing.Point(243, 219);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 1;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Location = new System.Drawing.Point(162, 219);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 1;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // b_TextBox2
            // 
            this.b_TextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_TextBox2.BindingDestination = "Person.Firstname";
            this.b_TextBox2.Description = "Firstname";
            this.b_TextBox2.FixedDescriptionWidth = 80;
            this.b_TextBox2.Location = new System.Drawing.Point(13, 50);
            this.b_TextBox2.Name = "b_TextBox2";
            this.b_TextBox2.Size = new System.Drawing.Size(365, 29);
            this.b_TextBox2.TabIndex = 0;
            this.b_TextBox2.Tag = "Person.Firstname";
            // 
            // b_TextBox3
            // 
            this.b_TextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_TextBox3.BindingDestination = "Person.Name";
            this.b_TextBox3.Description = "Name";
            this.b_TextBox3.FixedDescriptionWidth = 80;
            this.b_TextBox3.Location = new System.Drawing.Point(13, 85);
            this.b_TextBox3.Name = "b_TextBox3";
            this.b_TextBox3.Size = new System.Drawing.Size(365, 29);
            this.b_TextBox3.TabIndex = 0;
            this.b_TextBox3.Tag = "Person.Name";
            // 
            // btn_create
            // 
            this.btn_create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_create.Location = new System.Drawing.Point(81, 219);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(75, 23);
            this.btn_create.TabIndex = 2;
            this.btn_create.Text = "Create";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // bindableCollectionPanel1
            // 
            this.bindableCollectionPanel1.Controls.Add(this.b_TextBox1);
            this.bindableCollectionPanel1.Controls.Add(this.b_TextBox2);
            this.bindableCollectionPanel1.Controls.Add(this.b_TextBox4);
            this.bindableCollectionPanel1.Controls.Add(this.b_TextBox3);
            this.bindableCollectionPanel1.FixedDescriptionWidth = 80;
            this.bindableCollectionPanel1.Location = new System.Drawing.Point(0, 3);
            this.bindableCollectionPanel1.Name = "bindableCollectionPanel1";
            this.bindableCollectionPanel1.Size = new System.Drawing.Size(399, 210);
            this.bindableCollectionPanel1.TabIndex = 3;
            // 
            // b_TextBox4
            // 
            this.b_TextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_TextBox4.BindingDestination = "Person.Birthday";
            this.b_TextBox4.Description = "Birthday";
            this.b_TextBox4.FixedDescriptionWidth = 80;
            this.b_TextBox4.Location = new System.Drawing.Point(13, 120);
            this.b_TextBox4.Name = "b_TextBox4";
            this.b_TextBox4.Size = new System.Drawing.Size(365, 29);
            this.b_TextBox4.TabIndex = 0;
            this.b_TextBox4.Tag = "Person.Birthday";
            // 
            // button1
            // 
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Location = new System.Drawing.Point(3, 219);
            this.btn_clear.Name = "button1";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 2;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // CP_Personen
            // 
            this.Controls.Add(this.bindableCollectionPanel1);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_save);
            this.Name = "CP_Personen";
            this.bindableCollectionPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected override void OnOpened()
        {
            base.OnOpened();
            BaseWindow.Text = "Test-Fenster";

            DataContext.Person = new Person();

            Loaded(false);
        }

        protected override void OnErrorChanged()
        {
            base.OnErrorChanged();
            if(HasError) 
            {
                btn_create.Enabled = false;
                btn_delete.Enabled = false;
                btn_save.Enabled = false;
            }
            else 
            {
                btn_create.Enabled = true;
                btn_delete.Enabled = true;
                btn_save.Enabled = true;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            PersonClient Client = new PersonClient();
            try
            {
                Client.Change(DataContext.Person.ID, DataContext.Person);
                Loaded(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataContext.Person = DataContext.Person;
            SyncAll();
        }


        private void Loaded(bool loaded) 
        {
            if (loaded)
            {
                btn_save.Enabled = true;
                btn_delete.Enabled = true;
            }
            else 
            {
                btn_delete.Enabled = false;
                btn_save.Enabled = false;
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            PersonClient Client = new PersonClient();
            try
            {
                Client.GetData(DataContext.Person.ID);
                DataContext.Person = Client.Data;
                Loaded(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataContext.Person = DataContext.Person;
            SyncAll();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            PersonClient Client = new PersonClient();
            try
            {
                Client.Delete(DataContext.Person.ID);
                DataContext.Person = Client.Data;
                Loaded(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataContext.Person = DataContext.Person;
            SyncAll();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            PersonClient Client = new PersonClient();
            try
            {
                Client.Create(DataContext.Person);
                Loaded(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataContext.Person = DataContext.Person;
            SyncAll();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            ClearAllBinables();
        }
    }
}
