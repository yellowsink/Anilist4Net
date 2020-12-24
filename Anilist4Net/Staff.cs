using System.Linq;

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
		public  int[]               StaffMediaIds     => StaffMedia.Nodes.Select(n => n.Id).ToArray();
		private CharacterConnection Characters        { get; set; }
		public  int[]               CharacterIds      => Characters.Edges.Select(n => n.Node.Id).ToArray();
		private MediaNodeConnection CharacterMedia    { get; set; }
		public  int[]               CharacterMediaIds => CharacterMedia.Nodes.Select(n => n.Id).ToArray();
		public  int                 Favourites        { get; set; }
		public  string              ModNotes          { get; set; }
	}

	internal class StaffResponse
	{
		public Staff Staff { get; set; }
	}

	// ReSharper disable InconsistentNaming
	public enum StaffLanguages
	{
		JAPANESE,
		ENGLISH,
		KOREAN,
		ITALIAN,
		SPANISH,
		PORTUGUESE,
		FRENCH,
		GERMAN,
		HEBREW,
		HUNGARIAN
	}
	// ReSharper restore InconsistentNaming
}