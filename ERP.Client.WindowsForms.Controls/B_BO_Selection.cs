using ERP.Business.Client;
using ERP.Business.Objects;
using ERP.Business.Objects.Attributes;
using ERP.Business.Server;
using ERP.Client.WindowsForms.Binding.Parser;
using ERP.Client.WindowsForms.Controls.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Client.WindowsForms.Controls.BindableControls
{
    public class B_BO_Selection : BaseControl
    {
        public IBusinessObjectClient Client;
        private Button btn_back;
        private Button btn_fore;
        private IEnumerable<BusinessObjectIdentifier> BusinessObjectList;
        private IEnumerable<BusinessObject> CurrentObjects;
        private int currentSite = 1;
        private DataGridView dgv;
        private int elementCount = 50;
        private DataGridViewTextBoxColumn ID;
        private Label lbl_site;
        private int maxSite = 1;
        private int objectsfrom;
        private int objectsto;
        private Panel panel2;

        public B_BO_Selection()
        {
            InitializeComponent();
        }

        public BusinessObject SelectedObject
        {
            get
            {
                if (CurrentObjects != null && CurrentObjects.Any())
                {
                    if (dgv.Rows.Count > 0 && dgv.SelectedRows.Count == 1)
                    {
                        return CurrentObjects.FirstOrDefault(o => o.ID.Equals(Guid.Parse(dgv.Rows[dgv.SelectedRows[0].Index].Cells["ID"].Value.ToString())));
                    }
                }
                return null;
            }
        }

        public void SetBusinessObjectList(IBusinessObjectClient Client, IEnumerable<BusinessObjectIdentifier> BusinessObjectList)
        {
            this.BusinessObjectList = BusinessObjectList;
            this.Client = Client;
            currentSite = 1;
            maxSite = (int)Math.Ceiling(((double)BusinessObjectList.Count()) / ((double)elementCount));
            RefreshGUI();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (currentSite > 1)
            {
                currentSite--;
            }
            RefreshGUI();
        }

        private void btn_fore_Click(object sender, EventArgs e)
        {
            if (currentSite < maxSite)
            {
                currentSite++;
            }
            RefreshGUI();
        }

        private void DGV_Columns()
        {
            List<PropertyInfo> PIList = new List<PropertyInfo>();
            foreach (PropertyInfo PI in CurrentObjects.First().GetType().GetProperties())
            {
                if (PI.GetCustomAttributes(true).FirstOrDefault(o => o is ShowGUIAttribute) is ShowGUIAttribute SGA)
                {
                    PIList.Add(PI);
                }
            }
            foreach (KeyValuePair<PropertyInfo, ShowGUIAttribute> Kvp in PIList.Select(o => new KeyValuePair<PropertyInfo, ShowGUIAttribute>(o, o.GetCustomAttribute<ShowGUIAttribute>(true))))
            {
                if (!dgv.Columns.Cast<DataGridViewColumn>().Any(o => o.Name == Kvp.Key.Name))
                {
                    int colid = dgv.Columns.Add(Kvp.Key.Name, Kvp.Value.UserFriendlyName);
                    dgv.Columns[colid].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
        }

        private void DGV_Data()
        {
            foreach (BusinessObject BO in CurrentObjects)
            {
                int rowid = dgv.Rows.Add();
                DataGridViewRow row = dgv.Rows[rowid];
                foreach (DataGridViewCell Cell in row.Cells)
                {
                    IParser Parser = IParser.GetParser(typeof(string), BO.GetType().GetProperty(Cell.OwningColumn.Name).PropertyType);
                    Cell.Value = Parser.Parse(BO.GetType().GetProperty(Cell.OwningColumn.Name).GetValue(BO), typeof(string));
                }
            }
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_site = new System.Windows.Forms.Label();
            this.btn_fore = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            //
            // dgv
            //
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 25;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.ShowEditingIcon = false;
            this.dgv.Size = new System.Drawing.Size(426, 318);
            this.dgv.TabIndex = 1;
            //
            // ID
            //
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            //
            // panel2
            //
            this.panel2.Controls.Add(this.lbl_site);
            this.panel2.Controls.Add(this.btn_fore);
            this.panel2.Controls.Add(this.btn_back);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 318);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(426, 31);
            this.panel2.TabIndex = 2;
            //
            // lbl_site
            //
            this.lbl_site.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_site.Location = new System.Drawing.Point(47, 3);
            this.lbl_site.Name = "lbl_site";
            this.lbl_site.Size = new System.Drawing.Size(332, 25);
            this.lbl_site.TabIndex = 4;
            this.lbl_site.Text = "Seite 1 von 1 (0-0)";
            this.lbl_site.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btn_fore
            //
            this.btn_fore.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_fore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fore.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_fore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_fore.Location = new System.Drawing.Point(379, 3);
            this.btn_fore.Name = "btn_fore";
            this.btn_fore.Size = new System.Drawing.Size(44, 25);
            this.btn_fore.TabIndex = 3;
            this.btn_fore.Text = ">>";
            this.btn_fore.UseVisualStyleBackColor = true;
            this.btn_fore.Click += new System.EventHandler(this.btn_fore_Click);
            //
            // btn_back
            //
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_back.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_back.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_back.Location = new System.Drawing.Point(3, 3);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(44, 25);
            this.btn_back.TabIndex = 2;
            this.btn_back.Text = "<<";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            //
            // B_BO_Selection
            //
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "B_BO_Selection";
            this.Size = new System.Drawing.Size(426, 349);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void RefreshDGV()
        {
            try
            {
                CurrentObjects = Client.GetObjects(BusinessObjectList.Take(new Range(objectsfrom - 1, objectsto - 1)).Select(o => o.ID).ToArray());

                if (CurrentObjects.Any())
                {
                    DGV_Columns();
                    DGV_Data();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                this.ContentPanel.ShowMessage("Error", ex.Message);
            }
        }

        private void RefreshGUI()
        {
            if (BusinessObjectList != null)
            {
                if (currentSite == maxSite) { btn_fore.Enabled = false; } else { btn_fore.Enabled = true; }
                if (currentSite == 1) { btn_back.Enabled = false; } else { btn_back.Enabled = true; }
                objectsfrom = ((currentSite - 1) * elementCount) + 1;
                if (currentSite == maxSite && objectsfrom > BusinessObjectList.Count())
                {
                    objectsfrom = BusinessObjectList.Count();
                }
                objectsto = (currentSite * elementCount) + 1;
                if (objectsto == maxSite && objectsto > BusinessObjectList.Count())
                {
                    objectsto = BusinessObjectList.Count();
                }
                lbl_site.Text = $"Seite {currentSite} von {maxSite} ({objectsfrom}-{objectsto})";

                RefreshDGV();
            }
        }
    }
}