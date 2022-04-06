using ERP.Client.WindowsForms;
using ERP.Commands.Base;
using ERP.Test.Client.Library.GUI;
using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Windows.Forms;

namespace ERP.Test.Client.App
{
    public static class Program
    {
        public static void Main(string[]? args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            CommandCollection.ParentNamespace = "ERP.Test.Server.Library";

            try
            {
                {
                    PersonClient Client = new PersonClient();
                    Person person1 = new Person() { Firstname = "Nico", Name = "Volling", Birthday = DateTime.Now };

                    Client.Create(person1);
                    Client.GetData(person1.ID);
                    person1 = Client.Data;

                    Console.WriteLine(person1);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Test was successful");
                }
            }
            catch
            {
                throw;
            }

            BaseForm BF = new BaseForm("ERP-Test", ERP.Client.WindowsForms.Base.Resources.WindowIcon);
            BF.OpenWindow(new CP_Test());
            Application.Run(BF);
        }
    }
}
