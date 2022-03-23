using ERP.BaseLib.Attributes;
using ERP.BaseLib.Helpers;
using ERP.BaseLib.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Commands.Base
{
    /// <summary>
    /// Provides serveral Function for the Http-Server for handeling commands.
    /// </summary>
    public static class CommandMaster
    {
        /// <summary>
        /// All CommandCollectionTypes founded in the Namespace.
        /// </summary>
        static List<Type> CommandCollectionTypes = new List<Type>();

        /// <summary>
        /// Executes the command. Can only be used on server-side
        /// </summary>
        /// <param name="DataInput">Data.</param>
        /// <returns>The Result wich is output of the command.</returns>
        /// <exception cref="Exception"></exception>
        public static Result ExecuteCommand(DataInput DataInput)
        {
            Reload();
            if (CommandCollectionTypes.FirstOrDefault(o => o.Namespace?.Replace(CommandCollection.ParentNamespace + ".", "").EndsWith(DataInput.Command.Namespace) == true && o.Name.Replace("CC_", "") == DataInput.Command.Class && o.GetMethod(DataInput.Command.Action) != null) is Type CCType)
            {
                try
                {
                    return CommandCollection.GetInstance(CCType).ExecuteCommand(DataInput);
                }
                catch
                {
                    throw;
                }
            }
            else 
            {
                throw new Exception($"No such Command: {DataInput.Command.Namespace}.{DataInput.Command.Class}.{DataInput.Command}");
            }
        }

        /// <summary>
        /// Refreshes the list of CommandCollectionTypes if needed.
        /// </summary>
        public static void Reload() 
        {
            if (CommandCollectionTypes.Count < 1)
            {
                CommandCollectionTypes = new List<Type>();

                foreach (Type Type in CommandCollection.CommandAssembly.GetTypes().Where(o =>
                o.IsClass &&
                o.Namespace?.StartsWith(CommandCollection.ParentNamespace) == true &&
                ReflectionHelper.DoesInheritFrom(o, typeof(CommandCollection))))
                {
                    try
                    {
                        CommandCollection.GetInstance(Type);
                    } 
                    catch
                    {
                        throw;
                    }
                    CommandCollectionTypes.Add(Type);
                }
            }
        }
    }
}
