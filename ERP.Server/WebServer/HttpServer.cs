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

            bool isWebSendedRequest = false;

            if (Request == null)
            {
                Result = new(new ErpException("Request is null"));
            }
            else
            {
                string datastring = string.Empty;
                if (Request.HttpMethod == "GET")
                {
                    try
                    {
                        return DocumentationMaster.GetDocumentationPage(Request.Url?.AbsolutePath);
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                else if (Request.HttpMethod == "POST")
                {
                    using StreamReader Reader = new(Request.InputStream);
                    datastring = Reader.ReadToEnd();

                    if (!string.IsNullOrEmpty(datastring))
                    {
                        try
                        {
                            DataInput Input = GetInputFromData(datastring, out isWebSendedRequest);
                            Result = CommandMaster.ExecuteCommand(Input);
                            Log.WriteLine($"Executed Command: {Input.Command}", ConsoleColor.Green);
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
                    Result.ErrorMessage = "Method must be POST";
                }
            }

            string resultObject = Result.Serialize();

            if (isWebSendedRequest)
            {
                resultObject = DocumentationMaster.GetDocumentationRequlstPage(Result);
            }

            return resultObject;
        }

        private static DataInput GetInputFromData(string Data, out bool IsWebSendedRequest)
        {
            DataInput Input = null;
            if (Data.StartsWith("Namespace"))
            {
                IsWebSendedRequest = true;
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
                    IsWebSendedRequest = false;
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