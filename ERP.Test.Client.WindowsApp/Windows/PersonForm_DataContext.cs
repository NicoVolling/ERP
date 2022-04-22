using ERP.Business.Objects.Attributes;
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
        public Person Person { get; set; } = new Person();
    }
}