#nullable enable
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Anilist4Net.Enums;
using System.Net.Http;
using System;
using System.Net;
using Anilist4Net.Connections;
using System.Collections.Generic;

namespace Anilist4Net
{
	/// <summary>
	/// Client used to handle retrieval of data from AniList Graph API
	/// </summary>
	public class Client
	{
		/// <summary>
		/// GraphQLHttpClient instance
		/// </summary>
		private readonly GraphQLHttpClient graphQlClient;

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
			var options = new GraphQLHttpClientOptions
			{
				EndPoint = new Uri("https://graphql.anilist.co")
			};
			graphQlClient = new GraphQLHttpClient(options, new SystemTextJsonSerializer(), httpClient);
		}

        #region Query Invocations

		/// <summary>
		/// Retrieve a <see cref="User"/> by their username
		/// </summary>
		/// <param name="username">Username of the user to retrieve</param>
		/// <returns>User data</returns>
        public async Task<User> GetUserByName(string username)
		{
			var query         = $"query ($username: String) {{ User (name: $username) {QueryBuilder.GetUserQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {username}};
			var response      = await graphQlClient.SendQueryAsync<UserResponse>(request);

			return response.Data.User;
		}

		/// <summary>
		/// Retrieve a <see cref="User"/> by their AniList Id
		/// </summary>
		/// <param name="id">user's AniList Id</param>
		/// <returns>User data</returns>
		public async Task<User> GetUserById(int id)
		{
			var query         = $"query ($id: Int) {{ User (id: $id) {QueryBuilder.GetUserQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var response      = await graphQlClient.SendQueryAsync<UserResponse>(request);

			return response.Data.User;
		}

		/// <summary>
		/// Retrieve a <see cref="Media"/> entry by its AniList Id
		/// </summary>
		/// <param name="id">AniList media Id</param>
		/// <returns>Media instance if it exists, otherwise null</returns>
		public async Task<Media?> GetMediaById(int id)
		{
			var query         = $"query ($id: Int) {{ Media (id: $id) {QueryBuilder.GetMediaQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			GraphQLResponse<MediaResponse> response = null!;
			try
			{
				response = await graphQlClient.SendQueryAsync<MediaResponse>(request);
				var characterCount = response.Data.Media.Characters.Edges.Length;
				if(characterCount >= 25)
                {
					var additionalCharacters = await GetAllCharactersAsync(id, 2);
					additionalCharacters.AddRange(response.Data.Media.Characters.Edges);
					response.Data.Media.Characters.Edges = additionalCharacters.ToArray();
                }
			}
			catch (GraphQLHttpRequestException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					return null; // media did not exist
			}

			return response.Data.Media;
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
			var query         = $"query ($id: Int) {{ Media (idMal: $id) {QueryBuilder.GetMediaQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			GraphQLResponse<MediaResponse> response = null!;
			try
			{
				response = await graphQlClient.SendQueryAsync<MediaResponse>(request);
				var characterCount = response.Data.Media.Characters.Edges.Length;
				if (characterCount >= 25)
				{
					// Swap to the AniList Id rather than using the Mal Id
					var additionalCharacters = await GetAllCharactersAsync(response.Data.Media.Id, 2);
					additionalCharacters.AddRange(response.Data.Media.Characters.Edges);
					response.Data.Media.Characters.Edges = additionalCharacters.ToArray();
				}
			}
			catch (GraphQLHttpRequestException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					return null; // media did not exist
			}

			return response.Data.Media;
		}

		/// <summary>
		/// Retrieve a <see cref="Media"/> entry by its MyAnimeList Id and its AniList type
		/// </summary>
		/// <param name="id">MAL Id to retrieve entry for</param>
		/// <param name="mediaType">The type of media to retrieve</param>
		/// <returns>Media instance if one could be found, otherwise null</returns>
		public async Task<Media?> GetMediaByMalId(int id, MediaTypes mediaType)
        {
            var query = $"query ($id: Int) {{ Media (idMal: $id type: {mediaType}) {QueryBuilder.GetMediaQuery()} }}";
            var request = new GraphQLRequest { Query = query, Variables = new { id } };
            GraphQLResponse<MediaResponse> response = null!;
            try
            {
                response = await graphQlClient.SendQueryAsync<MediaResponse>(request);
				var characterCount = response.Data.Media.Characters.Edges.Length;
				if (characterCount >= 25)
				{
					var additionalCharacters = await GetAllCharactersAsync(id, 2);
					additionalCharacters.AddRange(response.Data.Media.Characters.Edges);
					response.Data.Media.Characters.Edges = additionalCharacters.ToArray();
				}
			}
            catch (GraphQLHttpRequestException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                    return null; // media did not exist
            }

            return response.Data.Media;
		}

		/// <summary>
		/// Search for a <see cref="Media"/> entry by its title
		/// </summary>
		/// <param name="search">Search query</param>
		/// <returns>Media instance if one could be found, otherwise null</returns>
		public async Task<Media?> GetMediaBySearch(string search)
		{
			var query         = $"query ($search: String) {{ Media (search: $search) {QueryBuilder.GetMediaQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {search}};
			GraphQLResponse<MediaResponse> response = null!;
			try
			{
				response = await graphQlClient.SendQueryAsync<MediaResponse>(request);
				var characterCount = response.Data.Media.Characters.Edges.Length;
				if (characterCount >= 25)
				{
					var additionalCharacters = await GetAllCharactersAsync(response.Data.Media.Id, 2);
					additionalCharacters.AddRange(response.Data.Media.Characters.Edges);
					response.Data.Media.Characters.Edges = additionalCharacters.ToArray();
				}
			}
			catch (GraphQLHttpRequestException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					return null; // media did not exist
			}

			return response.Data.Media;
		}

		/// <summary>
		/// Search for a <see cref="Media"/> entry by its title
		/// </summary>
		/// <param name="search">Search query</param>
		/// <param name="type">The type of media to search for</param>
		/// <returns>Media instance if one could be found, otherwise null</returns>
		public async Task<Media?> GetMediaBySearch(string search, MediaTypes type)
		{
			var query         = $"query ($search: String $type: MediaType) {{ Media (search: $search type: $type) {QueryBuilder.GetMediaQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {search, type}};
			GraphQLResponse<MediaResponse> response = null!;
			try
			{
				response = await graphQlClient.SendQueryAsync<MediaResponse>(request);
				var characterCount = response.Data.Media.Characters.Edges.Length;
				if (characterCount >= 25)
				{
					var additionalCharacters = await GetAllCharactersAsync(response.Data.Media.Id, 2);
					additionalCharacters.AddRange(response.Data.Media.Characters.Edges);
					response.Data.Media.Characters.Edges = additionalCharacters.ToArray();
				}
			}
			catch (GraphQLHttpRequestException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
					return null; // media did not exist
			}

			return response.Data.Media;
		}

		/// <summary>
		/// Retrieve all characters for a <see cref="Media"/> entry.
		/// </summary>
		/// <param name="id">AniList media Id</param>
		/// <param name="page">Page to start the retrieval from</param>
		/// <returns>List of <see cref="CharacterEdge"/>s for the Media</returns>
		public async Task<List<CharacterEdge>> GetAllCharactersAsync(int id, int page)
		{
			var characterCount = 0;
			var characters = new List<CharacterEdge>();
			do
			{
				try
				{
					var query = $"query ($id: Int) {{ Media (id: $id) {QueryBuilder.GetCharactersForMediaQuery(page)} }}";
					var request = new GraphQLRequest { Query = query, Variables = new { id } };
					var response = await graphQlClient.SendQueryAsync<MediaResponse>(request);
					characterCount = response.Data.Media.Characters.Edges.Length;
					characters.AddRange(response.Data.Media.Characters.Edges);
					page++;
				}
				catch (Exception)
				{
					// What to do here? We don't really mind as we can just keep trying until the end
				}
			} while (characterCount >= 25);

			return characters;
		}

		/// <summary>
		/// Retrieve a <see cref="Review"/> by its AniList Id
		/// </summary>
		/// <param name="id">Id of the review to retrieve</param>
		/// <returns>Review for the requested Id</returns>
		public async Task<Review> GetReviewById(int id)
		{
			var query         = $"query ($id: Int) {{ Review (id: $id) {QueryBuilder.GetReviewQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var response      = await graphQlClient.SendQueryAsync<ReviewResponse>(request);

			return response.Data.Review;
		}

		/// <summary>
		/// Retrieve an <see cref="AiringSchedule"/> entry by its AniList Id
		/// </summary>
		/// <param name="id">AniList Id of the schedule entry to retrieve</param>
		/// <returns>AiringSchedule entry</returns>
		public async Task<AiringSchedule> GetAiringScheduleById(int id)
		{
			var query         = $"query ($id: Int) {{ AiringSchedule (id: $id) {QueryBuilder.GetAiringScheduleQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var response      = await graphQlClient.SendQueryAsync<AiringScheduleResponse>(request);

			return response.Data.AiringSchedule;
		}

		/// <summary>
		/// Retrieve a <see cref="Recommendation"/> entry by its AniList Id
		/// </summary>
		/// <param name="id">AniList Id of the recommendation to retrieve</param>
		/// <returns>Recommendation entry</returns>
		public async Task<Recommendation> GetRecommendationById(int id)
		{
			var query         = $"query ($id: Int) {{ Recommendation (id: $id) {QueryBuilder.GetRecommendationQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var response      = await graphQlClient.SendQueryAsync<RecommendationResponse>(request);

			return response.Data.Recommendation;
		}

		/// <summary>
		/// Retrieve a <see cref="Character"/> entry by its AniList Id
		/// </summary>
		/// <param name="id">AniList Id of the character to retrieve</param>
		/// <returns>Character entry</returns>
		public async Task<Character> GetCharacterById(int id)
		{
			var query         = $"query ($id: Int) {{ Character (id: $id) {QueryBuilder.GetCharacterQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var response      = await graphQlClient.SendQueryAsync<CharacterResponse>(request);

			var mediaCount = response.Data.Character.Media.Edges.Length;
			if (mediaCount >= 25)
			{
				var additionalMedia = await GetAllMediaEntriesForCharacter(id, 2);
				additionalMedia.edges.AddRange(response.Data.Character.Media.Edges);
				additionalMedia.nodes.AddRange(response.Data.Character.Media.Nodes);
				response.Data.Character.Media.Edges = additionalMedia.edges.ToArray();
				response.Data.Character.Media.Nodes = additionalMedia.nodes.ToArray();
			}

			return response.Data.Character;
		}

		/// <summary>
		/// Retrieve all media entries for a <see cref="Character"/> entry.
		/// </summary>
		/// <param name="id">AniList character Id</param>
		/// <param name="page">Page to start the retrieval from</param>
		/// <returns>Tuple containing a list of <see cref="MediaEdge"/>s and <see cref="MediaNodePlaceholder"/>s for the <see cref="Character"/></returns>
		public async Task<(List<CharacterEdge> edges, List<MediaNodePlaceholder> nodes)> GetAllMediaEntriesForCharacter(int id, int page)
		{
			var mediaCount = 0;
			var edges = new List<CharacterEdge>();
			var nodes = new List<MediaNodePlaceholder>();
			do
			{
				try
				{
					var query = $"query ($id: Int) {{ Character (id: $id) {QueryBuilder.GetCharacterMediaQuery(page)} }}";
					var request = new GraphQLRequest { Query = query, Variables = new { id } };
					var response = await graphQlClient.SendQueryAsync<CharacterResponse>(request);
					mediaCount = response.Data.Character.Media.Edges.Length;
					edges.AddRange(response.Data.Character.Media.Edges);
					nodes.AddRange(response.Data.Character.Media.Nodes);					
					
					page++;
				}
				catch (Exception)
				{
					// What to do here? We don't really mind as we can just keep trying until the end
				}
			} while (mediaCount >= 25);

			return (edges, nodes);
		}

		/// <summary>
		/// Search for a <see cref="Character"/>
		/// </summary>
		/// <param name="search">Search query</param>
		/// <returns>Character matching the query</returns>
		public async Task<Character> GetCharacterBySearch(string search)
		{
			var query         = $"query ($search: String) {{ Character (search: $search) {QueryBuilder.GetCharacterQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {search}};
			var response      = await graphQlClient.SendQueryAsync<CharacterResponse>(request);

			return response.Data.Character;
		}

		/// <summary>
		/// Retrieve a <see cref="Studio"/> by its AniList Id
		/// </summary>
		/// <param name="id">Studio Id</param>
		/// <returns>Studio for the Id</returns>
		public async Task<Studio> GetStudioById(int id)
		{
			var query         = $"query ($id: Int) {{ Studio (id: $id) {QueryBuilder.GetStudioQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var response      = await graphQlClient.SendQueryAsync<StudioResponse>(request);

			return response.Data.Studio;
		}

		/// <summary>
		/// Search for a <see cref="Studio"/>
		/// </summary>
		/// <param name="search">Search query</param>
		/// <returns>Studio entry mathing the search query</returns>
		public async Task<Studio> GetStudioBySearch(string search)
		{
			var query         = $"query ($search: String) {{ Studio (search: $search) {QueryBuilder.GetStudioQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {search}};
			var response      = await graphQlClient.SendQueryAsync<StudioResponse>(request);

			return response.Data.Studio;
		}

		/// <summary>
		/// Retrieve a <see cref="Staff"/> entry from AniList
		/// </summary>
		/// <param name="id">Id of the staff entry to retrieve</param>
		/// <returns>Staff entry for the Id</returns>
		public async Task<Staff> GetStaffById(int id)
		{
			var query         = $"query ($id: Int) {{ Staff (id: $id) {QueryBuilder.GetStaffQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var response      = await graphQlClient.SendQueryAsync<StaffResponse>(request);

			var characterCount = response.Data.Staff.Characters.Edges.Length;
			if (characterCount >= 25)
			{
				var additionalCharacters = await GetAllCharactersForStaffAsync(id, 2);
				additionalCharacters.AddRange(response.Data.Staff.Characters.Edges);
				response.Data.Staff.Characters.Edges = additionalCharacters.ToArray();
			}

			return response.Data.Staff;
		}

		/// <summary>
		/// Search for a <see cref="Staff"/> entry
		/// </summary>
		/// <param name="search">Search query</param>
		/// <returns>Staff entry matching the search</returns>
		public async Task<Staff> GetStaffBySearch(string search)
		{
			var query         = $"query ($search: String) {{ Staff (search: $search) {QueryBuilder.GetStaffQuery()} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {search}};
			var response      = await graphQlClient.SendQueryAsync<StaffResponse>(request);

			var characterCount = response.Data.Staff.Characters.Edges.Length;
			if (characterCount >= 25)
			{
				var additionalCharacters = await GetAllCharactersForStaffAsync(response.Data.Staff.Id, 2);
				additionalCharacters.AddRange(response.Data.Staff.Characters.Edges);
				response.Data.Staff.Characters.Edges = additionalCharacters.ToArray();
			}

			return response.Data.Staff;
		}

		/// <summary>
		/// Retrieve all character entries for a <see cref="Staff"/> entry.
		/// </summary>
		/// <param name="id">AniList staff Id</param>
		/// <param name="page">Page to start the retrieval from</param>
		/// <returns>List of <see cref="MediaEdge"/>s for the <see cref="Staff"/></returns>
		public async Task<List<CharacterEdge>> GetAllCharactersForStaffAsync(int id, int page)
		{
			var mediaCount = 0;
			var edges = new List<CharacterEdge>();
			do
			{
				try
				{
					var query = $"query ($id: Int) {{ Staff (id: $id) {QueryBuilder.GetStaffCharactersQuery(page)} }}";
					var request = new GraphQLRequest { Query = query, Variables = new { id } };
					var response = await graphQlClient.SendQueryAsync<StaffResponse>(request);
					mediaCount = response.Data.Staff.Characters.Edges.Length;
					edges.AddRange(response.Data.Staff.Characters.Edges);

					page++;
				}
				catch (Exception)
				{
					// What to do here? We don't really mind as we can just keep trying until the end
				}
			} while (mediaCount >= 25);

			return edges;
		}

		#endregion
	}
}