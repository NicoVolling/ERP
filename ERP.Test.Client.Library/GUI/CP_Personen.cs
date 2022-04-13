using ERP.Business.Objects;
using ERP.Client.WindowsForms.Controls.Base;
using ERP.Client.WindowsForms.Controls.BindableControls;
using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;
using System.Windows.Forms;

namespace ERP.Test.Client.Library.GUI
{
    public class CP_Personen : ContentPanel
    {
        private B_BO_Combo b_ComboBox1;
        private ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox b_TextBox2;
        private ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox b_TextBox3;
        private B_TextBox b_TextBox4;
        private B_TextBox b_TextBox5;
        private BindableCollectionPanel bindableCollectionPanel1;
        private Button btn_clear;
        private Button btn_create;
        private Button btn_delete;
        private Button btn_save;

        private bool gettinglist = false;

        public CP_Personen()
        {
            DataContext = new CP_Personen_DataContext();
            InitializeComponent();
        }

        public new CP_Personen_DataContext DataContext { get => base.DataContext as CP_Personen_DataContext; set => base.DataContext = value; }

        protected override void OnDataContextChanged(string PropertyName)
        {
            base.OnDataContextChanged(PropertyName);
            if (PropertyName.Equals("Person"))
            {
                RefreshList();
                RefreshButtons();

                b_ComboBox1.SelectedObjectID = DataContext.Person.ID;

                SyncAll();
            }
        }

        protected override void OnErrorChanged()
        {
            base.OnErrorChanged();
            RefreshButtons();
        }

        protected override void OnOpened()
        {
            base.OnOpened();
            BaseWindow.Text = "Personen";

            DataContext.Person = new Person();

            b_ComboBox1.IDChanged += (s, e) =>
            {
                if (b_ComboBox1.SelectedObjectID != Guid.Empty)
                {
                    try
                    {
                        PersonClient PC = new PersonClient();
                        PC.GetData(b_ComboBox1.SelectedObjectID);
                        DataContext.Person = PC.Data;
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("Error", ex.Message);
                        b_ComboBox1.Clear();
                    }
                }
                else
                {
                    DataContext.Person = new Person();
                }
            };

            b_ComboBox1.Combobox.DropDown += Combobox_DropDown;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            ClearAllBinables();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            PersonClient Client = new();
            try
            {
                Client.Create(DataContext.Person);
                DataContext.Person = Client.Data;
            }
            catch (Exception ex)
            {
                ShowMessage("Error", ex.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            PersonClient Client = new();
            try
            {
                if (!Client.GetExistence(DataContext.Person.ID))
                {
                    DataContext.Person = new Person();
                }
                else
                {
                    Client.Delete(DataContext.Person.ID);
                    DataContext.Person = Client.Data;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error", ex.Message);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            PersonClient Client = new();
            try
            {
                if (!Client.GetExistence(DataContext.Person.ID))
                {
                    DataContext.Person = new Person();
                }
                else
                {
                    Client.Change(DataContext.Person.ID, DataContext.Person);
                    DataContext.Person = Client.Data;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error", ex.Message);
            }
        }

        private void Combobox_DropDown(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void InitializeComponent()
        {
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.b_TextBox2 = new ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox();
            this.b_TextBox3 = new ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox();
            this.btn_create = new System.Windows.Forms.Button();
            this.bindableCollectionPanel1 = new ERP.Client.WindowsForms.Controls.BindableControls.BindableCollectionPanel();
            this.b_ComboBox1 = new ERP.Client.WindowsForms.Controls.BindableControls.B_BO_Combo();
            this.b_TextBox5 = new ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox();
            this.b_TextBox4 = new ERP.Client.WindowsForms.Controls.BindableControls.B_TextBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.bindableCollectionPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(339, 219);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Location = new System.Drawing.Point(258, 219);
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
            this.b_TextBox2.IsReadOnly = false;
            this.b_TextBox2.Location = new System.Drawing.Point(3, 83);
            this.b_TextBox2.Name = "b_TextBox2";
            this.b_TextBox2.Size = new System.Drawing.Size(411, 29);
            this.b_TextBox2.TabIndex = 1;
            this.b_TextBox2.Tag = "Person.Firstname";
            // 
            // b_TextBox3
            // 
            this.b_TextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_TextBox3.BindingDestination = "Person.Name";
            this.b_TextBox3.Description = "Name";
            this.b_TextBox3.FixedDescriptionWidth = 80;
            this.b_TextBox3.IsReadOnly = false;
            this.b_TextBox3.Location = new System.Drawing.Point(3, 118);
            this.b_TextBox3.Name = "b_TextBox3";
            this.b_TextBox3.Size = new System.Drawing.Size(411, 29);
            this.b_TextBox3.TabIndex = 2;
            this.b_TextBox3.Tag = "Person.Name";
            // 
            // btn_create
            // 
            this.btn_create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_create.Location = new System.Drawing.Point(177, 219);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(75, 23);
            this.btn_create.TabIndex = 2;
            this.btn_create.Text = "Create";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // bindableCollectionPanel1
            // 
            this.bindableCollectionPanel1.Controls.Add(this.b_ComboBox1);
            this.bindableCollectionPanel1.Controls.Add(this.b_TextBox5);
            this.bindableCollectionPanel1.Controls.Add(this.b_TextBox2);
            this.bindableCollectionPanel1.Controls.Add(this.b_TextBox4);
            this.bindableCollectionPanel1.Controls.Add(this.b_TextBox3);
            this.bindableCollectionPanel1.FixedDescriptionWidth = 80;
            this.bindableCollectionPanel1.Location = new System.Drawing.Point(0, 3);
            this.bindableCollectionPanel1.Name = "bindableCollectionPanel1";
            this.bindableCollectionPanel1.Size = new System.Drawing.Size(417, 210);
            this.bindableCollectionPanel1.TabIndex = 3;
            // 
            // b_ComboBox1
            // 
            this.b_ComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_ComboBox1.BindingDestination = "BusinessObjectList";
            this.b_ComboBox1.Description = "Person";
            this.b_ComboBox1.FixedDescriptionWidth = 80;
            this.b_ComboBox1.IsReadOnly = false;
            this.b_ComboBox1.Location = new System.Drawing.Point(3, 11);
            this.b_ComboBox1.Name = "b_ComboBox1";
            this.b_ComboBox1.ObjectList = null;
            this.b_ComboBox1.SelectedObjectID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.b_ComboBox1.Size = new System.Drawing.Size(411, 31);
            this.b_ComboBox1.TabIndex = 0;
            this.b_ComboBox1.Tag = "BusinessObjectList";
            // 
            // b_TextBox5
            // 
            this.b_TextBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_TextBox5.BindingDestination = "Person.ID";
            this.b_TextBox5.Description = "ID";
            this.b_TextBox5.FixedDescriptionWidth = 80;
            this.b_TextBox5.IsReadOnly = true;
            this.b_TextBox5.Location = new System.Drawing.Point(3, 48);
            this.b_TextBox5.Name = "b_TextBox5";
            this.b_TextBox5.Size = new System.Drawing.Size(411, 29);
            this.b_TextBox5.TabIndex = 0;
            this.b_TextBox5.TabStop = false;
            this.b_TextBox5.Tag = "Person.ID";
            // 
            // b_TextBox4
            // 
            this.b_TextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_TextBox4.BindingDestination = "Person.Birthday";
            this.b_TextBox4.Description = "Birthday";
            this.b_TextBox4.FixedDescriptionWidth = 80;
            this.b_TextBox4.IsReadOnly = false;
            this.b_TextBox4.Location = new System.Drawing.Point(3, 153);
            this.b_TextBox4.Name = "b_TextBox4";
            this.b_TextBox4.Size = new System.Drawing.Size(411, 29);
            this.b_TextBox4.TabIndex = 3;
            this.b_TextBox4.Tag = "Person.Birthday";
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Location = new System.Drawing.Point(99, 219);
            this.btn_clear.Name = "btn_clear";
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
            this.Controls.Add(this.btn_save);
            this.Name = "CP_Personen";
            this.Size = new System.Drawing.Size(420, 245);
            this.bindableCollectionPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void RefreshButtons()
        {
            if (HasError)
            {
                btn_create.Enabled = false;
                btn_delete.Enabled = false;
                btn_save.Enabled = false;
            }
            else
            {
                if (!DataContext.Person.IsEmpty())
                {
                    btn_delete.Enabled = true;
                    btn_save.Enabled = true;
                    btn_create.Enabled = false;
                }
                else
                {
                    btn_delete.Enabled = false;
                    btn_save.Enabled = false;
                    btn_create.Enabled = true;
                }
            }
        }

        private void RefreshList()
        {
            if (!gettinglist)
            {
                gettinglist = true;

                PersonClient Client = new();
                DataContext.BusinessObjectList = Client.GetList().Select(o => o).ToList();
                b_ComboBox1.ObjectList = DataContext.BusinessObjectList;

                gettinglist = false;
            }
        }
    }
}