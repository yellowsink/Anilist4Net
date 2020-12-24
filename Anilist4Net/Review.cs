using Anilist4Net.Enums;

namespace Anilist4Net
{
	public class Review
	{
		/// <summary>
		///     The ID of the review
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     The ID of the user who made the review
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		///     The ID of the media the review is of
		/// </summary>
		public int MediaId { get; set; }

		/// <summary>
		///     What type the media is
		/// </summary>
		public MediaTypes MediaType { get; set; }

		/// <summary>
		///     The summary of the review
		/// </summary>
		public string Summary { get; set; }

		/// <summary>
		///     The review in markdown
		/// </summary>
		public string BodyMd { get; set; }

		/// <summary>
		///     The review in HTML
		/// </summary>
		public string BodyHtml { get; set; }

		/// <summary>
		///     The rating of the review
		/// </summary>
		public int Rating { get; set; }

		/// <summary>
		///     How many people rated it
		/// </summary>
		public int RatingAmount { get; set; }

		/// <summary>
		///     The score given to the media by the review
		/// </summary>
		public int Score { get; set; }

		/// <summary>
		///     Is the review private?
		/// </summary>
		public bool Private { get; set; }

		/// <summary>
		///     The Anilist URL
		/// </summary>
		public string SiteUrl { get; set; }

		/// <summary>
		///     When the review was created
		/// </summary>
		public int CreatedAt { get; set; }

		/// <summary>
		///     When the review was last updated
		/// </summary>
		public int UpdatedAt { get; set; }
	}

	internal class ReviewResponse
	{
		public Review Review { get; set; }
	}
}