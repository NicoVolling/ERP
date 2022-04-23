using ERP.Business.Client;
using ERP.Business.Objects;
using ERP.Business.Objects.Attributes;
using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;
using ERP.Windows.WF.Binding.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.Client.WindowsApp.Windows
{
    public class PersonForm_DataContext : DataContext
    {
        private IBusinessObjectClient client;
        private List<BusinessObjectIdentifier> objects;
        private Person person;

        public IBusinessObjectClient Client
        { get => client; set { client = value; NotifyPropertyChanged(nameof(Client)); } }

        public List<BusinessObjectIdentifier> Objects
        { get => objects; set { objects = value; NotifyPropertyChanged(nameof(Objects)); } }

        public Person Person
        {
            get => person;
            set { person = value; NotifyPropertyChanged(nameof(Person)); }
        }
    }
}