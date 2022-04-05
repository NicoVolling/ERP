using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Commands.List.Base
{
    public class CC_Ping : CommandCollection
    {
        public Result Ping(Result Result) 
        {
            if(!ServerSide) { return GetClientResult(Result); }
            Result.ReturnValue = "Pong";
            return Result;
        }
    }
}
