using System.Linq;
using Anilist4Net.Connections;

namespace Anilist4Net
{
	public class Studio
	{
		/// <summary>
		///     The studio ID
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     The studio name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     If the studio does animation, or some other kind of company
		/// </summary>
		public bool IsAnimationStudio { get; set; }

		private MediaNodeConnection Media { get; set; }

		/// <summary>
		///     The IDs of media the studio has worked on
		/// </summary>
		public int[] MediaIds => Media.Nodes.Select(n => n.Id).ToArray();

		/// <summary>
		///     The studio's Anilist URL
		/// </summary>
		public string SiteUrl { get; set; }

		/// <summary>
		///     How many users favourited the studio
		/// </summary>
		public int Favourites { get; set; }
	}

	internal class StudioResponse
	{
		public Studio Studio { get; set; }
	}
}