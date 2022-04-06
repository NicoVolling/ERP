using ERP.Server.WebServer;
using ERP.Test.Server.Library;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;

namespace ERP.Test.Server.App
{
    public static class Program
    {
        public static void Main(string[]? args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Startup.SetAssembly();

            HttpServer Server = new HttpServer();
            Server.Start(ERP.BaseLib.Statics.Http.ServerUrl);
        }
    }
}
