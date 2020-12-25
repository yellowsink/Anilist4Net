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
		public async Task YellowsinkTest(bool useUsername = false) // Me!!!
		{
			var client = new Client();
			var user   = !useUsername ? await client.GetUserById(668126) : await client.GetUserByName("Yellowsink");

			// The info in these test are likely to change. Make sure to fetch up-to-date info first! Remove the comment to enable the tests
			/*
			Assert.AreEqual(668126,       user.Id);
			Assert.AreEqual("Yellowsink", user.Name);
			Assert.IsNull(user.AboutMd);
			Assert.IsNull(user.AboutHtml);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/user/avatar/large/b668126-VYzGU0Un5il8.jpg",
			                user.LargeAvatar);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/user/avatar/medium/b668126-VYzGU0Un5il8.jpg",
			                user.MediumAvatar);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/user/banner/b668126-U4RJk8NvjQOs.jpg",
			                user.BannerImage);
			Assert.AreEqual(UserTitleLanguages.ENGLISH, user.TitleLanguage);
			Assert.IsTrue(user.DisplayAdultContent);
			Assert.IsTrue(user.AiringNotifications);
			Assert.AreEqual(UserProfileColours.green, user.ProfileColour);
			Assert.AreEqual(ScoreFormats.POINT_100,   user.ScoreFormat);
			Assert.AreEqual("score",                  user.RowOrder);
			Assert.IsFalse(user.AnimeList.SplitCompletedSectionByFormat);
			Assert.IsFalse(user.AnimeList.AdvancedScoringEnabled);
			Assert.IsFalse(user.MangaList.SplitCompletedSectionByFormat);
			Assert.IsFalse(user.MangaList.AdvancedScoringEnabled);
			Assert.AreEqual("https://anilist.co/user/668126", user.SiteUrl);
			Assert.AreEqual(0,                                user.DonatorTier);
			Assert.AreEqual("Donator",                        user.DonatorBadge);
			Assert.IsNull(user.ModeratorStatus);

			if (false)
				// */
			Assert.Inconclusive("The test is commented out");
		}
	}
}