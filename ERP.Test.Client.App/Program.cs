using ERP.BaseLib.Output;
using ERP.Business.Filtering;
using ERP.Commands.Base;
using ERP.Test.ObjectClients;
using ERP.Test.Public.Library.Objects;

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
                    Person person1 = new() { Firstname = "Nico", Name = "Volling", Birthday = DateOnly.FromDateTime(DateTime.Now) };

                    Client.Create(person1);
                    Client.GetData(person1.ID);
                    person1 = Client.Data;
                    Client.Delete(person1.ID);

                    Log.WriteLine(person1, ConsoleColor.Magenta);

                    Log.WriteLine("Test was successful", ConsoleColor.Magenta);

                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
    }
}