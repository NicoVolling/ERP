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
        public ComboBox Combobox;
        private bool isloading = false;
        private bool loaded = false;
        private List<BusinessObjectIdentifier> objectList;
        private int selectedObjectID = -1;

        public B_BO_Combo()
        {
            InitializeComponent();
        }

        public event EventHandler IDChanged;

        public List<BusinessObjectIdentifier> ObjectList
        {
            get => objectList;
            set { objectList = value; RefreshList(); }
        }

        public int SelectedObjectID
        {
            get => selectedObjectID;
            set
            {
                selectedObjectID = value;
                RefreshList();
            }
        }

        public ComboBox TextBox { get => Combobox; }

        protected override void OnBound()
        {
            Combobox.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        protected override void OnClear()
        {
            SelectedObjectID = -1;
            Status = BindingStatus.NullOrDefault;
            RefreshList();
        }

        protected override IParser OnGetParser()
        {
            return IParser.GetParser(typeof(List<BusinessObjectIdentifier>), TargetType);
        }

        protected virtual void OnIDChanged()
        {
            IDChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            loaded = true;
        }

        protected override void OnLoadData()
        {
            ObjectList = Get() as List<BusinessObjectIdentifier>;
            Status = SelectedObjectID == -1 ? BindingStatus.NullOrDefault : BindingStatus.Saved;
        }

        protected override void OnSaveData()
        {
            Status = SelectedObjectID == -1 ? BindingStatus.NullOrDefault : BindingStatus.Saved;
            OnIDChanged();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isloading)
            {
                SelectedObjectID = ((BusinessObjectIdentifier)Combobox.SelectedItem).ID;
                SaveData();
            }
        }

        private void InitializeComponent()
        {
            this.Combobox = new System.Windows.Forms.ComboBox();
            this.ControlPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // ControlPanel
            //
            this.ControlPanel.Controls.Add(this.Combobox);
            this.ControlPanel.Size = new System.Drawing.Size(351, 29);
            //
            // Combobox
            //
            this.Combobox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Combobox.FormattingEnabled = true;
            this.Combobox.Location = new System.Drawing.Point(3, 3);
            this.Combobox.Name = "Combobox";
            this.Combobox.Size = new System.Drawing.Size(344, 23);
            this.Combobox.TabIndex = 0;
            //
            // B_BO_Combo
            //
            this.Name = "B_BO_Combo";
            this.Size = new System.Drawing.Size(383, 29);
            this.ControlPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void RefreshList()
        {
            if (loaded)
            {
                this.Invoke(() =>
                {
                    if (!Combobox.DroppedDown)
                    {
                        try
                        {
                            isloading = true;

                            int ID = SelectedObjectID;
                            Combobox.Items.Clear();
                            Combobox.Items.AddRange(ObjectList.OrderBy(o => o.ID).ToArray());
                            Combobox.Items.Add(BusinessObjectIdentifier.Empty);
                            if (!Combobox.Items.Cast<Object>().Any(o => o is BusinessObjectIdentifier BO && BO.ID == ID))
                            {
                                ID = -1;
                            }

                            selectedObjectID = ID;

                            if (Combobox.Items.Cast<Object>().FirstOrDefault(o => o is BusinessObjectIdentifier BO && BO.ID == ID) is BusinessObjectIdentifier BO)
                            {
                                Combobox.SelectedItem = BO;
                            }

                            isloading = false;
                        }
                        catch { }
                    }
                });
            }
        }
    }
}