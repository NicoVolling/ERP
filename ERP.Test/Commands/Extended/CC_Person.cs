using ERP.BaseLib.Attributes;
using ERP.BaseLib.Objects;
using ERP.Business.Server;
using ERP.Test.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.Commands.Extended
{
    public class CC_Person : BusinessObjectServer<Person>
    {
        [RequireLogin(1)]
        protected override Person OnCreate(User User, Person Data)
        {
            return base.OnCreate(User, Data);
        }
    }
}
