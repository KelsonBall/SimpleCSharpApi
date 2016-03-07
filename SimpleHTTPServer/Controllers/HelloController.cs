using System;
using System.Text;

namespace SimpleHTTPServer.Controllers
{
	using Actions;

	public class HelloController : Action
	{
		public HelloController () : base("hello")
		{
		}

		public override byte[] Evaluate (string[] input)
		{
			base.Evaluate (input);
			string response = "Hello, World!";
			return Encoding.UTF8.GetBytes (response.ToCharArray());
		}


	}
}

