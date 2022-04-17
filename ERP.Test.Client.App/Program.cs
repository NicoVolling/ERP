using ERP.BaseLib.Output;
using ERP.Client.WindowsForms;
using ERP.Client.WindowsForms.Controls.BindableControls;
using ERP.Client.WindowsForms.Controls.Windows;
using ERP.Commands.Base;
using ERP.Test.Client.Library.GUI;
using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;
using System.Windows.Forms;

namespace ERP.Test.Client.App
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Log.Prefix = "Client";

            CommandCollection.ParentNamespace = "ERP.Test.Server.Library";

            try
            {
                {
                    PersonClient Client = new();
                    Person person1 = new() { Firstname = "Nico", Name = "Volling", Birthday = DateTime.Now };

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

            Dictionary<string, Dictionary<string, Func<BaseWindow>>> WindowList = new()
            {
                {
                    "Menschen",
                    new Dictionary<string, Func<BaseWindow>>()
                    {
                        { "Personen", () =>  new BaseWindow(new CP_Personen()) { Text = "Personen", Icon = ERP.Client.WindowsForms.Base.Resources.Icon, CanMaximize = false, CanResize = false } }
                    }
                },
                {
                    "GUI",
                    new Dictionary<string, Func<BaseWindow>>()
                    {
                        { "Selection", () =>
                        {
                            BaseWindow BW = new BaseWindow(new B_BO_Selection_ContentPanel()) { Text = "Selection", Icon = ERP.Client.WindowsForms.Base.Resources.Icon, CanMaximize = true, CanResize = true };
                            ((B_BO_Selection_ContentPanel)BW.ContentPanel).SetBusinessObjectList(new PersonClient(), new PersonClient().GetList());
                            return BW;
                        }
                        }
                    }
                }
            };

            BaseForm BF;
            BF = new BaseForm("ERP-Test", ERP.Client.WindowsForms.Base.Resources.WindowIcon, WindowList);
            Application.Run(BF);
        }
    }
}