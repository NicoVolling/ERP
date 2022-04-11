using ERP.BaseLib.Output;
using System.Net;
using System.Text;

namespace ERP.BaseLib.WebServer
{
    /// <summary>
    /// The HttpServer wich must be started in order to handle request from the client.
    /// </summary>
    public abstract class HttpServer
    {
        private readonly HttpListener listener;
        private int reqCount = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public HttpServer()
        {
            listener = new HttpListener();
        }

        public bool ConsoleOutput { get; set; } = true;

        /// <summary>
        /// Determines if the server is currently running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Starts the server.
        /// </summary>
        public void Start(string Url)
        {
            IsRunning = true;
            listener.Prefixes.Add(Url);
            listener.Start();
            if (ConsoleOutput)
            {
                Log.WriteLine($"Listening for connections on {Url}", ConsoleColor.Green);
            }
            Task listenTask = HandleIncommingConnections();
            listenTask.GetAwaiter().GetResult();

            listener.Close();
        }

        protected abstract string GetResultFromRequest(HttpListenerRequest Request);

        private async Task HandleIncommingConnections()
        {
            while (IsRunning)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                if (req.Url?.AbsolutePath != "/favicon.ico")
                {
                    byte[] data = Encoding.UTF8.GetBytes(GetResultFromRequest(req));

                    if (ConsoleOutput)
                    {
                        Log.WriteLine($"Request #{++reqCount}", ConsoleColor.Yellow);
                    }

                    resp.ContentType = "text/html";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;
                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                }

                resp.Close();
            }
        }
    }
}