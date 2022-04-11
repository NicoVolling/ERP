using ERP.Commands.Base;
using System.Reflection;

namespace ERP.Test.Server.Library
{
    public static class Startup
    {
        public static void SetAssembly()
        {
            CommandCollection.ParentNamespace = "ERP.Test.Server.Library";
            CommandCollection.CommandAssembly = Assembly.GetExecutingAssembly();
        }
    }
}