using System.Threading.Tasks;
using NUnit.Framework;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class StudioTests
	{
		[Test]
		public async Task LercheById() // Studio Lerche because they made Assassination Classroom, my favourite anime :)
		{
			var client = new Client();
			var studio = await client.GetStudioById(456);

			Assert.AreEqual(456,      studio.Id);
			Assert.AreEqual("Lerche", studio.Name);
			Assert.IsTrue(studio.IsAnimationStudio);
			Assert.Contains(10012, studio.MediaIds); // just the first 4 that appeared on the query
			Assert.Contains(10213, studio.MediaIds);
			Assert.Contains(12187, studio.MediaIds);
			Assert.Contains(12255, studio.MediaIds);
			Assert.AreEqual("https://anilist.co/studio/456", studio.SiteUrl);
		}
	}
}