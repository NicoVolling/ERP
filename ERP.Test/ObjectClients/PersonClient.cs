using ERP.BaseLib.Objects;
using ERP.Business.Client;
using ERP.Test.Commands.Extended;
using ERP.Test.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.ObjectClients
{
    public class PersonClient : BusinessObjectClient<CC_Person, Person>
    {
        public PersonClient(User CurrentUser) : base(CurrentUser)
        {
        }
    }
}
