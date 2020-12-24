namespace Anilist4Net
{
	public class Media
	{
		private MediaTitle          _mediaTitle     { get; set; }
		public  int                 Id              { get; set; }
		public  int?                IdMal           { get; set; }
		public  MediaTypes          Type            { get; set; }
		public  MediaFormats        Format          { get; set; }
		public  MediaStatuses       Status          { get; set; }
		public  string              DescriptionMd   { get; set; }
		public  string              DescriptionHtml { get; set; }
		private FuzzyDate           _startDate      { get; set; }
		private FuzzyDate           _endDate        { get; set; }
		public  Seasons             Season          { get; set; }
		public  int?                SeasonYear      { get; set; }
		public  int?                SeasonInt       { get; set; }
		public  int?                Episodes        { get; set; }
		public  int?                Duration        { get; set; }
		public  int?                Chapters        { get; set; }
		public  int?                Volumes         { get; set; }
		public  string              CountryOfOrigin { get; set; }
		public  bool                IsLicensed      { get; set; }
		public  MediaSources        Source          { get; set; }
		public  string              Hashtag         { get; set; }
		public  MediaTrailer        Trailer         { get; set; }
		public  int                 UpdatedAt       { get; set; }
		private MediaCoverImage     _coverImage     { get; set; }
		public  string              BannerImage     { get; set; }
		public  string[]            Genres          { get; set; }
		public  string[]            Synonyms        { get; set; }
		public  int                 AverageScore    { get; set; }
		public  int                 MeanScore       { get; set; }
		public  int                 Popularity      { get; set; }
		public  bool                IsLocked        { get; set; }
		public  int                 Trending        { get; set; }
		public  int                 Favourites      { get; set; }
		public  MediaTag[]          Tags            { get; set; }
		private MediaConnection     _relations      { get; set; }
		private CharacterConnection _characters     { get; set; }
		private StaffConnection     _staff          { get; set; }
		private StudioConnection    _studios        { get; set; }

		public bool IsAdult { get; set; }

		// public AiringSchedule NextAiringEpisode{ get; set; }
		private AiringScheduleConnection _airingSchedule       { get; set; }
		private MediaTrendConnection     _trends               { get; set; }
		public  MediaExternalLink[]      ExternalLinks         { get; set; }
		public  MediaStreamingEpisode[]  StreamingEpisodes     { get; set; }
		public  MediaRanking[]           Rankings              { get; set; }
		private ReviewConnection         _reviews              { get; set; }
		private ReccomendationConnection _reccomendations      { get; set; }
		public  MediaStats               Stats                 { get; set; }
		public  string                   SiteUrl               { get; set; }
		public  bool                     AutoCreateForumThread { get; set; }
		public  string                   ModNotes              { get; set; }
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

	internal class MediaConnection
	{
		public MediaEdge[] Edges { get; set; }
	}

	internal class MediaEdge
	{
		public MediaNodePlaceholder Node         { get; set; }
		public MediaRelationType    RelationType { get; set; }
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
		// public MediaTrend[] Nodes { get; set; }
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

	internal class ReccomendationConnection
	{
		public ReccomendationNode[] Nodes { get; set; }
	}

	internal class ReccomendationNode
	{
		public int                            Id                  { get; set; }
		public int                            Rating              { get; set; }
		public MediaNodePlaceholder           Media               { get; set; }
		public MediaReccomendationPlaceholder MediaReccomendation { get; set; }
		public UserPlaceholder                User                { get; set; }
	}

	internal class MediaReccomendationPlaceholder
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