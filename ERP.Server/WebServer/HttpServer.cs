using ERP.BaseLib.Objects;
using ERP.BaseLib.Output;
using ERP.BaseLib.Serialization;
using ERP.Commands.Base;
using ERP.Exceptions.ErpExceptions;
using System.Net;

namespace ERP.Server.WebServer
{
    /// <summary>
    /// The HttpServer wich must be started in order to handle request from the client.
    /// </summary>
    public sealed class HttpServer : ERP.BaseLib.WebServer.HttpServer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HttpServer()
        {
            this.ConsoleOutput = false;
            CommandMaster.Reload();
        }

        protected override string GetResultFromRequest(HttpListenerRequest Request)
        {
            Result Result = new(new ErpException("UnknownError"));

            if (Request == null)
            {
                Result = new(new ErpException("Request is null"));
            }
            else
            {
                bool documentation = Request.Url?.AbsolutePath.StartsWith("/documentation/") == true;

                if (Request.HttpMethod == "GET" && documentation)
                {
                    try
                    {
                        return DocumentationMaster.GetDocumentationPage(Request.Url?.AbsolutePath).ToString();
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                else if (Request.HttpMethod == "POST")
                {
                    using StreamReader Reader = new(Request.InputStream);

                    string datastring = Reader.ReadToEnd();

                    if (!string.IsNullOrEmpty(datastring))
                    {
                        try
                        {
                            DataInput Input = GetInputFromData(datastring);
                            try
                            {

                                Result = CommandMaster.ExecuteCommand(Input);
                            }
                            catch (Exception ex)
                            {
                                Result.Error = true;
                                Result.ErrorMessage = ex.Message;
                            }
                            Log.WriteLine($"Executed Command: {Input.Command}", ConsoleColor.Green);
                            if (documentation)
                            {
                                return DocumentationMaster.GetDocumentationRequestPage(Result).ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Result.Error = true;
                            Result.ErrorMessage = ex.Message;
                            Log.WriteLine($"Received an Error: {ex.GetType().Name}", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        Result.Error = true;
                        Result.ErrorMessage = "Missing command";
                    }
                }
                else
                {
                    Result.Error = true;
                    Result.ErrorMessage = "HttpMethod must be POST";
                }
            }

            return Result.Serialize();
        }

        private static DataInput GetInputFromData(string Data)
        {
            DataInput Input = null;
            if (Data.StartsWith("Namespace"))
            {
                try
                {
                    string Namespace = "";
                    string CommandCollection = "";
                    string Action = "";
                    foreach (string item in Data.Split("\r\n", StringSplitOptions.RemoveEmptyEntries))
                    {
                        string[] kvp = item.Split('=');
                        string name = kvp[0];
                        string value = kvp[1];
                        switch (name)
                        {
                            case "Namespace":
                                Namespace = value;
                                break;

                            case "CommandCollection":
                                CommandCollection = value;
                                break;

                            case "Action":
                                Action = value;
                                Input = new DataInput(new(Namespace, CommandCollection, Action));
                                break;

                            default:
                                string val = value.TrimStart().StartsWith("{") || value.TrimStart().StartsWith("[") ? value : "\"" + value + "\"";
                                Input.Arguments.Add(new(name, val));
                                break;
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    Input = Json.Deserialize<DataInput>(Data);
                }
                catch
                {
                    throw;
                }
            }
            return Input;
        }
    }
}