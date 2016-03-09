using System;
using System.Collections.Generic;

namespace SimpleHTTPServer.Repository
{
	using Models;

	public static class Repository
	{
		public static List<NoteModel> NoteModels { get; set; } = new List<NoteModel>();
	}
}

