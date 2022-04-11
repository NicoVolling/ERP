using ERP.BaseLib.Objects;
using ERP.Commands.Base;

namespace ERP.Test.Server.Library.Commands.Base
{
    public class CC_Ping : CommandCollection
    {
        public Result Ping(Result Result)
        {
            if (!ServerSide) { return GetClientResult(Result); }
            return Result;
        }
    }
}