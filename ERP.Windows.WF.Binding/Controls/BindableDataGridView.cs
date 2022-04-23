using ERP.Business.Client;
using ERP.Business.Objects;
using ERP.Business.Objects.Attributes;
using ERP.Windows.WF.Base;
using ERP.Windows.WF.Binding.Forms;
using ERP.Windows.WF.Binding.Parsing.Parser;
using ERP.Windows.WF.Binding.Supervisor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Windows.WF.Binding.Controls
{
    public partial class BindableDataGridView : UserControl, IBindable
    {
        private IEnumerable<BusinessObject> CurrentObjects;

        private int currentSite = 1;

        private int elementCount = 50;

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
            ClientBinder.ControlValueChanged += ClientBinder_ControlValueChanged;
            BindingMaster.Bind(ClientBinder, DataContext);
        }

        public void SetControlValue(object Value)
        {
            BOIList = Value as List<BusinessObjectIdentifier>;
            if (BOIList != null)
            {
                currentSite = 1;
                maxSite = (int)Math.Ceiling(((double)BOIList.Count()) / ((double)elementCount));
                RefreshGUI();
            }
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
            foreach (KeyValuePair<PropertyInfo, ShowGUIAttribute> Kvp in PIList.Select(o => new KeyValuePair<PropertyInfo, ShowGUIAttribute>(o, o.GetCustomAttribute<ShowGUIAttribute>(true))))
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
                    dgv.Columns.Add(Column);
                    Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
                    PropertyInfo PI = BO.GetType().GetProperty(Cell.OwningColumn.Name);
                    Type Type = PI.PropertyType == typeof(bool) ? typeof(bool) : typeof(string);
                    IParser Parser = IParser.GetParser(Type, PI.PropertyType);
                    Cell.Value = Parser.Parse(PI.GetValue(BO), Type, out bool Error);
                }
            }
        }

        private void RefreshDGV()
        {
            try
            {
                CurrentObjects = Client.GetObjects(BOIList.Take(new Range(objectsfrom - 1, objectsto - 1)).Select(o => o.ID).ToArray());

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
                objectsfrom = ((currentSite - 1) * elementCount) + 1;
                if (currentSite == maxSite && objectsfrom > BOIList.Count())
                {
                    objectsfrom = BOIList.Count();
                }
                objectsto = (currentSite * elementCount) + 1;
                if (objectsto == maxSite && objectsto > BOIList.Count())
                {
                    objectsto = BOIList.Count();
                }
                lbl_site.Text = $"Seite {currentSite} von {maxSite} ({objectsfrom}-{objectsto})";

                RefreshDGV();
            }
        }
    }
}