using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.Client.WindowsForms;
using ERP.Commands.Base;
using ERP.Server.WebServer;
using ERP.Test.Commands.Base;
using ERP.Test.GUI;
using ERP.Test.ObjectClients;
using ERP.Test.Objects;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Windows.Forms;

namespace ERP.Server
{
    public static class Program
    {
        public static void Main(string[]? args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            CommandCollection.ParentNamespace = "ERP.Test.Commands";
            CommandCollection.CommandAssembly = Assembly.GetExecutingAssembly();


            Thread server = new Thread(() =>
            {
                try
                {
                    HttpServer Server = new HttpServer();
                    Server.Start(ERP.BaseLib.Statics.Http.ServerUrl);
                }
                catch
                {
                    throw;
                }
            });

            Thread client = new Thread(async () =>
            {
                try
                {
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Test was successful");
                    }
                }
                catch
                {
                    throw;
                }
            });

            server.Start();

            Thread.Sleep(1000);

            client.Start();

            BaseForm BF = new BaseForm("ERP-Test", Client.WindowsForms.Base.Resources.WindowIcon);
            BF.OpenWindow(new CP_Test());
            Application.Run(BF);

            Console.ReadLine();
        }


    }
}
