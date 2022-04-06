using ERP.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
