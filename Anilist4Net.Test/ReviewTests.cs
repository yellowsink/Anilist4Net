using System.Threading.Tasks;
using Anilist4Net.Enums;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class ReviewTests
	{
		[Test]
		public async Task ReviewById() // a review of Cowboy Bebop. chosen because it was convenient.
		{
			var client = new Client();
			var review = await client.GetReviewById(760);

			AreEqual(760,                                       review.Id);
			AreEqual(22003,                                     review.UserId);
			AreEqual(1,                                         review.MediaId);
			AreEqual(MediaTypes.ANIME,                          review.MediaType);
			AreEqual("Cowboy Bebop is a smash-hit jam session", review.Summary);
			AreEqual(100,                                       review.Score);
			IsFalse(review.Private);
			AreEqual("https://anilist.co/review/760", review.SiteUrl);
			AreEqual(1437942041,                      review.CreatedAt);
		}
	}
}