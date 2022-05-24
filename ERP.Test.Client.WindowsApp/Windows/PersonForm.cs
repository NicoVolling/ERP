using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;
using ERP.Windows.WF.Binding.Controls;
using ERP.Windows.WF.Binding.Forms;

namespace ERP.Test.Client.WindowsApp.Windows
{
    public partial class PersonForm : BindingForm
    {
        public PersonForm()
        {
            InitializeComponent();
        }

        public new PersonForm_DataContext DataContext { get => base.DataContext as PersonForm_DataContext; set => base.DataContext = value; }

        protected override DataContext GetDataContext()
        {
            return new PersonForm_DataContext();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DataContext.Client = new PersonClient();
            DataContext.Person = new Person();
            DataContext.Objects = DataContext.Client.GetList();
        }

        private void bindableTextBox4_BeforeButtonClick(object sender, EventArgs e)
        {
            DataContext.Objects = DataContext.Client.GetList();
        }

        private void bindableTextBox4_ButtonClick(object sender, Business.Objects.BusinessObject e)
        {
            if (e != null)
            {
                DataContext.Person = e as Person;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataContext.Client.GetExistence(DataContext.Person.ID))
                {
                    DataContext.Client.Delete(DataContext.Person.ID);
                    DataContext.Person = new Person();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            try
            {
                DataContext.Person = new Person();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (this.BindingMaster.Bindables.Any(o => o.Status == ERP.Windows.WF.Base.InputStatus.Error))
            {
                MessageBox.Show("Es liegen Fehler vor.");
            }
            else
            {
                try
                {
                    if (DataContext.Client.GetExistence(DataContext.Person.ID))
                    {
                        DataContext.Client.Change(DataContext.Person.ID, DataContext.Person);
                        DataContext.Person = DataContext.Client.BO_Data as Person;
                    }
                    else
                    {
                        DataContext.Client.Create(DataContext.Person);
                        DataContext.Person = DataContext.Client.BO_Data as Person;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}