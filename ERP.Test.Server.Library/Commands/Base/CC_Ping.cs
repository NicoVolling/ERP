﻿using ERP.BaseLib.Objects;
using ERP.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Test.Server.Library.Commands.Base
{
    public class CC_Ping : CommandCollection
    {
        public Result Ping(Result Result) 
        {
            if(!ServerSide) { return GetClientResult(Result); }
            return Result;
        }
    }
}