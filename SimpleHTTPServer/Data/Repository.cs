using System.Collections.Generic;

namespace HttpServer.Data
{
	using Models;

	public static class Repository
	{
		public static List<NoteModel> NoteModels { get; set; } = new List<NoteModel>();
	}
}

