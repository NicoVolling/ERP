using ERP.BaseLib.Helpers;
using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.BaseLib.Statics;
using ERP.Exceptions.ErpExceptions;
using ERP.Exceptions.ErpExceptions.CommandExceptions;
using System.Diagnostics;
using System.Reflection;

namespace ERP.Commands.Base
{
    /// <summary>
    /// Contains several commands an functions for dealing with commands.
    /// </summary>
    public abstract class CommandCollection
    {
        /// <summary>
        /// The Assembly that contains all commands.
        /// </summary>
        public static Assembly CommandAssembly { get; set; } = Assembly.GetExecutingAssembly();

        /// <summary>
        /// The Basenamespace where all Commandcollections are listed.
        /// </summary>
        public static string ParentNamespace { get; set; } = "ERP.Commands.List";

        /// <summary>
        /// The relative namespace of this commandcollection
        /// </summary>
        protected internal string Namespace { get => this.GetType().Namespace.Replace(ParentNamespace + ".", ""); }

        /// <summary>
        /// Wether the command is been executed by the server.
        /// </summary>
        protected bool ServerSide { get; private set; }

        /// <summary>
        /// A list containing all commandcollections for caching.
        /// </summary>
        private static List<CommandCollection> CommandCollectionList { get; set; } = new List<CommandCollection>();

        /// <summary>
        /// Returns if an instance of the given type exists.
        /// </summary>
        /// <typeparam name="T">Type of CommandCollection</typeparam>
        /// <returns>Instance of the given Type.</returns>
        public static bool DoesInstanceExists<T>() where T : CommandCollection, new()
        {
            return CommandCollectionList.Any(o => o is T);
        }

        /// <summary>
        /// Returns if an instance of the given type exists.
        /// </summary>
        /// <param name="Type">Type of CommandCollection</param>
        /// <returns>Instance of the given Type.</returns>
        public static bool DoesInstanceExists(Type Type)
        {
            return CommandCollectionList.Any(o => o.GetType() == Type);
        }

        /// <summary>
        /// Return an Instance of the given Type.
        /// </summary>
        /// <typeparam name="T">Type of CommandCollection.</typeparam>
        /// <returns>Instance of the given Type.</returns>
        public static T GetInstance<T>() where T : CommandCollection, new()
        {
            if (CommandCollectionList.FirstOrDefault(o => o is T) is T CoCo)
            {
                return CoCo;
            }
            else
            {
                T Obj = new();
                CommandCollectionList.Add(Obj);
                return Obj;
            }
        }

        /// <summary>
        /// Return an Instance of the given Type.
        /// </summary>
        /// <param name="Type">Type of CommandCollection.</param>
        /// <returns>Instance of the given Type.</returns>
        internal static CommandCollection GetInstance(Type Type)
        {
            if (CommandCollectionList.FirstOrDefault(o => o.GetType() == Type) is CommandCollection CoCo)
            {
                return CoCo;
            }
            else
            {
                try
                {
                    Object obj = Activator.CreateInstance(Type);
                    if (obj is CommandCollection CommandCollection)
                    {
                        CommandCollectionList.Add(CommandCollection);
                        return CommandCollection;
                    }
                    else
                    {
                        throw new ReflectionErpException($"Couldn't create instance of {Type.Name}");
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Execute-Method. It automatically differenciate client-side and server-side.
        /// </summary>
        /// <param name="Input">Data.</param>
        /// <returns>Result wich comes from or will be handed to server.</returns>
        internal Result ExecuteCommand(DataInput Input)
        {
            if (new StackTrace().GetFrame(1)?.GetMethod()?.DeclaringType == typeof(CommandMaster))
            {
                ServerSide = true;
                Result Result = ExecuteCommandServer(Input);
                ServerSide = false;
                return Result;
            }
            else
            {
                ServerSide = false;
                Result Result = ExecuteCommandClient(Input);
                return Result;
            }
        }

        /// <summary>
        /// Ensures that the argument does exist and throws an exception if argument, that must exist does not exist.
        /// </summary>
        /// <param name="Arguments">Contains all arguments.</param>
        /// <param name="ArgumentName">Name of the searched argument.</param>
        /// <param name="MustExist">if exception should be thrown when argument is missing.</param>
        /// <returns>the serialized value of the argument</returns>
        /// <exception cref="Exception"></exception>
        protected static string EnsureArgument(ArgumentCollection Arguments, string ArgumentName, bool MustExist = true)
        {
            string value = Arguments.FirstOrDefault(o => o.Name == ArgumentName)?.Value;

            if (string.IsNullOrEmpty(value) && MustExist)
            {
                throw new MissingArgumentErpException(ArgumentName);
            }
            else if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value;
        }

        /// <summary>
        /// This Method makes a request on the server and returns the handed result.
        /// </summary>
        /// <param name="Arguments">Arguments must be in the correct order.</param>
        /// <returns>The Result wich comes from the server.</returns>
        protected Result GetClientResult(params object[] Arguments)
        {
            Result Result = new(new ErpException("Client: Unknown Error"));

            if (new StackTrace().GetFrame(1) is StackFrame frame && frame.GetMethod() is MethodBase MB && this.GetType() is Type Type)
            {
                string Namespace = this.Namespace;
                string Class = Type.Name.Replace("CC_", "");
                string Command = MB.Name;

                return GetClientResult(new Command(Namespace, Class, Command), Type, MB, Arguments);
            }

            return Result;
        }

        /// <summary>
        /// Client-sideded Execute-Method.
        /// </summary>
        /// <param name="Input">Data.</param>
        /// <returns>The Result wich comes from the server.</returns>
        private static Result ExecuteCommandClient(DataInput Input)
        {
            Task<HttpResponseMessage> response = new HttpClient().PostAsync(Http.ServerUrls.First(), new StringContent(Input.Serialize()));
            string res = response.Result.Content.ReadAsStringAsync().Result;
            try
            {
                Result result = Json.Deserialize<Result>(res);
                return result;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        /// <summary>
        /// Serversided Execute-Method.
        /// </summary>
        /// <param name="Input">Data.</param>
        /// <returns>The Result wich will be handed to the client.</returns>
        /// <exception cref="Exception"></exception>
        private Result ExecuteCommandServer(DataInput Input)
        {
            List<Object> Params = new();

            if (this.GetType().GetMethod(Input.Command.Action) is MethodInfo MI)
            {
                foreach (ParameterInfo Parameter in MI.GetParameters())
                {
                    if (Parameter.ParameterType == Input.Arguments.GetType())
                    {
                        Params.Add(Input.Arguments);
                    }
                    if (Parameter.Name != null)
                    {
                        string arg = EnsureArgument(Input.Arguments, Parameter.Name, !Parameter.IsOptional);
                        try
                        {
                            Object Param = Json.Deserialize(arg, Parameter.ParameterType);
                            if (Param is null && !Parameter.IsOptional)
                            {
                                throw new MissingArgumentErpException(Parameter.Name);
                            }
                            Params.Add(Param);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    else
                    {
                        throw new ParameterNotSupportedErpException(Parameter.ParameterType);
                    }
                }
                if (MI.ReturnType.BaseType == typeof(Result))
                {
                    try
                    {
                        if (MI.Invoke(this, Params.ToArray()) is Result Result)
                        {
                            return Result;
                        }
                        else
                        {
                            throw new CommandExecutionErpException(Input.Command.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException is Exception exx)
                        {
                            throw exx;
                        }
                        throw;
                    }
                }
                else
                {
                    throw new CommandExecutionErpException(Input.Command.ToString());
                }
            }
            else
            {
                throw new CommandNotFoundErrorException(Input.Command.ToString());
            }
        }

        /// <summary>
        /// This Method makes a request on the server and returns the handed result.
        /// </summary>
        /// <param name="Command">The Command that should be executed</param>
        /// <param name="Type">The Type of the commandocllection</param>
        /// <param name="Arguments">Arguments must be in the correct order.</param>
        /// <returns>The Result wich comes from the server.</returns>
        private Result GetClientResult(Command Command, Type Type, MethodBase MethodBase, params object[] Arguments)
        {
            Result Result = new(new ErpException("Client: Unknown Error"));

            if (Type.GetMethod(MethodBase.Name) is MethodInfo MI)
            {
                Dictionary<string, string> Parameters = new();

                int i = 0;
                foreach (ParameterInfo PI in MI.GetParameters())
                {
                    if (PI.Name != null)
                    {
                        if (Arguments.Length <= i)
                        {
                            return new(new CommandErpException($"Client: Cannot process Parameters, because of Argumentcount: {Type.Name}.{MethodBase.Name}"));
                        }
                        if (Arguments[i].GetType() != PI.ParameterType && !ReflectionHelper.DoesInheritFrom(Arguments[i].GetType(), PI.ParameterType))
                        {
                            return new(new CommandErpException($"Client: Cannot process Parameters, because of Argumenttype: {Type.Name}.{MethodBase.Name}, {PI.ParameterType.Name}!={Arguments[i].GetType().Name}"));
                        }
                        Parameters.Add(PI.Name, Arguments[i].Serialize());
                    }

                    i++;
                }

                return ExecuteCommand(new DataInput(Command, (ArgumentCollection)Parameters));
            }
            return Result;
        }
    }
}