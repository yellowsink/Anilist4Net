using Anilist4Net.Connections;

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

		public MediaNodePlaceholder Media { get; set; }

		/// <summary>
		///     The ID of the media the recommendation is from
		/// </summary>
		public int MediaId => Media.Id;

		public MediaNodePlaceholder MediaRecommendation { get; set; }

		/// <summary>
		///     The ID of the media being recommended
		/// </summary>
		public int MediaRecommendationId => MediaRecommendation.Id;

		public UserPlaceholder User { get; set; }

		/// <summary>
		///     The ID of the user making the recommendation
		/// </summary>
		public int UserId => User.Id;
	}

	public class RecommendationResponse
	{
		public Recommendation Recommendation { get; set; }
	}
}