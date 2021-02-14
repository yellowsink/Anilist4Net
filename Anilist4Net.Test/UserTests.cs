// ReSharper disable RedundantUsingDirective

using Anilist4Net.Enums;
// ReSharper restore RedundantUsingDirective
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class UserTests
	{
		// Users are really hard to test as they change over time, so if you want to run
		// these tests make a request at https://anilist.co/graphiq to get up to date data first.

		// Use a parameter to use username instead of ID. I did this because I didn't want
		// to write my Assert.___() functions twice.
		[TestCase(true, TestName = "Username Test")]
		[TestCase(TestName       = "ID Test")]
		public async Task YellowsinkTest(bool useUsername = false) // Me!!!
		{
			var client = new Client();
			var user   = !useUsername ? await client.GetUserById(668126) : await client.GetUserByName("Yellowsink");

			// The info in these test are likely to change. Make sure to fetch up-to-date info first!

			/* // PUT A BLOCK COMMENT HERE TO DISABLE
			AreEqual(668126,       user.Id);
			AreEqual("Yellowsink", user.Name);
			IsNull(user.AboutMd);
			IsNull(user.AboutHtml);
			AreEqual("https://s4.anilist.co/file/anilistcdn/user/avatar/large/b668126-V8x7E1JOB5im.png",
			         user.LargeAvatar);
			AreEqual("https://s4.anilist.co/file/anilistcdn/user/avatar/medium/b668126-V8x7E1JOB5im.png",
			         user.MediumAvatar);
			AreEqual("https://s4.anilist.co/file/anilistcdn/user/banner/b668126-U4RJk8NvjQOs.jpg",
			         user.BannerImage);
			AreEqual(UserTitleLanguages.ENGLISH, user.TitleLanguage);
			IsTrue(user.DisplayAdultContent);
			IsTrue(user.AiringNotifications);
			AreEqual(UserProfileColours.green, user.ProfileColour);
			AreEqual(ScoreFormats.POINT_100,   user.ScoreFormat);
			AreEqual("score",                  user.RowOrder);
			IsFalse(user.AnimeList.SplitCompletedSectionByFormat);
			IsFalse(user.AnimeList.AdvancedScoringEnabled);
			IsFalse(user.MangaList.SplitCompletedSectionByFormat);
			IsFalse(user.MangaList.AdvancedScoringEnabled);
			AreEqual("https://anilist.co/user/668126", user.SiteUrl);
			AreEqual(0,                                user.DonatorTier);
			AreEqual("Donator",                        user.DonatorBadge);
			IsNull(user.ModeratorStatus);

			if (false)
				// */
				Inconclusive("The test is commented out");
		}
	}
}
