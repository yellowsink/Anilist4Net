#nullable enable
using Anilist4Net.Connections;
using Anilist4Net.Enums;
using GraphQL;
using GraphQL.Client.Http;
using System.Collections.Generic;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Anilist4Net;

/// <summary>
/// Client used to handle retrieval of data from AniList Graph API
/// </summary>
public partial class Client
{
    /// <summary>
    /// Retrieve a <see cref="User"/> by their username
    /// </summary>
    /// <param name="username">Username of the user to retrieve</param>
    /// <returns>User data and remaining rate limit</returns>
    public async Task<(User user, int rateLimit)> GetUserByNameWithRateLimit(string username)
    {
        var query = $"query ($username: String) {{ User (name: $username) {QueryBuilder.GetUserQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { username } };
        var response = await _graphQlClient.SendQueryAsync<UserResponse>(request);

        return (response.Data.User, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve a <see cref="User"/> by their AniList Id
    /// </summary>
    /// <param name="id">user's AniList Id</param>
    /// <returns>User data and remaining rate limit</returns>
    public async Task<(User user, int rateLimit)> GetUserByIdWithRateLimit(int id)
    {
        var query = $"query ($id: Int) {{ User (id: $id) {QueryBuilder.GetUserQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { id } };
        var response = await _graphQlClient.SendQueryAsync<UserResponse>(request);

        return (response.Data.User, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve a <see cref="Media"/> entry by its AniList Id
    /// </summary>
    /// <param name="id">AniList media Id</param>
    /// <returns>Media instance if it exists, otherwise null</returns>
    public async Task<(Media? media, int rateLimitLeft)> GetMediaByIdWithRateLimit(int id)
    {
        var query = $"query ($id: Int) {{ Media (id: $id) {QueryBuilder.GetMediaQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { id } };
        GraphQLResponse<MediaResponse> response = null!;
        try
        {
            response = await _graphQlClient.SendQueryAsync<MediaResponse>(request);
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
                return (null, e.GetRemainingLimit()); // media did not exist
        }

        return (response?.Data.Media, response.GetRemainingLimit());
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
    public async Task<(Media? media, int rateLimit)> GetMediaByMalIdWithRateLimit(int id)
    {
        var query = $"query ($id: Int) {{ Media (idMal: $id) {QueryBuilder.GetMediaQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { id } };
        GraphQLResponse<MediaResponse> response = null!;
        try
        {
            response = await _graphQlClient.SendQueryAsync<MediaResponse>(request);
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
                return (null, e.GetRemainingLimit()); // media did not exist
        }

        return (response?.Data.Media, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve a <see cref="Media"/> entry by its MyAnimeList Id and its AniList type
    /// </summary>
    /// <param name="id">MAL Id to retrieve entry for</param>
    /// <param name="mediaType">The type of media to retrieve</param>
    /// <returns>Media instance if one could be found, otherwise null</returns>
    public async Task<(Media? media, int rateLimit)> GetMediaByMalIdWithRateLimit(int id, MediaTypes mediaType)
    {
        var query = $"query ($id: Int) {{ Media (idMal: $id type: {mediaType}) {QueryBuilder.GetMediaQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { id } };
        GraphQLResponse<MediaResponse> response = null!;
        try
        {
            response = await _graphQlClient.SendQueryAsync<MediaResponse>(request);
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
                return (null, e.GetRemainingLimit()); // media did not exist
        }

        return (response?.Data.Media, response.GetRemainingLimit());
    }

    /// <summary>
    /// Search for a <see cref="Media"/> entry by its title
    /// </summary>
    /// <param name="search">Search query</param>
    /// <returns>Media instance if one could be found, otherwise null</returns>
    public async Task<(Media? media, int rateLimit)> GetMediaBySearchWithRateLimit(string search)
    {
        var query = $"query ($search: String) {{ Media (search: $search) {QueryBuilder.GetMediaQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { search } };
        GraphQLResponse<MediaResponse> response = null!;
        try
        {
            response = await _graphQlClient.SendQueryAsync<MediaResponse>(request);
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
                return (null, e.GetRemainingLimit()); // media did not exist
        }

        return (response?.Data.Media, response.GetRemainingLimit());
    }

    /// <summary>
    /// Search for a <see cref="Media"/> entry by its title
    /// </summary>
    /// <param name="search">Search query</param>
    /// <param name="type">The type of media to search for</param>
    /// <returns>Media instance if one could be found, otherwise null</returns>
    public async Task<(Media? media, int rateLimit)> GetMediaBySearchWithRateLimit(string search, MediaTypes type)
    {
        var query = $"query ($search: String $type: MediaType) {{ Media (search: $search type: $type) {QueryBuilder.GetMediaQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { search, type } };
        GraphQLResponse<MediaResponse> response = null!;
        try
        {
            response = await _graphQlClient.SendQueryAsync<MediaResponse>(request);
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
                return (null, e.GetRemainingLimit()); // media did not exist
        }

        return (response?.Data.Media, response.GetRemainingLimit());
    }

    /// <summary>
    /// Search for <see cref="Media"/> entries by its title
    /// </summary>
    /// <param name="search">Search query</param>
    /// <param name="page">The page to search on</param>
    /// <param name="perPage">The number of entries per page</param>
    /// <returns>A paginated list of Media instances if found</returns>
    public async Task<(Page page, int rateLimit)> GetMediaBySearchWithRateLimit(string search, int page, int perPage)
    {
        var query = $"query ($search: String, $page: Int, $perPage: Int) {{ Page(page: $page, perPage: $perPage) {{ {QueryBuilder.GetPageInfoQuery()} media (search: $search) {QueryBuilder.GetMediaQuery()} }} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { search, page, perPage } };
        var response = await _graphQlClient.SendQueryAsync<PageResponse>(request);

        return (response.Data.Page, response.GetRemainingLimit());
    }

    /// <summary>
    /// Search for <see cref="Media"/> entries by its title
    /// </summary>
    /// <param name="search">Search query</param>
    /// <param name="type">The type of media to search for</param>
    /// <param name="page">The page to search on</param>
    /// <param name="perPage">The number of entries per page</param>
    /// <returns>A paginated list of Media instances if found</returns>
    public async Task<(Page? page, int rateLimit)> GetMediaBySearchWithRateLimit(string search, MediaTypes type, int page, int perPage)
    {
        var query = $"query ($search: String, $type: MediaType, $page: Int, $perPage: Int) {{ Page(page: $page, perPage: $perPage) {{ {QueryBuilder.GetPageInfoQuery()} media (search: $search, type: $type) {QueryBuilder.GetMediaQuery()} }} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { search, type, page, perPage } };
        var response = await _graphQlClient.SendQueryAsync<PageResponse>(request);

        return (response.Data.Page, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve all characters for a <see cref="Media"/> entry.
    /// </summary>
    /// <param name="id">AniList media Id</param>
    /// <param name="page">Page to start the retrieval from</param>
    /// <returns>List of <see cref="CharacterEdge"/>s for the Media</returns>
    public async Task<(List<CharacterEdge> characters, int rateLimit)> GetAllCharactersAsyncWithRateLimit(int id, int page)
    {
        var characterCount = 0;
        var characters = new List<CharacterEdge>();
        GraphQLResponse<MediaResponse>? response = null;
        do
        {
            try
            {
                var query = $"query ($id: Int) {{ Media (id: $id) {QueryBuilder.GetCharactersForMediaQuery(page)} }}";
                var request = new GraphQLRequest { Query = query, Variables = new { id } };
                response = await _graphQlClient.SendQueryAsync<MediaResponse>(request);
                characterCount = response.Data.Media.Characters.Edges.Length;
                characters.AddRange(response.Data.Media.Characters.Edges);
                page++;
            }
            catch (Exception)
            {
                // What to do here? We don't really mind as we can just keep trying until the end
            }
        } while (characterCount >= 25);

        return (characters, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve a <see cref="Review"/> by its AniList Id
    /// </summary>
    /// <param name="id">Id of the review to retrieve</param>
    /// <returns>Review for the requested Id</returns>
    public async Task<(Review review, int rateLimit)> GetReviewByIdWithRateLimit(int id)
    {
        var query = $"query ($id: Int) {{ Review (id: $id) {QueryBuilder.GetReviewQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { id } };
        var response = await _graphQlClient.SendQueryAsync<ReviewResponse>(request);

        return (response.Data.Review, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve an <see cref="AiringSchedule"/> entry by its AniList Id
    /// </summary>
    /// <param name="id">AniList Id of the schedule entry to retrieve</param>
    /// <returns>AiringSchedule entry</returns>
    public async Task<(AiringSchedule schedule, int rateLimit)> GetAiringScheduleByIdWithRateLimit(int id)
    {
        var query = $"query ($id: Int) {{ AiringSchedule (id: $id) {QueryBuilder.GetAiringScheduleQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { id } };
        var response = await _graphQlClient.SendQueryAsync<AiringScheduleResponse>(request);

        return (response.Data.AiringSchedule, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve a <see cref="Recommendation"/> entry by its AniList Id
    /// </summary>
    /// <param name="id">AniList Id of the recommendation to retrieve</param>
    /// <returns>Recommendation entry</returns>
    public async Task<(Recommendation recommendation, int rateLimit)> GetRecommendationByIdWithRateLimit(int id)
    {
        var query = $"query ($id: Int) {{ Recommendation (id: $id) {QueryBuilder.GetRecommendationQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { id } };
        var response = await _graphQlClient.SendQueryAsync<RecommendationResponse>(request);

        return (response.Data.Recommendation, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve a <see cref="Character"/> entry by its AniList Id
    /// </summary>
    /// <param name="id">AniList Id of the character to retrieve</param>
    /// <returns>Character entry</returns>
    public async Task<(Character character, int rateLimit)> GetCharacterByIdWithRateLimit(int id)
    {
        var query = $"query ($id: Int) {{ Character (id: $id) {QueryBuilder.GetCharacterQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { id } };
        var response = await _graphQlClient.SendQueryAsync<CharacterResponse>(request);

        var mediaCount = response.Data.Character.Media.Edges.Length;
        if (mediaCount >= 25)
        {
            var additionalMedia = await GetAllMediaEntriesForCharacter(id, 2);
            additionalMedia.edges.AddRange(response.Data.Character.Media.Edges);
            additionalMedia.nodes.AddRange(response.Data.Character.Media.Nodes);
            response.Data.Character.Media.Edges = additionalMedia.edges.ToArray();
            response.Data.Character.Media.Nodes = additionalMedia.nodes.ToArray();
        }

        return (response.Data.Character, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve all media entries for a <see cref="Character"/> entry.
    /// </summary>
    /// <param name="id">AniList character Id</param>
    /// <param name="page">Page to start the retrieval from</param>
    /// <returns>Tuple containing a list of <see cref="MediaEdge"/>s and <see cref="MediaNodePlaceholder"/>s for the <see cref="Character"/></returns>
    public async Task<(List<CharacterEdge> edges, List<MediaNodePlaceholder> nodes, int rateLimit)> GetAllMediaEntriesForCharacterWithRateLimit(int id, int page)
    {
        var mediaCount = 0;
        var edges = new List<CharacterEdge>();
        var nodes = new List<MediaNodePlaceholder>();
        GraphQLResponse<CharacterResponse>? response = null;
        do
        {
            try
            {
                var query = $"query ($id: Int) {{ Character (id: $id) {QueryBuilder.GetCharacterMediaQuery(page)} }}";
                var request = new GraphQLRequest { Query = query, Variables = new { id } };
                response = await _graphQlClient.SendQueryAsync<CharacterResponse>(request);
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

        return (edges, nodes, response.GetRemainingLimit());
    }

    /// <summary>
    /// Search for a <see cref="Character"/>
    /// </summary>
    /// <param name="search">Search query</param>
    /// <returns>Character matching the query</returns>
    public async Task<(Character character, int rateLimit)> GetCharacterBySearchWithRateLimit(string search)
    {
        var query = $"query ($search: String) {{ Character (search: $search) {QueryBuilder.GetCharacterQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { search } };
        var response = await _graphQlClient.SendQueryAsync<CharacterResponse>(request);

        return (response.Data.Character, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve a <see cref="Studio"/> by its AniList Id
    /// </summary>
    /// <param name="id">Studio Id</param>
    /// <returns>Studio for the Id</returns>
    public async Task<(Studio studio, int rateLimit)> GetStudioByIdWithRateLimit(int id)
    {
        var query = $"query ($id: Int) {{ Studio (id: $id) {QueryBuilder.GetStudioQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { id } };
        var response = await _graphQlClient.SendQueryAsync<StudioResponse>(request);

        return (response.Data.Studio, response.GetRemainingLimit());
    }

    /// <summary>
    /// Search for a <see cref="Studio"/>
    /// </summary>
    /// <param name="search">Search query</param>
    /// <returns>Studio entry matching the search query</returns>
    public async Task<(Studio studio, int rateLimit)> GetStudioBySearchWithRateLimit(string search)
    {
        var query = $"query ($search: String) {{ Studio (search: $search) {QueryBuilder.GetStudioQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { search } };
        var response = await _graphQlClient.SendQueryAsync<StudioResponse>(request);

        return (response.Data.Studio, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve a <see cref="Staff"/> entry from AniList
    /// </summary>
    /// <param name="id">Id of the staff entry to retrieve</param>
    /// <returns>Staff entry for the Id</returns>
    public async Task<(Staff staff, int rateLimit)> GetStaffByIdWithRateLimit(int id)
    {
        var query = $"query ($id: Int) {{ Staff (id: $id) {QueryBuilder.GetStaffQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { id } };
        var response = await _graphQlClient.SendQueryAsync<StaffResponse>(request);

        var characterCount = response.Data.Staff.Characters.Edges.Length;
        if (characterCount >= 25)
        {
            var additionalCharacters = await GetAllCharactersForStaffAsync(id, 2);
            additionalCharacters.AddRange(response.Data.Staff.Characters.Edges);
            response.Data.Staff.Characters.Edges = additionalCharacters.ToArray();
        }

        return (response.Data.Staff, response.GetRemainingLimit());
    }

    /// <summary>
    /// Search for a <see cref="Staff"/> entry
    /// </summary>
    /// <param name="search">Search query</param>
    /// <returns>Staff entry matching the search</returns>
    public async Task<(Staff staff, int rateLimit)> GetStaffBySearchWithRateLimit(string search)
    {
        var query = $"query ($search: String) {{ Staff (search: $search) {QueryBuilder.GetStaffQuery()} }}";
        var request = new GraphQLRequest { Query = query, Variables = new { search } };
        var response = await _graphQlClient.SendQueryAsync<StaffResponse>(request);

        var characterCount = response.Data.Staff.Characters.Edges.Length;
        if (characterCount >= 25)
        {
            var additionalCharacters = await GetAllCharactersForStaffAsync(response.Data.Staff.Id, 2);
            additionalCharacters.AddRange(response.Data.Staff.Characters.Edges);
            response.Data.Staff.Characters.Edges = additionalCharacters.ToArray();
        }

        return (response.Data.Staff, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve query that can be used to retrieve <see cref="Media"/> for a season
    /// </summary>
    /// <param name="page">The page to retrieve</param>
    /// <param name="season">The season to retrieve</param>
    /// <param name="seasonYear">The year the season is in</param>
    /// <returns>Media for the requested season</returns>
    public async Task<(Page page, int rateLimit)> GetMediaForSeasonWithRateLimit(int page, Seasons season, int seasonYear)
    {
        var query = $"query ($page: Int $season: MediaSeason, $seasonYear: Int) {{ Page (page: $page) {{ media (season: $season seasonYear: $seasonYear) {QueryBuilder.GetSeasonQuery()} }}}}";
        var request = new GraphQLRequest { Query = query, Variables = new { page, season, seasonYear } };
        var response = await _graphQlClient.SendQueryAsync<PageResponse>(request);

        return (response.Data.Page, response.GetRemainingLimit());
    }

    /// <summary>
    /// Retrieve all character entries for a <see cref="Staff"/> entry.
    /// </summary>
    /// <param name="id">AniList staff Id</param>
    /// <param name="page">Page to start the retrieval from</param>
    /// <returns>List of <see cref="MediaEdge"/>s for the <see cref="Staff"/></returns>
    public async Task<(List<CharacterEdge> characters, int rateLimit)> GetAllCharactersForStaffWithRateLimitAsync(int id, int page)
    {
        var mediaCount = 0;
        var edges = new List<CharacterEdge>();
        GraphQLResponse<StaffResponse>? response = null;
        do
        {
            try
            {
                var query = $"query ($id: Int) {{ Staff (id: $id) {QueryBuilder.GetStaffCharactersQuery(page)} }}";
                var request = new GraphQLRequest { Query = query, Variables = new { id } };
                response = await _graphQlClient.SendQueryAsync<StaffResponse>(request);
                mediaCount = response.Data.Staff.Characters.Edges.Length;
                edges.AddRange(response.Data.Staff.Characters.Edges);

                page++;
            }
            catch (Exception)
            {
                // What to do here? We don't really mind as we can just keep trying until the end
            }
        } while (mediaCount >= 25);

        return (edges, response.GetRemainingLimit());
    }
}