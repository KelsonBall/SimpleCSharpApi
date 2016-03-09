using System;
using System.Text;

namespace SimpleHTTPServer.Controllers
{
	using Actions;

	public class TimeController : EvaluationAction
	{
		public TimeController () : base("time")
		{
		}

		public override byte[] Get (string[] input)
		{
			return Encoding.UTF8.GetBytes(DateTime.Now.ToString ());
		}


	}
}

