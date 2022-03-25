using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.BaseLib.Statics;
using ERP.Exceptions.ErpExceptions;

namespace ERP.BaseLib.WebServer
{
    /// <summary>
    /// The HttpServer wich must be started in order to handle request from the client.
    /// </summary>
    public abstract class HttpServer
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
                    byte[] data = Encoding.UTF8.GetBytes(GetResultFromRequest(req));

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

        protected abstract string GetResultFromRequest(HttpListenerRequest Request);

        /// <summary>
        /// Starts the server.
        /// </summary>
        public void Start(string Url)
        {
            IsRunning = true;
            listener.Prefixes.Add(Url);
            listener.Start();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Listening for connections on {Url}");

            Task listenTask = HandleIncommingConnections();
            listenTask.GetAwaiter().GetResult();

            listener.Close();
        }
    }
}
