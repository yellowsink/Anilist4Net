using System.Linq;
using Anilist4Net.Connections;
using Anilist4Net.Enums;

namespace Anilist4Net
{
	public class Staff
	{
		/// <summary>
		///     The ID of the staff
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     The name of the staff in Romaji and Native
		/// </summary>
		public MultiLanguageName Name { get; set; }

		/// <summary>
		///     The native language of the staff
		/// </summary>
		public StaffLanguages Language { get; set; }

		/// <summary>
		///     The description in Markdown
		/// </summary>
		public string DescriptionMd { get; set; }

		/// <summary>
		///     The description in HTML
		/// </summary>
		public string DescriptionHtml { get; set; }

		/// <summary>
		///     The Anilist URL
		/// </summary>
		public string SiteUrl { get; set; }

		public MediaNodeConnection StaffMedia { get; set; }

		/// <summary>
		///     The IDs of the media the staff has a production role on
		/// </summary>
		public int?[] StaffMediaIds => StaffMedia.Nodes.Select(n => n.Id).Cast<int?>().ToArray();

		public CharacterConnection Characters { get; set; }

		/// <summary>
		///     IDs of characters the staff voiced
		/// </summary>
		public int?[] CharacterIds => Characters.Edges != null
			                              ? Characters.Edges.Select(n => n.Node.Id).Cast<int?>().ToArray()
			                              : new int?[0];

		public MediaNodeConnection CharacterMedia { get; set; }

		/// <summary>
		///     IDs of media the staff voice acted in
		/// </summary>
		public int?[] CharacterMediaIds => CharacterMedia.Nodes != null
			                                   ? CharacterMedia.Nodes.Select(n => n.Id).Cast<int?>().ToArray()
			                                   : new int?[0];

		/// <summary>
		///     How many users have favourited the staff
		/// </summary>
		public int Favourites { get; set; }

		/// <summary>
		///     Mods Notes
		/// </summary>
		public string ModNotes { get; set; }
	}

	public class StaffResponse
	{
		public Staff Staff { get; set; }
	}
}