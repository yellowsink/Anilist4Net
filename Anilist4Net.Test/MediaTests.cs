using System;
using System.Linq;
using System.Threading.Tasks;
using Anilist4Net.Enums;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class MediaTests
	{
		[Test]
		public async Task CowboyBebopByIdTest()
		{
			var client = new Client();
			var media  = await client.GetMediaById(1);
			AreEqual(1,                         media.Id);
			AreEqual(1,                         media.IdMal);
			AreEqual("Cowboy Bebop",            media.EnglishTitle);
			AreEqual("Cowboy Bebop",            media.RomajiTitle);
			AreEqual("カウボーイビバップ",               media.NativeTitle);
			AreEqual(MediaTypes.ANIME,          media.Type);
			AreEqual(MediaFormats.TV,           media.Format);
			AreEqual(MediaStatuses.FINISHED,    media.Status);
			AreEqual(new DateTime(1998, 4, 3),  media.AiringStartDate);
			AreEqual(new DateTime(1999, 4, 24), media.AiringEndDate);
			AreEqual(Seasons.SPRING,            media.Season);
			AreEqual(1998,                      media.SeasonYear);
			AreEqual(982,                       media.SeasonInt);
			AreEqual(26,                        media.Episodes);
			AreEqual(24,                        media.Duration);
			IsNull(media.Chapters);
			IsNull(media.Volumes);
			AreEqual("JP", media.CountryOfOrigin);
			IsTrue(media.IsLicensed);
			AreEqual(MediaSources.ORIGINAL, media.Source);
			IsNull(media.Hashtag);
			IsNull(media.Trailer);
			AreEqual("https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx1-CXtrrkMpJ8Zq.png",
			         media.CoverImageExtraLarge);
			AreEqual("https://s4.anilist.co/file/anilistcdn/media/anime/cover/medium/bx1-CXtrrkMpJ8Zq.png",
			         media.CoverImageLarge);
			AreEqual("https://s4.anilist.co/file/anilistcdn/media/anime/cover/small/bx1-CXtrrkMpJ8Zq.png",
			         media.CoverImageMedium);
			AreEqual("#f1785d", media.CoverImageColour);
			AreEqual("https://s4.anilist.co/file/anilistcdn/media/anime/banner/1-T3PJUjFJyRwg.jpg",
			         media.BannerImage);
			AreEqual(new[] {"Action", "Adventure", "Drama", "Sci-Fi"}, media.Genres);
			IsEmpty(media.Synonyms);
			IsFalse(media.IsLocked);
			IsNotEmpty(media.Tags.Where(t => t.Id == 63 && t.Name == "Space" && t.IsGeneralSpoiler == false &&
			                                 t.IsMediaSpoiler == false).ToArray());
			IsNotEmpty(media.MediaRelations
			                .Where(r => r.MediaId == 5 && r.RelationType == MediaRelationType.SIDE_STORY)
			                .ToArray());
			Contains(1, media.MediaCharacters);
			IsNotEmpty(media.Staff.Edges.Select(e => e.Node.Id == 95269 && e.Role == "ADR Director"));
			IsNotEmpty(media.Studios.Edges.Select(e => e.Node.Id == 14 && e.IsMain));
			IsFalse(media.IsAdult);
			IsNull(media.NextAiringEpisode);
			IsEmpty(media.MediaAiringSchedule);
			IsNotEmpty(media.ExternalLinks.Select(l => l.Id == 823
			                                        && l.Url == "http://www.hulu.com/cowboy-bebop"
			                                        && l.Site == "Hulu"));
			IsNotEmpty(media.StreamingEpisodes.Select(e => e.Title == "Episode 1 - Asteroid Blues"
			                                            && e.Thumbnail ==
			                                               "https://img1.ak.crunchyroll.com/i/spire2-tmb/e3a45e86c597fe16f02d29efcadedcd81473268732_full.jpg"
			                                            && e.Url ==
			                                               "http://www.crunchyroll.com/cowboy-bebop/episode-1-asteroid-blues-719653"
			                                            && e.Site == "Crunchyroll"));
			// can't test recommendations, etc as they dynamically change, as they are based on (or literally *are*) user activity
			AreEqual("https://anilist.co/anime/1", media.SiteUrl);
			IsFalse(media.AutoCreateForumThread);
			IsFalse(media.IsRecommendationBlocked);
			IsNull(media.ModNotes);
		}
	}
}