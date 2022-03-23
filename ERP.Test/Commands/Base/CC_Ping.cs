using ERP.BaseLib.Attributes;
using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.Commands.Base
{
    public class CC_Ping : CommandCollection
    {
        [RequireLogin(1)]
        public Result Ping(Result Result, User User) 
        {
            if(!ServerSide) { return GetClientResult(Result, User); }
            return Result;
        }
    }
}
