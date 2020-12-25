using System.Threading.Tasks;
using NUnit.Framework;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class AiringScheduleTests
	{
		[Test]
		public async Task
			HypnosisMicEp1ById() // First episode of HYPNOSISMIC because it was all I could think of that was airing.
		{
			var client         = new Client();
			var airingSchedule = await client.GetAiringScheduleById(293185);

			Assert.AreEqual(293185,     airingSchedule.Id);
			Assert.AreEqual(1601650800, airingSchedule.AiringAt);
			Assert.AreEqual(1,          airingSchedule.Episode);
			Assert.AreEqual(113652,     airingSchedule.MediaId);
		}
	}
}