using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.BaseLib.Statics;
using ERP.Commands.Base;

namespace ERP.Server.WebServer
{
    /// <summary>
    /// The HttpServer wich must be started in order to handle request from the client.
    /// </summary>
    public sealed class HttpServer
    {
        private HttpListener listener;
        private int reqCount = 0;

        /// <summary>
        /// Determines if the server is currently running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HttpServer()
        {
            listener = new HttpListener();
            CommandMaster.Reload();
        }

        private async Task HandleIncommingConnections() 
        {
            while(IsRunning) 
            {
                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                if (req.Url?.AbsolutePath != "/favicon.ico")
                {
                    byte[] data = Encoding.UTF8.GetBytes(GetDataString(req));

                    ConsoleColor cl = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Request #{++reqCount}");
                    Console.WriteLine("{");
                    Console.ForegroundColor = cl;
                    Console.WriteLine($"    [{req.HttpMethod}] " + req.Url.ToString());
                    Console.WriteLine($"    \"{req.UserHostName}\" - " + req.UserHostAddress);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("}" + Environment.NewLine);

                    resp.ContentType = "text/html";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;
                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                }

                resp.Close();
            }
        }

        private string GetDataString(HttpListenerRequest Request)
        {
            Result Result = new Result(true, "UnknownError");

            if(Request == null) 
            {
                Result = new Result(true, "Request is null");
            }
            else
            {
                string datastring = string.Empty;
                if (Request.HttpMethod != "POST")
                {
                    Result.Error = true;
                    Result.ErrorMessage = "Method must be POST";
                }
                else
                {
                    using (var Reader = new StreamReader(Request.InputStream))
                    {
                        datastring = Reader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(datastring))
                    {
                        try
                        {
                            DataInput Input = Json.Deserialize<DataInput>(datastring);
                            Result = CommandMaster.ExecuteCommand(Input);
                        }
                        catch (Exception ex)
                        {
                            Result.Error = true;
                            Result.ErrorMessage = ex.Message;
                        }
                    }
                    else
                    {
                        Result.Error = true;
                        Result.ErrorMessage = "Missing command";
                    }
                }
            }

            string resultObject = Json.Serialize(Result);
            if (Result.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            return resultObject;
        }

        /// <summary>
        /// Starts the server.
        /// </summary>
        public void Start()
        {
            IsRunning = true;
            listener.Prefixes.Add(Http.Url);
            listener.Start();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Listening for connections on {Http.Url}");

            Task listenTask = HandleIncommingConnections();
            listenTask.GetAwaiter().GetResult();

            listener.Close();
        }
    }
}
