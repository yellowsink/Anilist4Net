using System.Linq;
using Anilist4Net.Connections;
using Anilist4Net.Enums;

namespace Anilist4Net
{
	public class Staff
	{
		public  int                 Id                { get; set; }
		private CharacterName       Name              { get; set; }
		public  StaffLanguages      Language          { get; set; }
		public  string              DescriptionMd     { get; set; }
		public  string              DescriptionHtml   { get; set; }
		public  string              SiteUrl           { get; set; }
		private MediaNodeConnection StaffMedia        { get; set; }
		public  int?[]              StaffMediaIds     => StaffMedia.Nodes.Select(n => n.Id).Cast<int?>().ToArray();
		private CharacterConnection Characters        { get; set; }
		public  int?[]              CharacterIds      => Characters.Edges.Select(n => n.Node.Id).Cast<int?>().ToArray();
		private MediaNodeConnection CharacterMedia    { get; set; }
		public  int?[]              CharacterMediaIds => CharacterMedia.Nodes.Select(n => n.Id).Cast<int?>().ToArray();
		public  int                 Favourites        { get; set; }
		public  string              ModNotes          { get; set; }
	}

	public class StaffResponse
	{
		public Staff Staff { get; set; }
	}
}