using System;
using System.Collections.Generic;

namespace SimpleHTTPServer.Actions
{
	public abstract class RestDirectory
	{
		protected Dictionary<string,RestDirectory> Actions;
		protected string Path;

		public RestDirectory (string path)
		{
			this.Path = path;
			this.Actions = new Dictionary<string, RestDirectory> ();
		}

		public virtual byte[] Get(string[] input)
		{
			if (input == null || input?.Length == 0)
			{
				return null;
			}

			if (this.Actions.ContainsKey (input [0]))
			{
				string[] remaining = new string[input.Length-1];
				Array.Copy (input, 1, remaining, 0, 0);
				return this.Actions [input [0]].Get (remaining);
			}

			return null;
		}

		public virtual byte[] Post(string[] input, byte[] data)
		{
			if (input == null || input?.Length == 0)
			{
				return null;
			}

			if (this.Actions.ContainsKey (input [0]))
			{
				string[] remaining = new string[input.Length-1];
				Array.Copy (input, 1, remaining, 0, 0);
				return this.Actions [input [0]].Post (remaining, data);
			}

			return null;
		}

		public virtual byte[] Put(string[] input, byte[] data)
		{
			if (input == null || input?.Length == 0)
			{
				return null;
			}

			if (this.Actions.ContainsKey (input [0]))
			{
				string[] remaining = new string[input.Length-1];
				Array.Copy (input, 1, remaining, 0, 0);
				return this.Actions [input [0]].Post (remaining, data);
			}

			return null;
		}

		public virtual byte[] Delete(string[] input, byte[] data)
		{
			if (input == null || input?.Length == 0)
			{
				return null;
			}

			if (this.Actions.ContainsKey (input [0]))
			{
				string[] remaining = new string[input.Length-1];
				Array.Copy (input, 1, remaining, 0, 0);
				return this.Actions [input [0]].Post (remaining, data);
			}

			return null;
		}

		public void Add(RestDirectory action)
		{
			this.Actions.Add (action.Path, action);
		}
	}
}

