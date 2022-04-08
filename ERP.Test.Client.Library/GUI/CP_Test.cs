using ERP.Client.WindowsForms.Controls.Base;
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
    public class CP_Test : ContentPanel
    {
        private Button btn_save;
        private Button btn_load;
        private Button btn_delete;
        private ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox b_TextBox2;
        private ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox b_TextBox3;
        private Button btn_create;
        private ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox b_TextBox1;

        public CP_Test_DataContext DataContext { get => _DataContext as CP_Test_DataContext; set => _DataContext = value; }

        public CP_Test()
        {
            DataContext = new CP_Test_DataContext();
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
            this.SuspendLayout();
            // 
            // b_TextBox1
            // 
            this.b_TextBox1.Description = "ID";
            this.b_TextBox1.Get = null;
            this.b_TextBox1.Location = new System.Drawing.Point(38, 31);
            this.b_TextBox1.Name = "b_TextBox1";
            this.b_TextBox1.Set = null;
            this.b_TextBox1.Size = new System.Drawing.Size(296, 29);
            this.b_TextBox1.Status = ERP.Client.WindowsForms.Controls.BindableControls.BindingStatus.Unbound;
            this.b_TextBox1.TabIndex = 0;
            this.b_TextBox1.Tag = "Person.ID";
            this.b_TextBox1.TargetType = null;
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
            this.b_TextBox2.Description = "Firstname";
            this.b_TextBox2.Get = null;
            this.b_TextBox2.Location = new System.Drawing.Point(38, 66);
            this.b_TextBox2.Name = "b_TextBox2";
            this.b_TextBox2.Set = null;
            this.b_TextBox2.Size = new System.Drawing.Size(296, 29);
            this.b_TextBox2.Status = ERP.Client.WindowsForms.Controls.BindableControls.BindingStatus.Unbound;
            this.b_TextBox2.TabIndex = 0;
            this.b_TextBox2.Tag = "Person.Firstname";
            this.b_TextBox2.TargetType = null;
            // 
            // b_TextBox3
            // 
            this.b_TextBox3.Description = "Name";
            this.b_TextBox3.Get = null;
            this.b_TextBox3.Location = new System.Drawing.Point(38, 101);
            this.b_TextBox3.Name = "b_TextBox3";
            this.b_TextBox3.Set = null;
            this.b_TextBox3.Size = new System.Drawing.Size(296, 29);
            this.b_TextBox3.Status = ERP.Client.WindowsForms.Controls.BindableControls.BindingStatus.Unbound;
            this.b_TextBox3.TabIndex = 0;
            this.b_TextBox3.Tag = "Person.Name";
            this.b_TextBox3.TargetType = null;
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
            // CP_Test
            // 
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.b_TextBox3);
            this.Controls.Add(this.b_TextBox2);
            this.Controls.Add(this.b_TextBox1);
            this.Name = "CP_Test";
            this.ResumeLayout(false);

        }

        protected override void OnOpened()
        {
            base.OnOpened();
            BaseWindow.Text = "Test-Fenster";

            DataContext.Person = new Person();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            PersonClient Client = new PersonClient();
            try
            {
                Client.Change(DataContext.Person.ID, DataContext.Person);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataContext.Person = DataContext.Person;
            SyncAll();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            PersonClient Client = new PersonClient();
            try
            {
                Client.GetData(DataContext.Person.ID);
                DataContext.Person = Client.Data;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataContext.Person = DataContext.Person;
            SyncAll();
        }
    }
}
