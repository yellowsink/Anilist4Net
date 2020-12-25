using System.Threading.Tasks;
using NUnit.Framework;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class UserTests
	{
		// Users are really hard to test as they change over time, so if you want to run
		// these tests make a request at https://anilist.co/graphiq to get up to date data first.

		// Use a parameter to use username instead of ID. I did this because I didn't want
		// to write my Assert.___() functions twice.
		[TestCase(true, Description = "Username Test")]
		[TestCase(Description       = "ID Test")]
		public async Task YellowsinkTest(bool username = false) // Me!!!
		{
			var client = new Client();
			var user   = !username ? await client.GetUserById(668126) : await client.GetUserByName("Yellowsink");

			// The info in these test are likely to change. Make sure to fetch up-to-date info first! Remove the comment to enable the tests
			/*
			Assert.AreEqual(668126,       user.Id);
			Assert.AreEqual("Yellowsink", user.Name);

			// */
		}
	}
}