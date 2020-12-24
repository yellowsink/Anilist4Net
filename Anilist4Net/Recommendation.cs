namespace Anilist4Net
{
	public class Recommendation
	{
		public  int                  Id                    { get; set; }
		public  int                  Rating                { get; set; }
		private MediaNodePlaceholder _media                { get; set; }
		public  int                  MediaId               => _media.Id;
		private MediaNodePlaceholder _mediaRecommendation  { get; set; }
		public  int                  MediaRecommendationId => _mediaRecommendation.Id;
		public  int                  UserId                { get; set; }
	}

	internal class RecommendationResponse
	{
		public Recommendation Recommendation { get; set; }
	}
}