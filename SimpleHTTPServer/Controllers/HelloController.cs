using System;
using System.Text;

namespace SimpleHTTPServer.Controllers
{
	using Actions;

	public class HelloController : EvaluationAction
	{
		public HelloController () : base("hello")
		{
		}

		public override byte[] Get(string[] input)
		{
			base.Get (input);
			string response = "Hello, World!";
			return Encoding.UTF8.GetBytes (response.ToCharArray());
		}


	}
}

