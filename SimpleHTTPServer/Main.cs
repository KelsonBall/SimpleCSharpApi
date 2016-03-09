using SimpleHTTPServer.Server;
using SimpleHTTPServer.Actions;
using SimpleHTTPServer.Controllers;

public static class Program
{
	public static void Main(string[] args)
	{
		SimpleHTTPServer.Server.Server server = new SimpleHTTPServer.Server.Server ("localhost", 8080);
		ApiController api = new ApiController ();
		api.Add (new HelloController ());
		api.Add (new TimeController ());
		api.Add (new NoteController ());
		server.RootAction = api;
	}
}