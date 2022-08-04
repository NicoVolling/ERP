using ERP.Windows.WF.Base;
using ERP.Windows.WF.Binding.Controls;
using ERP.Windows.WF.Binding.Supervisor;

namespace ERP.Windows.WF.Binding.Forms
{
    public partial class BindingForm : BaseForm, IBindingParent
    {
        public BindingForm()
        {
            DataContext = GetDataContext();
            if (DataContext != null)
            {
                this.BindingMaster = new BindingMaster(this);
            }
            InitializeComponent();
        }

        public BindingMaster BindingMaster { get; protected set; }

        public DataContext DataContext { get; set; }

        protected virtual DataContext GetDataContext()
        {
            return null;
        }
    }
}