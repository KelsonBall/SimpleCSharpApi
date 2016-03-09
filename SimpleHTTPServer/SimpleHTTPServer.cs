using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace SimpleHTTPServer.Server
{	
	using Actions;

	public class Server
	{	
			
		private Thread _serverThread;
		private string _rootDirectory;
		private HttpListener _listener;
		private int _port;

		public RestDirectory RootAction { get; set; }

		public int Port
		{
			get { return _port; }
			private set { }
		}

		/// <summary>
		/// Construct server with given port.
		/// </summary>
		/// <param name="path">Directory path to serve.</param>
		/// <param name="port">Port of the server.</param>
		public Server(string path, int port)
		{
			this.Initialize(path, port);
		}

		/// <summary>
		/// Construct server with suitable port.
		/// </summary>
		/// <param name="path">Directory path to serve.</param>
		public Server(string path)
		{
			//get an empty port
			TcpListener l = new TcpListener(IPAddress.Loopback, 0);
			l.Start();
			int port = ((IPEndPoint)l.LocalEndpoint).Port;
			l.Stop();
			this.Initialize(path, port);
		}

		/// <summary>
		/// Stop server and dispose all functions.
		/// </summary>
		public void Stop()
		{
			_serverThread.Abort();
			_listener.Stop();
		}

		public void Start()
		{
			//_serverThread.Start();
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

			string[] path = (urlData.Substring (1)).Split ('/');
			byte[] inputBytes = null;

			if (context.Request.ContentLength64 > 0)
			{
				inputBytes = context.Request.InputStream.ReadToEnd ();
				Console.WriteLine(Encoding.UTF8.GetString(inputBytes));
			}

			byte[] output = null;

			if (context.Request.HttpMethod.Equals("GET"))
			{
				output = this.RootAction.Get (path);	
			}
			else if (context.Request.HttpMethod.Equals("POST")) 
			{
				output = this.RootAction.Post (path, inputBytes);
			}


			if (output == null)
			{
				//string response = "Null output buffer.";
				//context.Response.OutputStream.Write (Encoding.UTF8.GetBytes (response), 0, response.Length);
			}
			else
			{
				
				context.Response.OutputStream.Write (output, 0, output.Length);
			}

			context.Response.OutputStream.Flush ();

			context.Response.OutputStream.Close();
		}

		private void Initialize(string path, int port)
		{
			this._rootDirectory = path;
			this._port = port;
			_serverThread = new Thread(this.Listen);
			_serverThread.Start ();
		}


	}
}




