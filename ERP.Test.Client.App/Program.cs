using ERP.Client.WindowsForms;
using ERP.Client.WindowsForms.Controls.Windows;
using ERP.Commands.Base;
using ERP.Test.Client.Library.GUI;
using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;
using System.Diagnostics;
using System.Drawing;
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

            var WindowList = new Dictionary<string, Dictionary<string, BaseWindow>>()
            {
                { "Test",
                    new Dictionary<string, BaseWindow>()
                    {
                        { "Moin", new BaseWindow(new CP_Test()) { Text = "Mooooin", Icon = ERP.Client.WindowsForms.Base.Resources.Icon, StatusColor = Color.Green } } 
                    } 
                } 
            };

            BaseForm BF;
            BF = new BaseForm("ERP-Test", ERP.Client.WindowsForms.Base.Resources.WindowIcon, WindowList);
            Application.Run(BF);
        }
    }
}
