using ERP.BaseLib.Objects;
using ERP.Commands.Base;

namespace ERP.Test.Server.Library.Commands.Base
{
    public class CC_Ping : CommandCollection
    {
        public Result<bool> Ping()
        {
            if (!ServerSide) { return GetClientResult().ToGenericResult<bool>(); }
            return new(true);
        }
    }
}