using System;
using System.Linq;
using Anilist4Net.Connections;
using Anilist4Net.Enums;

namespace Anilist4Net
{
	public class Media
	{
		public MediaTitle Title { get; set; }

		/// <summary>
		///     The title in Romaji
		/// </summary>
		public string RomajiTitle => Title.Romaji;

		/// <summary>
		///     The title in English
		/// </summary>
		public string EnglishTitle => Title.English;

		/// <summary>
		///     The title in the native language (usually Japanese)
		/// </summary>
		public string NativeTitle => Title.Native;

		/// <summary>
		///     The ID of the media
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     The MyAnimeList ID of the media
		/// </summary>
		public int? IdMal { get; set; }

		/// <summary>
		///     The type of the media
		/// </summary>
		public MediaTypes Type { get; set; }

		/// <summary>
		///     The format of the media
		/// </summary>
		public MediaFormats Format { get; set; }

		/// <summary>
		///     The status of the media (airing, finished, etc)
		/// </summary>
		public MediaStatuses Status { get; set; }

		/// <summary>
		///     The description in Markdown
		/// </summary>
		public string DescriptionMd { get; set; }

		/// <summary>
		///     The description in HTML
		/// </summary>
		public string DescriptionHtml { get; set; }

		public FuzzyDate StartDate { get; set; }
		public FuzzyDate EndDate   { get; set; }

		/// <summary>
		///     The season the media is from
		/// </summary>
		public Seasons? Season { get; set; }

		/// <summary>
		///     The year the media is from
		/// </summary>
		public int? SeasonYear { get; set; }

		/// <summary>
		///     The year and season in the format YYS
		/// </summary>
		public int? SeasonInt { get; set; }

		/// <summary>
		///     How many episodes there are
		/// </summary>
		public int? Episodes { get; set; }

		/// <summary>
		///     Roughly how long it takes to watch
		/// </summary>
		public int? Duration { get; set; }

		/// <summary>
		///     How many chapters it has
		/// </summary>
		public int? Chapters { get; set; }

		/// <summary>
		///     How many volumes it has
		/// </summary>
		public int? Volumes { get; set; }

		/// <summary>
		///     The country of origin as an ISO country code
		/// </summary>
		public string CountryOfOrigin { get; set; }

		/// <summary>
		///     Whether the media is licensed (i.e. by Funimation or Viz)
		/// </summary>
		public bool IsLicensed { get; set; }

		/// <summary>
		///     The source of the media (is it original? is it a manga adaptation?)
		/// </summary>
		public MediaSources Source { get; set; }

		/// <summary>
		///     Official Twitter hashtags
		/// </summary>
		public string Hashtag { get; set; }

		/// <summary>
		///     The trailer
		/// </summary>
		public MediaTrailer Trailer { get; set; }

		/// <summary>
		///     When the media was last updated
		/// </summary>
		public int UpdatedAt { get; set; }

		public MediaCoverImage CoverImage { get; set; }

		/// <summary>
		///     The cover image URL (Extra Large)
		/// </summary>
		public string CoverImageExtraLarge => CoverImage.ExtraLarge;

		/// <summary>
		///     The cover image URL (Large)
		/// </summary>
		public string CoverImageLarge => CoverImage.Large;

		/// <summary>
		///     The cover image URL (Medium)
		/// </summary>
		public string CoverImageMedium => CoverImage.Medium;

		/// <summary>
		///     The cover image colour URL
		/// </summary>
		public string CoverImageColour => CoverImage.Color;

		/// <summary>
		///     The banner image URL
		/// </summary>
		public string BannerImage { get; set; }

		/// <summary>
		///     The genres of the media
		/// </summary>
		public string[] Genres { get; set; }

		/// <summary>
		///     Synonyms of the media
		/// </summary>
		public string[] Synonyms { get; set; }

		/// <summary>
		///     The weighted average score
		/// </summary>
		public int AverageScore { get; set; }

		/// <summary>
		///     The true mean average score
		/// </summary>
		public int MeanScore { get; set; }

		/// <summary>
		///     How popular the media is
		/// </summary>
		public int Popularity { get; set; }

		/// <summary>
		///     Is this media blocked from being added to lists etc? (usually items pending deletion)
		/// </summary>
		public bool IsLocked { get; set; }

		/// <summary>
		///     Trending rank
		/// </summary>
		public int Trending { get; set; }

		/// <summary>
		///     How many people have favourited the media
		/// </summary>
		public int Favourites { get; set; }

		/// <summary>
		///     The tags of the media
		/// </summary>
		public MediaTag[] Tags { get; set; }

		public MediaEdgeConnection Relations  { get; set; }
		public CharacterConnection Characters { get; set; }
		public StaffConnection     Staff      { get; set; }
		public StudioConnection    Studios    { get; set; }

		/// <summary>
		///     Is this media adult only? (18+ content)
		/// </summary>
		public bool IsAdult { get; set; }

		/// <summary>
		///     If currently airing, the schedule for the next airing episode
		/// </summary>
		public AiringSchedule NextAiringEpisode { get; set; }

		public AiringScheduleConnection AiringSchedule { get; set; }
		public MediaTrendConnection     Trends         { get; set; }

		/// <summary>
		///     External links for this media
		/// </summary>
		public MediaExternalLink[] ExternalLinks { get; set; }

		/// <summary>
		///     Streaming episodes on sites like Crunchyroll, Hidive, etc
		/// </summary>
		public MediaStreamingEpisode[] StreamingEpisodes { get; set; }

		/// <summary>
		///     The rankings of the media
		/// </summary>
		public MediaRanking[] Rankings { get; set; }

		public ReviewConnection         Reviews         { get; set; }
		public RecommendationConnection Recommendations { get; set; }

		/// <summary>
		///     The media stats (score and status distribution)
		/// </summary>
		public MediaStats Stats { get; set; }

		/// <summary>
		///     The Anilist site URL
		/// </summary>
		public string SiteUrl { get; set; }

		/// <summary>
		///     Whether new episodes of this media auto create forum threads
		/// </summary>
		public bool AutoCreateForumThread { get; set; }

		/// <summary>
		///     Is the media blocked from being recommended from / to?
		/// </summary>
		public bool IsRecommendationBlocked { get; set; }

		/// <summary>
		///     Notes for the mods
		/// </summary>
		public string ModNotes { get; set; }

		/// <summary>
		///     The start date of the media
		/// </summary>
		public DateTime AiringStartDate => new DateTime(StartDate.Year, StartDate.Month, StartDate.Day);

		/// <summary>
		///     The end date of the media
		/// </summary>
		public DateTime AiringEndDate => new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

		/// <summary>
		///     Related media
		/// </summary>
		public MediaRelation[] MediaRelations => Relations.Edges
		                                                  .Select(e => new MediaRelation
		                                                   {
			                                                   MediaId = e.Node.Id, RelationType = e.RelationType
		                                                   }).ToArray();

		/// <summary>
		///     The airing schedule IDs for all the media's episodes
		/// </summary>
		public int[] MediaAiringSchedule => AiringSchedule.Nodes.Select(n => n.Id).ToArray();

		/// <summary>
		///     The media's trending chart history
		/// </summary>
		public MediaTrend[] MediaTrends => Trends.Nodes.Select(n => new MediaTrend
		{
			MediaId    = n.MediaId, Date          = n.Date, Trending        = n.Trending, AverageScore = n.AverageScore,
			Popularity = n.Popularity, InProgress = n.InProgress, Releasing = n.Releasing, Episode     = n.Episode
		}).ToArray();

		/// <summary>
		///     The IDs of the media's reviews
		/// </summary>
		public int[] MediaReviews => Reviews.Nodes.Select(n => n.Id).ToArray();

		/// <summary>
		///     The IDs of the media's recommendations
		/// </summary>
		public int[] MediaRecommendations => Recommendations.Nodes.Select(n => n.Id).ToArray();

		/// <summary>
		///     The IDs of the media's characters
		/// </summary>
		public int[] MediaCharacters => Characters.Edges.Select(e => e.Node.Id).ToArray();
	}

	public class MediaResponse
	{
		public Media Media { get; set; }
	}

	public class MediaTitle
	{
		public string Romaji  { get; set; }
		public string English { get; set; }
		public string Native  { get; set; }
	}

	public class FuzzyDate
	{
		public int Year  { get; set; }
		public int Month { get; set; }
		public int Day   { get; set; }
	}

	public class MediaTrailer
	{
		public string Id        { get; set; }
		public string Site      { get; set; }
		public string Thumbnail { get; set; }
	}

	public class MediaCoverImage
	{
		public string ExtraLarge { get; set; }
		public string Large      { get; set; }
		public string Medium     { get; set; }
		public string Color      { get; set; }
	}

	public class MediaTag
	{
		public int    Id               { get; set; }
		public string Name             { get; set; }
		public string Description      { get; set; }
		public string Category         { get; set; }
		public int    Rank             { get; set; }
		public bool   IsGeneralSpoiler { get; set; }
		public bool   IsMediaSpoiler   { get; set; }
		public bool   IsAdult          { get; set; }
	}

	public class MediaRelation
	{
		public int               MediaId;
		public MediaRelationType RelationType;
		public Media             Media;

		public async void PopulateMedia()
		{
			Media = await new Client().GetMediaById(MediaId);
		}
	}

	public class MediaTrend
	{
		public int  MediaId      { get; set; }
		public int  Date         { get; set; }
		public int  Trending     { get; set; }
		public int? AverageScore { get; set; }
		public int? Popularity   { get; set; }
		public int? InProgress   { get; set; }
		public bool Releasing    { get; set; }
		public int? Episode      { get; set; }
	}

	public class MediaExternalLink
	{
		public int    Id   { get; set; }
		public string Url  { get; set; }
		public string Site { get; set; }
	}

	public class MediaStreamingEpisode
	{
		public string Title     { get; set; }
		public string Thumbnail { get; set; }
		public string Url       { get; set; }
		public string Site      { get; set; }
	}

	public class MediaRanking
	{
		public int            Id      { get; set; }
		public int            Rank    { get; set; }
		public MediaRankTypes Types   { get; set; }
		public MediaFormats   Format  { get; set; }
		public int?           Year    { get; set; }
		public Seasons?       Season  { get; set; }
		public bool           AllTime { get; set; }
		public string         Context { get; set; }
	}


	public class MediaStats
	{
		public ScoreDistribution[]  ScoreDistribution  { get; set; }
		public StatusDistribution[] StatusDistribution { get; set; }
	}

	public class ScoreDistribution
	{
		public int Score  { get; set; }
		public int Amount { get; set; }
	}

	public class StatusDistribution
	{
		public MediaListStatuses? Status { get; set; }
		public int                Amount { get; set; }
	}
}