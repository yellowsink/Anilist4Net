using System;
using System.Linq;
using System.Threading.Tasks;
using Anilist4Net.Enums;
using NUnit.Framework;

namespace Anilist4Net.Test
{
	public class MediaTests
	{
		[Test]
		public async Task CowboyBebopByIdTest()
		{
			var client = new Client();
			var media  = await client.GetMediaById(1);
			Assert.AreEqual(1,                         media.Id);
			Assert.AreEqual(1,                         media.IdMal);
			Assert.AreEqual("Cowboy Bebop",            media.EnglishTitle);
			Assert.AreEqual("Cowboy Bebop",            media.RomajiTitle);
			Assert.AreEqual("カウボーイビバップ",               media.NativeTitle);
			Assert.AreEqual(MediaTypes.ANIME,          media.Type);
			Assert.AreEqual(MediaFormats.TV,           media.Format);
			Assert.AreEqual(MediaStatuses.FINISHED,    media.Status);
			Assert.AreEqual(new DateTime(1998, 4, 3),  media.AiringStartDate);
			Assert.AreEqual(new DateTime(1999, 4, 24), media.AiringEndDate);
			Assert.AreEqual(Seasons.SPRING,            media.Season);
			Assert.AreEqual(1998,                      media.SeasonYear);
			Assert.AreEqual(982,                       media.SeasonInt);
			Assert.AreEqual(26,                        media.Episodes);
			Assert.AreEqual(24,                        media.Duration);
			Assert.IsNull(media.Chapters);
			Assert.IsNull(media.Volumes);
			Assert.AreEqual("JP", media.CountryOfOrigin);
			Assert.IsTrue(media.IsLicensed);
			Assert.AreEqual(MediaSources.ORIGINAL, media.Source);
			Assert.IsNull(media.Hashtag);
			Assert.IsNull(media.Trailer);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx1-CXtrrkMpJ8Zq.png",
			                media.CoverImageExtraLarge);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/media/anime/cover/medium/bx1-CXtrrkMpJ8Zq.png",
			                media.CoverImageLarge);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/media/anime/cover/small/bx1-CXtrrkMpJ8Zq.png",
			                media.CoverImageMedium);
			Assert.AreEqual("#f1785d", media.CoverImageColour);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/media/anime/banner/1-T3PJUjFJyRwg.jpg",
			                media.BannerImage);
			Assert.AreEqual(new[] {"Action", "Adventure", "Drama", "Sci-Fi"}, media.Genres);
			Assert.IsEmpty(media.Synonyms);
			Assert.IsFalse(media.IsLocked);
			Assert.IsNotEmpty(media.Tags.Where(t => t.Id == 63 && t.Name == "Space" && t.IsGeneralSpoiler == false &&
			                                        t.IsMediaSpoiler == false).ToArray());
			Assert.IsNotEmpty(media.MediaRelations
			                       .Where(r => r.MediaId == 5 && r.RelationType == MediaRelationType.SIDE_STORY)
			                       .ToArray());
			Assert.Contains(1, media.MediaCharacters);
			// TODO: test entire query response (except stuff like score that changes)
		}
	}
}