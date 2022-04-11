using ERP.BaseLib.Output;
using ERP.Server.WebServer;
using ERP.Test.Server.Library;

namespace ERP.Test.Server.App
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Prefix = "Server";

            Startup.SetAssembly();

            HttpServer Server = new();
            Server.Start(ERP.BaseLib.Statics.Http.ServerUrl);
        }
    }
}