﻿using ERP.BaseLib.Serialization;
using ERP.Business.Client;
using ERP.Business.Objects;
using ERP.Business.Objects.Attributes;
using ERP.Parsing.Parser;
using ERP.Windows.WF.Base;
using ERP.Windows.WF.Binding.Supervisor;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace ERP.Windows.WF.Binding.Controls
{
    public partial class BindableDataGridView : UserControl, IBindable
    {
        private IEnumerable<BusinessObject> CurrentObjects;

        private int currentSite = 1;

        private DataContext DataContext;
        private int maxElementCount = 35;

        private int maxSite = 1;

        private int objectsfrom;

        private int objectsto;

        public BindableDataGridView()
        {
            InitializeComponent();
        }

        public event EventHandler Accept;

        public event EventHandler ControlValueChanged;

        public event EventHandler FormatRequest;

        public bool Access
        { get => !lbl_access.Visible; set { lbl_access.Visible = !value; dgv.Visible = value; } }

        [Category("Bindung")]
        public string BindingDestination { get; set; }

        [Category("Bindung")]
        public string BindingDestinationClient { get => ClientBinder.BindingDestination; set => ClientBinder.BindingDestination = value; }

        public List<BusinessObjectIdentifier> BOIList { get; set; }

        public IBusinessObjectClient Client { get => ClientBinder.Value as IBusinessObjectClient; }

        public BaseBindable ClientBinder { get; } = new BaseBindable();

        public Type OriginType => typeof(List<BusinessObjectIdentifier>);

        public bool ReadOnly { get; set; }

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

        public InputStatus Status { get; set; }

        public string UserFriendlyName { get; set; }

        public new void Dispose()
        {
            base.Dispose();
            FormatRequest = null;
            ControlValueChanged = null;
            ClientBinder.Dispose();
        }

        public object GetControlValue()
        {
            return BOIList;
        }

        public Control GetMainControl()
        {
            return this;
        }

        public void OnBound(DataContext DataContext)
        {
            this.DataContext = DataContext;
            ClientBinder.ControlValueChanged += ClientBinder_ControlValueChanged;
            BindingMaster.Bind(ClientBinder, DataContext);
        }

        public void SetControlValue(object Value)
        {
            BOIList = Value as List<BusinessObjectIdentifier>;
            if (BOIList != null)
            {
                currentSite = 1;
                maxSite = (int)Math.Ceiling(((double)BOIList.Count()) / ((double)maxElementCount));
                RefreshGUI();
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            currentSite = currentSite - 1;
            RefreshGUI();
        }

        private void btn_fore_Click(object sender, EventArgs e)
        {
            currentSite = currentSite + 1;
            RefreshGUI();
        }

        private void ClientBinder_ControlValueChanged(object sender, EventArgs e)
        {
            SetControlValue(BOIList);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Accept?.Invoke(this, EventArgs.Empty);
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
            foreach (KeyValuePair<PropertyInfo, ShowGUIAttribute> Kvp in PIList.Select(o => new KeyValuePair<PropertyInfo, ShowGUIAttribute>(o, o.GetCustomAttribute<ShowGUIAttribute>(true))).OrderBy(o => o.Value.ID))
            {
                if (!dgv.Columns.Cast<DataGridViewColumn>().Any(o => o.Name == Kvp.Key.Name))
                {
                    DataGridViewColumn Column;
                    if (Kvp.Key.PropertyType == typeof(bool))
                    {
                        Column = new DataGridViewCheckBoxColumn();
                    }
                    else
                    {
                        Column = new DataGridViewTextBoxColumn();
                    }
                    Column.Name = Kvp.Key.Name;
                    Column.HeaderText = Kvp.Value.UserFriendlyName;
                    Column.Tag = Kvp.Value.Serialize();
                    dgv.Columns.Add(Column);
                    Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
        }

        private void DGV_Data()
        {
            dgv.Rows.Clear();
            foreach (BusinessObject BO in CurrentObjects)
            {
                int rowid = dgv.Rows.Add();
                DataGridViewRow row = dgv.Rows[rowid];
                foreach (DataGridViewCell Cell in row.Cells)
                {
                    PropertyInfo PI = BO.GetType().GetProperty(Cell.OwningColumn.Name);
                    Type Type = PI.PropertyType == typeof(bool) ? typeof(bool) : typeof(string);
                    IParser Parser = ParsingMaster.GetParser(Type, PI.PropertyType);
                    ShowGUIAttribute SGA = new ShowGUIAttribute();
                    if (Cell.OwningColumn.Tag != null)
                    {
                        SGA = Json.Deserialize<ShowGUIAttribute>(Cell.OwningColumn.Tag.ToString());
                    }
                    Cell.Value = Parser.Parse(PI.GetValue(BO), Type, SGA, out bool Error);
                }
            }
        }

        private void RefreshDGV()
        {
            try
            {
                Guid[] array;
                if (BOIList.Count() > 1)
                {
                    array = BOIList.Take(new Range(objectsfrom - 1, objectsto - 1)).Select(o => o.ID).ToArray();
                } else 
                {
                    array = BOIList.Select(o => o.ID).ToArray();
                }
                CurrentObjects = Client.GetObjects(DataContext.SECURITY_CODE, array);

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
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshGUI()
        {
            if (BOIList != null && ClientBinder != null && Client != null)
            {
                if (currentSite == maxSite) { btn_fore.Enabled = false; } else { btn_fore.Enabled = true; }
                if (currentSite == 1) { btn_back.Enabled = false; } else { btn_back.Enabled = true; }
                objectsfrom = ((currentSite - 1) * maxElementCount) + 1;
                if (currentSite == maxSite && objectsfrom > BOIList.Count())
                {
                    objectsfrom = BOIList.Count();
                }
                objectsto = (currentSite * maxElementCount) + 1;
                if (currentSite == maxSite && objectsto > BOIList.Count())
                {
                    objectsto = BOIList.Count();
                }
                lbl_site.Text = $"Seite {currentSite} von {maxSite} ({objectsfrom}-{objectsto - 1})";

                RefreshDGV();
            }
        }
    }
}