namespace Anilist4Net
{
	public class Recommendation
	{
		/// <summary>
		///     The ID of the Recommendation
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Users rating of the recommendation
		/// </summary>
		public int Rating { get; set; }

		private MediaNodePlaceholder _media { get; set; }

		/// <summary>
		///     The ID of the media the recommendation is from
		/// </summary>
		public int MediaId => _media.Id;

		private MediaNodePlaceholder _mediaRecommendation { get; set; }

		/// <summary>
		///     The ID of the media being recommended
		/// </summary>
		public int MediaRecommendationId => _mediaRecommendation.Id;

		/// <summary>
		///     The ID of the user making the recommendation
		/// </summary>
		public int UserId { get; set; }
	}

	internal class RecommendationResponse
	{
		public Recommendation Recommendation { get; set; }
	}
}