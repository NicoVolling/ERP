using ERP.BaseLib.Objects;
using ERP.Business.Client;
using ERP.Test.Public.Library.Objects;
using ERP.Test.Server.Library.Commands.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.ObjectClients
{
    public class PersonClient : BusinessObjectClient<CC_Person, Person>
    {
    }
}
