using ERP.BaseLib.Output;
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
            Log.Prefix = "Client";

            CommandCollection.ParentNamespace = "ERP.Test.Server.Library";

            try
            {
                {
                    PersonClient Client = new PersonClient();
                    Person person1 = new Person() { Firstname = "Nico", Name = "Volling", Birthday = DateTime.Now };

                    Client.Create(person1);
                    Client.GetData(person1.ID);
                    person1 = Client.Data;

                    Log.WriteLine(person1, ConsoleColor.Magenta);

                    Log.WriteLine("Test was successful", ConsoleColor.Magenta);
                }
            }
            catch
            {
                throw;
            }

            var WindowList = new Dictionary<string, Dictionary<string, Func<BaseWindow>>>()
            {
                { "Test",
                    new Dictionary<string, Func<BaseWindow>>()
                    {
                        { "Moin",() =>  new BaseWindow(new CP_Personen()) { Text = "Mooooin", Icon = ERP.Client.WindowsForms.Base.Resources.Icon } } 
                    } 
                } 
            };

            BaseForm BF;
            BF = new BaseForm("ERP-Test", ERP.Client.WindowsForms.Base.Resources.WindowIcon, WindowList);
            Application.Run(BF);
        }
    }
}
