#nullable enable
using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Anilist4Net.Enums;
using System.Net.Http;
using System;
using Anilist4Net.Connections;
using System.Collections.Generic;

namespace Anilist4Net
{
	/// <summary>
	/// Client used to handle retrieval of data from AniList Graph API
	/// </summary>
	public partial class Client
	{
		/// <summary>
		/// GraphQLHttpClient instance
		/// </summary>
		private readonly GraphQLHttpClient _graphQlClient;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Client() : this(new HttpClient()) { }

		/// <summary>
		/// Construct with a <see cref="HttpClient"/> instance
		/// </summary>
		/// <param name="httpClient">Client instance to use for queries</param>
		public Client(HttpClient httpClient)
        {
            RateLimitTimeout = TimeSpan.FromMinutes(1);
            RateLimitTolerance = 0;
			var options = new GraphQLHttpClientOptions
			{
				EndPoint = new Uri("https://graphql.anilist.co")
			};
			_graphQlClient = new GraphQLHttpClient(options, new SystemTextJsonSerializer(), httpClient);
		}

		/// <summary>
		/// Indicate if methods that fetch multiple pages should wait when they hit the rate limit
		/// </summary>
		public bool ObeyRateLimit { get; set; }

		/// <summary>
		/// The amount of time to wait if the rate limit was hit
		/// </summary>
		public TimeSpan RateLimitTimeout { get; set; }

		/// <summary>
		/// If the remaining rate limit entries are below this value retrieval of multiple entries will wait for rate limit recovery
		/// </summary>
		public int RateLimitTolerance { get; set; }

        #region Query Invocations

		/// <summary>
		/// Retrieve a <see cref="User"/> by their username
		/// </summary>
		/// <param name="username">Username of the user to retrieve</param>
		/// <returns>User data</returns>
        public async Task<User> GetUserByName(string username)
        {
            return (await GetUserByNameWithRateLimit(username)).user;
        }

		/// <summary>
		/// Retrieve a <see cref="User"/> by their AniList Id
		/// </summary>
		/// <param name="id">user's AniList Id</param>
		/// <returns>User data</returns>
		public async Task<User> GetUserById(int id)
		{
			return (await GetUserByIdWithRateLimit(id)).user;
		}

		/// <summary>
		/// Retrieve a <see cref="Media"/> entry by its AniList Id
		/// </summary>
		/// <param name="id">AniList media Id</param>
		/// <returns>Media instance if it exists, otherwise null</returns>
		public async Task<Media?> GetMediaById(int id)
        {
            return (await GetMediaByIdWithRateLimit(id)).media;
        }

		/// <summary>
		/// Retrieve a <see cref="Media"/> entry by its MyAnimeList Id.
		/// <remarks>
		///		If there is both a matching anime and manga entry for the provided MAL Id it is uncertain which entry will be returned.
		///		To better control the response rather use the <see cref="GetMediaByMalId(int, MediaTypes)"/> method.
		/// </remarks>
		/// </summary>
		/// <param name="id">MAL Id to retrieve entry for</param>
		/// <returns>Media instance if one could be found, otherwise null</returns>
		public async Task<Media?> GetMediaByMalId(int id)
        {
            return (await GetMediaByMalIdWithRateLimit(id)).media;
        }

		/// <summary>
		/// Retrieve a <see cref="Media"/> entry by its MyAnimeList Id and its AniList type
		/// </summary>
		/// <param name="id">MAL Id to retrieve entry for</param>
		/// <param name="mediaType">The type of media to retrieve</param>
		/// <returns>Media instance if one could be found, otherwise null</returns>
		public async Task<Media?> GetMediaByMalId(int id, MediaTypes mediaType)
        {
            return (await GetMediaByMalIdWithRateLimit(id, mediaType)).media;
		}

		/// <summary>
		/// Search for a <see cref="Media"/> entry by its title
		/// </summary>
		/// <param name="search">Search query</param>
		/// <returns>Media instance if one could be found, otherwise null</returns>
		public async Task<Media?> GetMediaBySearch(string search)
		{
			return (await GetMediaBySearchWithRateLimit(search)).media;
		}

		/// <summary>
		/// Search for a <see cref="Media"/> entry by its title
		/// </summary>
		/// <param name="search">Search query</param>
		/// <param name="type">The type of media to search for</param>
		/// <returns>Media instance if one could be found, otherwise null</returns>
		public async Task<Media?> GetMediaBySearch(string search, MediaTypes type)
		{
			return (await GetMediaBySearchWithRateLimit(search, type)).media;
		}

		/// <summary>
		/// Search for <see cref="Media"/> entries by its title
		/// </summary>
		/// <param name="search">Search query</param>
		/// <param name="page">The page to search on</param>
		/// <param name="perPage">The number of entries per page</param>
		/// <returns>A paginated list of Media instances if found</returns>
		public async Task<Page> GetMediaBySearch(string search, int page, int perPage)
        {
            return (await GetMediaBySearchWithRateLimit(search, page, perPage)).page;
        }

		/// <summary>
		/// Search for <see cref="Media"/> entries by its title
		/// </summary>
		/// <param name="search">Search query</param>
		/// <param name="type">The type of media to search for</param>
		/// <param name="page">The page to search on</param>
		/// <param name="perPage">The number of entries per page</param>
		/// <returns>A paginated list of Media instances if found</returns>
		public async Task<Page?> GetMediaBySearch(string search, MediaTypes type, int page, int perPage)
		{
			return (await GetMediaBySearchWithRateLimit(search, type, page, perPage)).page;
		}

		/// <summary>
		/// Retrieve all characters for a <see cref="Media"/> entry.
		/// </summary>
		/// <param name="id">AniList media Id</param>
		/// <param name="page">Page to start the retrieval from</param>
		/// <returns>List of <see cref="CharacterEdge"/>s for the Media</returns>
		public async Task<List<CharacterEdge>> GetAllCharactersAsync(int id, int page)
		{
			return (await GetAllCharactersAsyncWithRateLimit(id, page)).characters;
		}

		/// <summary>
		/// Retrieve a <see cref="Review"/> by its AniList Id
		/// </summary>
		/// <param name="id">Id of the review to retrieve</param>
		/// <returns>Review for the requested Id</returns>
		public async Task<Review> GetReviewById(int id)
		{
			return (await GetReviewByIdWithRateLimit(id)).review;
		}

		/// <summary>
		/// Retrieve an <see cref="AiringSchedule"/> entry by its AniList Id
		/// </summary>
		/// <param name="id">AniList Id of the schedule entry to retrieve</param>
		/// <returns>AiringSchedule entry</returns>
		public async Task<AiringSchedule> GetAiringScheduleById(int id)
        {
            return (await GetAiringScheduleByIdWithRateLimit(id)).schedule;
        }

		/// <summary>
		/// Retrieve a <see cref="Recommendation"/> entry by its AniList Id
		/// </summary>
		/// <param name="id">AniList Id of the recommendation to retrieve</param>
		/// <returns>Recommendation entry</returns>
		public async Task<Recommendation> GetRecommendationById(int id)
		{
			return (await GetRecommendationByIdWithRateLimit(id)).recommendation;
		}

		/// <summary>
		/// Retrieve a <see cref="Character"/> entry by its AniList Id
		/// </summary>
		/// <param name="id">AniList Id of the character to retrieve</param>
		/// <returns>Character entry</returns>
		public async Task<Character> GetCharacterById(int id)
		{
			return (await GetCharacterByIdWithRateLimit(id)).character;
		}

		/// <summary>
		/// Retrieve all media entries for a <see cref="Character"/> entry.
		/// </summary>
		/// <param name="id">AniList character Id</param>
		/// <param name="page">Page to start the retrieval from</param>
		/// <returns>Tuple containing a list of <see cref="MediaEdge"/>s and <see cref="MediaNodePlaceholder"/>s for the <see cref="Character"/></returns>
		public async Task<(List<CharacterEdge> edges, List<MediaNodePlaceholder> nodes)> GetAllMediaEntriesForCharacter(int id, int page)
        {
            var (edges, nodes, _) = await GetAllMediaEntriesForCharacterWithRateLimit(id, page);

			return (edges, nodes);
        }

		/// <summary>
		/// Search for a <see cref="Character"/>
		/// </summary>
		/// <param name="search">Search query</param>
		/// <returns>Character matching the query</returns>
		public async Task<Character> GetCharacterBySearch(string search)
		{
			return (await GetCharacterBySearchWithRateLimit(search)).character;
		}

		/// <summary>
		/// Retrieve a <see cref="Studio"/> by its AniList Id
		/// </summary>
		/// <param name="id">Studio Id</param>
		/// <returns>Studio for the Id</returns>
		public async Task<Studio> GetStudioById(int id)
		{
			return (await GetStudioByIdWithRateLimit(id)).studio;
		}

		/// <summary>
		/// Search for a <see cref="Studio"/>
		/// </summary>
		/// <param name="search">Search query</param>
		/// <returns>Studio entry matching the search query</returns>
		public async Task<Studio> GetStudioBySearch(string search)
		{
			return (await GetStudioBySearchWithRateLimit(search)).studio;
		}

		/// <summary>
		/// Retrieve a <see cref="Staff"/> entry from AniList
		/// </summary>
		/// <param name="id">Id of the staff entry to retrieve</param>
		/// <returns>Staff entry for the Id</returns>
		public async Task<Staff> GetStaffById(int id)
        {
            return (await GetStaffByIdWithRateLimit(id)).staff;
        }

		/// <summary>
		/// Search for a <see cref="Staff"/> entry
		/// </summary>
		/// <param name="search">Search query</param>
		/// <returns>Staff entry matching the search</returns>
		public async Task<Staff> GetStaffBySearch(string search)
		{
			return (await GetStaffBySearchWithRateLimit(search)).staff;
		}

        /// <summary>
        /// Retrieve query that can be used to retrieve <see cref="Media"/> for a season
        /// </summary>
        /// <param name="page">The page to retrieve</param>
        /// <param name="season">The season to retrieve</param>
        /// <param name="seasonYear">The year the season is in</param>
        /// <returns>Media for the requested season</returns>
        public async Task<Page> GetMediaForSeason(int page, Seasons season, int seasonYear)
        {
            return (await GetMediaForSeasonWithRateLimit(page, season, seasonYear)).page;
        }

        /// <summary>
        /// Retrieve all character entries for a <see cref="Staff"/> entry.
        /// </summary>
        /// <param name="id">AniList staff Id</param>
        /// <param name="page">Page to start the retrieval from</param>
        /// <returns>List of <see cref="MediaEdge"/>s for the <see cref="Staff"/></returns>
        public async Task<List<CharacterEdge>> GetAllCharactersForStaffAsync(int id, int page)
        {
            return (await GetAllCharactersForStaffWithRateLimitAsync(id, page)).characters;
        }

		#endregion
	}
}