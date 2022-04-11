using ERP.Business.Objects;
using ERP.Client.WindowsForms.Binding;
using ERP.Client.WindowsForms.Binding.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Client.WindowsForms.Controls.BindableControls
{
    public class B_BO_Combo : BindableControl
    {
        private System.Windows.Forms.ComboBox comboBox1;
        private List<BusinessObject> objectList;

        public B_BO_Combo()
        {
            InitializeComponent();
        }

        public event EventHandler IDChanged;

        public List<BusinessObject> ObjectList
        { get => objectList; private set { objectList = value; RefreshList(); } }

        public BusinessObject SelectedObject { get => ObjectList.FirstOrDefault(o => o.ID == SelectedObjectID); }

        public int SelectedObjectID { get; private set; } = -1;

        public ComboBox TextBox { get => comboBox1; }

        protected override void OnBound()
        {
            comboBox1.SelectedIndexChanged += (s, e) =>
            {
                SelectedObjectID = ((BusinessObject)comboBox1.SelectedItem).ID;
                SaveData();
            };
        }

        protected override void OnClear()
        {
            SelectedObjectID = -1;
            RefreshList();
        }

        protected override IParser OnGetParser()
        {
            return IParser.GetParser(typeof(List<BusinessObject>), TargetType);
        }

        protected virtual void OnIDChanged()
        {
            IDChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnLoadData()
        {
            ObjectList = Get() as List<BusinessObject>;
            Status = SelectedObjectID == -1 ? BindingStatus.NullOrDefault : BindingStatus.Saved;
        }

        protected override void OnSaveData()
        {
            Status = SelectedObjectID == -1 ? BindingStatus.NullOrDefault : BindingStatus.Saved;
            OnIDChanged();
        }

        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ControlPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // ControlPanel
            //
            this.ControlPanel.Controls.Add(this.comboBox1);
            this.ControlPanel.Size = new System.Drawing.Size(351, 29);
            //
            // comboBox1
            //
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(344, 23);
            this.comboBox1.TabIndex = 0;
            //
            // B_ComboBox
            //
            this.Name = "B_ComboBox";
            this.Size = new System.Drawing.Size(383, 29);
            this.ControlPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void RefreshList()
        {
            int ID = SelectedObjectID;
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(ObjectList.ToArray());
            comboBox1.Items.Add(BusinessObject.Empty);
            if (comboBox1.Items.Cast<Object>().FirstOrDefault(o => o is BusinessObject BO && BO.ID == ID) is BusinessObject BO)
            {
                comboBox1.SelectedItem = BO;
            }
        }
    }
}