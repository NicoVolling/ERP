﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ERP.BaseLib.Objects;
using ERP.BaseLib.Serialization;
using ERP.BaseLib.Statics;
using ERP.Commands.Base;
using ERP.Exceptions.ErpExceptions;
using ERP.BaseLib.Output;

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
            Result Result = new Result(new ErpException("UnknownError"));

            if(Request == null) 
            {
                Result = new Result(new ErpException("Request is null"));
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
            }

            string resultObject = Json.Serialize(Result);

            return resultObject;
        }
    }
}
