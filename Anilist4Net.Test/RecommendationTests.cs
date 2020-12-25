using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class RecommendationTests
	{
		[Test]
		public async Task
			RecommendationById() // some random recommendation, probably for Cowboy Bebop (again, convenience)
		{
			var client         = new Client();
			var recommendation = await client.GetRecommendationById(250);

			AreEqual(250,   recommendation.Id);
			AreEqual(1,     recommendation.MediaId);
			AreEqual(6,     recommendation.MediaRecommendationId);
			AreEqual(40330, recommendation.UserId);
		}
	}
}