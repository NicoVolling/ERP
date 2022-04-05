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
                        Result result = CC_Ping.GetInstance<CC_Ping>().Ping(Result.OK, new User("Nico", "Volling"));
                        if (!result.Error)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(Json.Serialize(result.ReturnValue));
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Test was successful");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Json.Serialize(result.ErrorMessage));
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine("Test has failed");
                        }

                        Person Nico = new Person() { Name = "Volling", FirstName = "Nico" };
                        PersonClient Client = new PersonClient(new User("Nico", "Volling"));

                        Client.Create(Nico);
                        Client.GetData(Nico.ID);
                        Client.Delete(Nico.ID);
                        var b = Client.GetList();

                        Console.WriteLine(String.Join(",", b));
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
