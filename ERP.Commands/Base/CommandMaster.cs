using ERP.BaseLib.Helpers;
using ERP.BaseLib.Objects;
using ERP.BaseLib.Output;
using ERP.Exceptions.ErpExceptions.CommandExceptions;
using ERP.IO.FileSystem;

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
        private static List<Type> CommandCollectionTypes = new();

        private static Timer timer;

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
                throw new CommandNotFoundEroException(DataInput.Command.ToString());
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
            if (timer is null)
            {
                timer = new Timer(new TimerCallback((o) =>
                {
                    Save();
                    Log.WriteLine("Data has been saved", ConsoleColor.Blue);
                }), null, 150000, 150000); //2:30 min.
            }
        }

        /// <summary>
        /// Executes Save() on every CommandCollection that is in Cache.
        /// </summary>
        public static void Save()
        {
            foreach (Type Type in CommandCollectionTypes)
            {
                if (CommandCollection.DoesInstanceExists(Type))
                {
                    if (CommandCollection.GetInstance(Type) is IFileSaver FS)
                    {
                        FS.Save();
                    }
                }
            }
        }
    }
}