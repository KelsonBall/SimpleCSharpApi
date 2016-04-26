using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using HttpServer.Controllers;

namespace HttpServer
{    
    public class Server
	{	
			
		private Thread _serverThread;		
		private HttpListener _listener;
		private int _port;

		public Dictionary<string, List<IWebController>> ControllersTable { get; set; } = new Dictionary<string, List<IWebController>>();

		public int Port
		{
			get { return _port; }
			private set { }
		}

		/// <summary>
		/// Construct server with given port.
		/// </summary>
		public Server(int port)
		{
            this._port = port;            
		}

		/// <summary>
		/// Construct server with suitable port.
		/// </summary>		
		public Server()
		{
			//get an empty port
			TcpListener l = new TcpListener(IPAddress.Loopback, 0);
			l.Start();
			int port = ((IPEndPoint)l.LocalEndpoint).Port;
			l.Stop();			
		}
                
		private void Listen()
		{
			_listener = new HttpListener();
			_listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
			_listener.Start();
			while (true)
			{
				try
				{
					HttpListenerContext context = _listener.GetContext();
					Process(context);
				}
				catch (Exception ex)
				{
					Console.WriteLine (ex.Message);
				}
			}
		}

		private void Process(HttpListenerContext context)
		{
			string urlData = context.Request.Url.AbsolutePath;
			Console.WriteLine(urlData);

		    Uri url = context.Request.Url;
		    string key = (context.Request.UrlReferrer == null) 
                         ? url.AbsolutePath.Split('/')[1] 
                         : context.Request.UrlReferrer.AbsolutePath.Split('/')[1];
            
            if (!ControllersTable.ContainsKey(key))
		    {
		        Console.WriteLine($@"Attempt to find missing key: {key}");
                goto ProceessEnd;
		    }

		    switch (context.Request.HttpMethod)
		    {
                case "GET":
		            this.ControllersTable[key].ForEach(webController => webController.Get(ref context));
		            break;
                case "POST":
                    this.ControllersTable[key].ForEach(webController => webController.Post(ref context));           
                    break;
                case "PUT":
                    this.ControllersTable[key].ForEach(webController => webController.Put(ref context));
                    break;
                case "PATCH":
                    this.ControllersTable[key].ForEach(webController => webController.Patch(ref context));
                    break;
                case "DELETE":
                    this.ControllersTable[key].ForEach(webController => webController.Delete(ref context));
                    break;
            }		   

            ProceessEnd:

			context.Response.OutputStream.Flush ();
			context.Response.OutputStream.Close();
		}

		public void Initialize()
		{						
			_serverThread = new Thread(this.Listen);
			_serverThread.Start ();
		}

        /// <summary>
		/// Stop server and dispose all functions.
		/// </summary>
		public void Stop()
        {
            _serverThread.Abort();
            _listener.Stop();
        }
    }
}




