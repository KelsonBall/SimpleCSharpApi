using System;
using System.Collections.Generic;

namespace SimpleHTTPServer.Actions
{
	public abstract class Action
	{
		protected Dictionary<string,Action> Actions;
		protected string Path;

		public Action (string path)
		{
			this.Path = path;
			this.Actions = new Dictionary<string, Action> ();
		}

		public virtual byte[] Evaluate(string[] input)
		{
			if (this.Actions.ContainsKey (input [0]))
			{
				string[] remaining = new string[input.Length-1];
				Array.Copy (input, 1, remaining, 0, 0);
				this.Actions [input [0]].Evaluate (remaining);
			}

			return (byte[])null;
		}

		public void Add(Action action)
		{
			this.Actions.Add (action.Path, action);
		}
	}
}

