using ERP.BaseLib.Attributes;
using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.BaseLib.Statics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        internal string Namespace { get => this.GetType().Namespace.Replace(ParentNamespace + ".", ""); }

        /// <summary>
        /// Wether the command is been executed by the server.
        /// </summary>
        protected bool ServerSide { get; private set; }

        /// <summary>
        /// A list containing all commandcollections for caching.
        /// </summary>
        static List<CommandCollection> CommandCollectionList { get; set; } = new List<CommandCollection>();

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
                throw new Exception($"Missing Argument: {ArgumentName}");
            }

            return value;
        }

        /// <summary>
        /// Ensures that the userlogin is correct and the user has access.
        /// <para>
        /// If user has no access, exception will be thrown.
        /// </para>
        /// </summary>
        /// <param name="User">User</param>
        /// <param name="PermissionLevel">Needed Accesslevel</param>
        /// <returns>True if user has access</returns>
        /// <exception cref="Exception"></exception>
        protected static bool EnsureUser(User? User, int PermissionLevel) 
        {
            if(User is User user)
            {
                user = User.Login(User.Name, User.Password);
                if (user.LoggedIn && user.PermissionLevel >= PermissionLevel)
                {
                    return true;
                }
            }
            throw new Exception($"Command is protected and requires PermissionLevel {PermissionLevel}");
        }

        /// <summary>
        /// Serversided Execute-Method.
        /// </summary>
        /// <param name="Input">Data.</param>
        /// <returns>The Result wich will be handed to the client.</returns>
        /// <exception cref="Exception"></exception>
        private Result ExecuteCommandServer(DataInput Input) 
        {
            List<Object> Params = new List<object>();

            if (this.GetType().GetMethod(Input.Command) is MethodInfo MI)
            {
                if(MI.GetCustomAttribute<RequireLoginAttribute>() is RequireLoginAttribute RLA) 
                {
                    try
                    {
                        EnsureUser(Input.User, RLA.PermissionLevel);
                    }
                    catch 
                    {
                        throw;
                    }
                }
                foreach (ParameterInfo Parameter in MI.GetParameters())
                {
                    if (Parameter.ParameterType == Input.Arguments.GetType())
                    {
                        Params.Add(Input.Arguments);
                    }
                    if (Parameter.Name != null)
                    {
                        string arg = EnsureArgument(Input.Arguments, Parameter.Name, !Parameter.IsOptional);
                        if (Parameter.ParameterType == typeof(string))
                        {
                            Params.Add(arg);
                        }
                        else
                        {
                            try
                            {
                                Params.Add(Json.Deserialize(arg, Parameter.ParameterType));
                            }
                            catch
                            {
                                throw;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"Command: {Input.Command}, Parametertype is not supported: {Parameter.ParameterType}:{Parameter.Name}");
                    }
                }
                if (MI.ReturnType == typeof(Result))
                {
                    try
                    {
                        if (MI.Invoke(this, Params.ToArray()) is Result Result)
                        {
                            return Result;
                        }
                        else
                        {
                            throw new Exception($"Command: {Input.Command}, Command could not return a Result");
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
                    throw new Exception($"Command: {Input.Command}, Command mismatches the Returntype");
                }
            }
            else
            {
                throw new Exception($"Command: {Input.Command}, No such Command");
            }
        }

        /// <summary>
        /// Client-sideded Execute-Method.
        /// </summary>
        /// <param name="Input">Data.</param>
        /// <returns>The Result wich comes from the server.</returns>
        private Result ExecuteCommandClient(DataInput Input) 
        {
            var response = new HttpClient().PostAsync(Http.Url, new StringContent(Json.Serialize(Input)));
            string res = response.Result.Content.ReadAsStringAsync().Result;
            try
            {
                Result result = Json.Deserialize<Result>(res);
                return result;
            }
            catch(Exception ex)
            {
                return new Result(true, ex.Message);
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
        /// This Method makes a request on the server and returns the handed result.
        /// </summary>
        /// <param name="Arguments">Arguments must be in the correct order.</param>
        /// <returns>The Result wich comes from the server.</returns>
        protected Result GetClientResult(params object[] Arguments) 
        {
            Result Result = new Result(true, "Client: Unknown Error");

            if(new StackTrace().GetFrame(1) is StackFrame frame && frame.GetMethod() is MethodBase MB && this.GetType() is Type Type)
            {
                string? Namespace = this.Namespace;
                string? Class = Type.Name.Replace("CC_", "");
                string? Command = MB.Name;

                Dictionary<string, string> Parameters = new Dictionary<string, string>();

                int i = 0;
                foreach(ParameterInfo PI in MB.GetParameters()) 
                {
                    if (PI.Name != null)
                    {
                        if(Arguments.Length <= i) 
                        {
                            return new Result(true, $"Client: Cannot process Parameters, because of Argumentcount: {Type.Name}.{MB.Name}");
                        }
                        if (PI.ParameterType != Arguments[i].GetType())
                        {
                            return new Result(true, $"Client: Cannot process Parameters, because of Argumenttype: {Type.Name}.{MB.Name}, {PI.ParameterType.Name}!={Arguments[i].GetType().Name}");
                        }
                        Parameters.Add(PI.Name, Json.Serialize(Arguments[i]));
                    }

                    i++;
                }
                User User = null;
                if(Arguments.FirstOrDefault(o => o.GetType() == typeof(User)) is User user) 
                {
                    User = user;
                }

                return ExecuteCommand(new DataInput(User, Namespace, Class, Command, (ArgumentCollection)Parameters));

            }

            return Result;
        }

        /// <summary>
        /// Return an Instance of the given Type
        /// </summary>
        /// <typeparam name="T">Type of CommandCollection</typeparam>
        /// <returns>Instance of the given Type</returns>
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
        /// Return an Instance of the given Type
        /// </summary>
        /// <param name="Type">Type of CommandCollection</param>
        /// <returns>Instance of the given Type</returns>
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
                    Object? obj = Activator.CreateInstance(Type);
                    if (obj is CommandCollection CommandCollection)
                    {
                        CommandCollectionList.Add(CommandCollection);
                        return CommandCollection;
                    }
                    else
                    {
                        throw new Exception($"Could not create Instance of type {Type.Name}");
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
