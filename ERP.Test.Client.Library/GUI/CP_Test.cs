using ERP.Client.WindowsForms.Controls.Base;
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
            this.SuspendLayout();
            // 
            // b_TextBox1
            // 
            this.b_TextBox1.Get = null;
            this.b_TextBox1.Location = new System.Drawing.Point(25, 14);
            this.b_TextBox1.Name = "b_TextBox1";
            this.b_TextBox1.Set = null;
            this.b_TextBox1.Size = new System.Drawing.Size(368, 29);
            this.b_TextBox1.TabIndex = 0;
            this.b_TextBox1.Tag = "Person.Firstname";
            // 
            // btn_save
            // 
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(108, 70);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // CP_Test
            // 
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.b_TextBox1);
            this.Name = "CP_Test";
            this.Size = new System.Drawing.Size(448, 150);
            this.ResumeLayout(false);

        }

        protected override void OnOpened()
        {
            base.OnOpened();
            BaseWindow.Text = "Test-Fenster";
            BaseWindow.StatusColor = Color.Green;

            DataContext.Person = new Person();
            DataContext.Person.Firstname = "Nico";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SyncAll();
        }
    }
}
