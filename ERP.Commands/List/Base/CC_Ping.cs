using ERP.BaseLib.Objects;
using ERP.Commands.Base;

namespace ERP.Commands.List.Base
{
    public class CC_Ping : CommandCollection
    {
        public Result Ping(Result Result)
        {
            if (!ServerSide) { return GetClientResult(Result); }
            Result.ReturnValue = "Pong";
            return Result;
        }
    }
}