using System;

namespace SimpleHTTPServer.Controllers
{
	using Actions;

	public class ApiController : RestDirectory
	{
		public ApiController () : base("api")
		{
		}
	}
}

