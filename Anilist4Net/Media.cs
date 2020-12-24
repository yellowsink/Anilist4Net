using System;
using System.Linq;

namespace Anilist4Net
{
	public class Media
	{
		private MediaTitle _mediaTitle { get; set; }

		/// <summary>
		///     The title in Romaji
		/// </summary>
		public string RomajiTitle => _mediaTitle.Romaji;

		/// <summary>
		///     The title in English
		/// </summary>
		public string EnglishTitle => _mediaTitle.English;

		/// <summary>
		///     The title in the native language (usually Japanese)
		/// </summary>
		public string NativeTitle => _mediaTitle.Native;

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

		private FuzzyDate _startDate { get; set; }
		private FuzzyDate _endDate   { get; set; }

		/// <summary>
		///     The season the media is from
		/// </summary>
		public Seasons Season { get; set; }

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

		private MediaCoverImage _coverImage { get; set; }

		/// <summary>
		///     The cover image URL (Extra Large)
		/// </summary>
		public string CoverImageExtraLarge => _coverImage.ExtraLarge;

		/// <summary>
		///     The cover image URL (Large)
		/// </summary>
		public string CoverImageLarge => _coverImage.Large;

		/// <summary>
		///     The cover image URL (Medium)
		/// </summary>
		public string CoverImageMedium => _coverImage.Medium;

		/// <summary>
		///     The cover image colour URL
		/// </summary>
		public string CoverImageColour => _coverImage.Color;

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

		private MediaEdgeConnection _relations  { get; set; }
		private CharacterConnection _characters { get; set; }
		private StaffConnection     _staff      { get; set; }
		private StudioConnection    _studios    { get; set; }

		/// <summary>
		///     Is this media adult only? (18+ content)
		/// </summary>
		public bool IsAdult { get; set; }

		/// <summary>
		///     If currently airing, the schedule for the next airing episode
		/// </summary>
		public AiringSchedule NextAiringEpisode { get; set; }

		private AiringScheduleConnection _airingSchedule { get; set; }
		private MediaTrendConnection     _trends         { get; set; }

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

		private ReviewConnection         _reviews         { get; set; }
		private RecommendationConnection _recommendations { get; set; }

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
		///     Notes for the mods
		/// </summary>
		public string ModNotes { get; set; }

		public DateTime StartDate => new DateTime(_startDate.Year, _startDate.Month, _startDate.Day);
		public DateTime EndDate   => new DateTime(_endDate.Year,   _endDate.Month,   _endDate.Day);

		public MediaRelation[] Relations => _relations.Edges
		                                              .Select(e => new MediaRelation
		                                               {
			                                               MediaId = e.Node.Id, RelationType = e.RelationType
		                                               }).ToArray();

		public int[] AiringSchedule => _airingSchedule.Nodes.Select(n => n.Id).ToArray();

		public MediaTrend[] Trends => _trends.Nodes.Select(n => new MediaTrend
		{
			MediaId    = n.MediaId, Date          = n.Date, Trending        = n.Trending, AverageScore = n.AverageScore,
			Popularity = n.Popularity, InProgress = n.InProgress, Releasing = n.Releasing, Episode     = n.Episode
		}).ToArray();

		public int[] Reviews => _reviews.Nodes.Select(n => n.Id).ToArray();

		public int[] Recommendations => _recommendations.Nodes.Select(n => n.Id).ToArray();
		public int[] Characters      => _characters.Edges.Select(e => e.Node.Id).ToArray();
	}

	internal class MediaResponse
	{
		public Media Media { get; set; }
	}

	internal class MediaTitle
	{
		public string Romaji  { get; set; }
		public string English { get; set; }
		public string Native  { get; set; }
	}

	internal class FuzzyDate
	{
		public int Year  { get; set; }
		public int Month { get; set; }
		public int Day   { get; set; }
	}

	public class MediaTrailer
	{
		public int    Id        { get; set; }
		public string Site      { get; set; }
		public string Thumbnail { get; set; }
	}

	internal class MediaCoverImage
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

	internal class MediaEdgeConnection
	{
		public MediaEdge[] Edges { get; set; }
	}

	internal class MediaEdge
	{
		public MediaNodePlaceholder Node         { get; set; }
		public MediaRelationType    RelationType { get; set; }
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

	internal class MediaNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class CharacterConnection
	{
		public CharacterEdge[] Edges { get; set; }
	}

	internal class CharacterEdge
	{
		public CharacterNodePlaceholder Node        { get; set; }
		public VoiceActorPlaceholder[]  VoiceActors { get; set; }
	}

	internal class CharacterNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class VoiceActorPlaceholder
	{
		public int Id { get; set; }
	}

	internal class StaffConnection
	{
		public StaffEdge[] Edges { get; set; }
	}

	internal class StaffEdge
	{
		public StaffNodePlaceholder Node { get; set; }
		public string               Role { get; set; }
	}

	internal class StaffNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class StudioConnection
	{
		public StudioEdge[] Edges { get; set; }
	}

	internal class StudioEdge
	{
		public StudioNodePlaceholder Node   { get; set; }
		public bool                  IsMain { get; set; }
	}

	internal class StudioNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class AiringScheduleConnection
	{
		public AiringScheduleNodePlaceholder[] Nodes { get; set; }
	}

	internal class AiringScheduleNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class MediaTrendConnection
	{
		public MediaTrend[] Nodes { get; set; }
	}

	public class MediaTrend
	{
		public int MediaId      { get; set; }
		public int Date         { get; set; }
		public int Trending     { get; set; }
		public int AverageScore { get; set; }
		public int Popularity   { get; set; }
		public int InProgress   { get; set; }
		public int Releasing    { get; set; }
		public int Episode      { get; set; }
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
		public int          Id      { get; set; }
		public int          Rank    { get; set; }
		public string       Type    { get; set; }
		public MediaFormats Format  { get; set; }
		public int          Year    { get; set; }
		public Seasons      Season  { get; set; }
		public bool         AllTime { get; set; }
		public string       Context { get; set; }
	}

	internal class ReviewConnection
	{
		public ReviewNodePlaceholder[] Nodes { get; set; }
	}

	internal class ReviewNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class RecommendationConnection
	{
		public RecommendationNode[] Nodes { get; set; }
	}

	internal class RecommendationNode
	{
		public int Id { get; set; }
	}

	internal class UserPlaceholder
	{
		public int Id { get; set; }
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
		public MediaStatuses Status { get; set; }
		public int           Amount { get; set; }
	}

	// ReSharper disable InconsistentNaming
	// ReSharper disable IdentifierTypo
	public enum MediaTypes
	{
		ANIME,
		MANGA
	}

	public enum MediaFormats
	{
		TV,
		TV_SHORT,
		MOVIE,
		SPECIAL,
		OVA,
		ONA,
		MUSIC,
		MANGA,
		NOVEL,
		ONE_SHOT
	}

	public enum MediaStatuses
	{
		FINISHED,
		RELEASING,
		NOT_YET_RELEASED,
		CANCELLED,
		HIATUS
	}

	public enum Seasons
	{
		WINTER,
		SPRING,
		SUMMER,
		AUGUST
	}

	public enum MediaSources
	{
		ORIGINAL,
		MANGA,
		LIGHT_NOVEL,
		VISUAL_NOVEL,
		VIDEO_GAME,
		OTHER,
		NOVEL,
		DOUJINSHI,
		ANIME
	}

	public enum MediaRelationType
	{
		ADAPTATION,
		PREQUEL,
		SEQUEL,
		PARENT,
		SIDE_STORY,
		CHARACTER,
		SUMMARY,
		ALTERNATIVE,
		SPIN_OFF,
		OTHER,
		SOURCE,
		COMPILATION,
		CONTAINS
	}
	// ReSharper restore IdentifierTypo
	// ReSharper restore InconsistentNaming
}