using System.Linq;

namespace Anilist4Net
{
	public class Studio
	{
		public  int                 Id                { get; set; }
		public  string              Name              { get; set; }
		public  bool                IsAnimationStudio { get; set; }
		private MediaNodeConnection Media             { get; set; }
		public  int[]               MediaIds          => Media.Nodes.Select(n => n.Id).ToArray();
		public  string              SiteUrl           { get; set; }
		public  int                 Favourites        { get; set; }
	}

	internal class StudioResponse
	{
		public Studio Studio { get; set; }
	}
}