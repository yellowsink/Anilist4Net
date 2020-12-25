using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class StudioTests
	{
		[TestCase(TestName = "Lerche By ID")]
		[TestCase(true, TestName = "Lerche by search")]
		public async Task
			LercheTest(bool search = false) // Studio Lerche because they made Assassination Classroom, my favourite anime :)
		{
			var client = new Client();
			var studio = search ? await client.GetStudioBySearch("lerche") : await client.GetStudioById(456);

			AreEqual(456,      studio.Id);
			AreEqual("Lerche", studio.Name);
			IsTrue(studio.IsAnimationStudio);
			Contains(10012, studio.MediaIds); // just the first 4 that appeared on the query
			Contains(10213, studio.MediaIds);
			Contains(12187, studio.MediaIds);
			Contains(12255, studio.MediaIds);
			AreEqual("https://anilist.co/studio/456", studio.SiteUrl);
		}
	}
}