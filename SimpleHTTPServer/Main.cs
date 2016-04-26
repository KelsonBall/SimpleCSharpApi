using System.Collections.Generic;

namespace HttpServer
{
    using Controllers;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Server server = new Server(8080);
            server.ControllersTable.Add("notes", new List<IWebController>() { new NoteController() });
            server.ControllersTable.Add("gradient", new List<IWebController>() { new PageController("gradient") });
            server.ControllersTable.Add("reach", new List<IWebController>() { new PageController("reach") });
            server.Initialize();
        }
    }
}