using System;
using System.Text;
using Newtonsoft.Json;

namespace SimpleHTTPServer.Controllers
{
	using Actions;
	using Repository;
	using Models;

	public class NoteController : EvaluationAction
	{
		public NoteController () : base("notes")
		{
		}

		public override byte[] Post (string[] input, byte[] bytes)
		{
			string inputText = Encoding.UTF8.GetString (bytes);
			NoteModel newNote = JsonConvert.DeserializeObject<NoteModel> (inputText);
			Repository.NoteModels.Add (newNote);
			return null;
		}

		public override byte[] Get (string[] input)
		{
			string outputText = JsonConvert.SerializeObject (Repository.NoteModels);
			byte[] bytes = Encoding.UTF8.GetBytes (outputText);
			return bytes;
		}
	}
}

