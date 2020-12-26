using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Anilist4Net.Enums;

namespace Anilist4Net
{
	public class Client
	{
		private static readonly string UserQueryReturn = @"{
	id
	name
	aboutMd: about(asHtml: false)
	aboutHtml:about(asHtml: true)
	avatar {
		large
		medium
	}
	bannerImage
	options {
		titleLanguage
		displayAdultContent
		airingNotifications
		profileColor
	}
	mediaListOptions {
		scoreFormat
		rowOrder
		animeList {
				sectionOrder
				splitCompletedSectionByFormat
				customLists
				advancedScoring
				advancedScoringEnabled
			}
		mangaList {
			sectionOrder
			splitCompletedSectionByFormat
			customLists
			advancedScoring
			advancedScoringEnabled
		}
	}
	unreadNotificationCount
	siteUrl
	donatorTier
	donatorBadge
	moderatorStatus
	updatedAt
}";

		private static readonly string MediaQueryReturn = @"{
    id
    idMal
    title {
      romaji
      english
      native
    }
    type
    format
    status(version: 2)
    descriptionMd: description(asHtml: false)
    descriptionHtml: description(asHtml: true)
    startDate {
      year
      month
      day
    }
    endDate {
      year
      month
      day
    }
    season
    seasonYear
    seasonInt
    episodes
    duration
    chapters
    volumes
    countryOfOrigin
    isLicensed
    source(version: 2)
    hashtag
    trailer {
      id
      site
      thumbnail
    }
    updatedAt
    coverImage {
      extraLarge
      large
      medium
      color
    }
    bannerImage
    genres
    synonyms
    averageScore
    meanScore
    popularity
    isLocked
    trending
    favourites
    tags {
      id
      name
      description
      category
      rank
      isGeneralSpoiler
      isMediaSpoiler
      isAdult
    }
    relations {
      edges {
        node {
          id
        }
        relationType
      }
    }
    characters {
      edges {
        node {
          id
        }
        voiceActors {
          id
        }
      }
    }
    staff {
      edges {
        node {
          id
        }
        role
      }
    }
    studios {
      edges {
        node {
          id
        }
        isMain
      }
    }
    isAdult
    nextAiringEpisode {
      id
      airingAt
      timeUntilAiring
      episode
      mediaId
    }
    airingSchedule {
      nodes {
        id
        airingAt
        timeUntilAiring
        episode
        mediaId
      }
    }
    trends {
      nodes {
        mediaId
        date
        trending
        averageScore
        popularity
        inProgress
        releasing
        episode
      }
    }
    externalLinks {
      id
      url
      site
    }
    streamingEpisodes {
      title
      thumbnail
      url
      site
    }
    rankings {
      id
      rank
      type
      format
      year
      season
      allTime
      context
    }
    reviews {
      nodes {
        id
      }
    }
    recommendations {
      nodes {
        id
      }
    }
    stats {
      scoreDistribution {
        score
        amount
      }
      statusDistribution {
        status
        amount
      }
    }
    siteUrl
    autoCreateForumThread
    isRecommendationBlocked
    modNotes
}";

		private static readonly string ReviewQueryReturn = @"{
		id
		userId
		mediaId
		mediaType
		summary
		bodyMd: body(asHtml: false)
		bodyHtml: body(asHtml: true)
		rating
		ratingAmount
		score
		private
		siteUrl
		createdAt
		updatedAt
}";

		private static readonly string AiringScheduleQueryReturn = @"{
	id
	airingAt
	timeUntilAiring
	episode
	mediaId
}";

		private static readonly string RecommendationQueryReturn = @"{
	id
	rating
	media {
		id
	}
	mediaRecommendation {
		id
	}
	user {
		id
	}
}";

		private static readonly string CharacterQueryReturn = @"{
	id
	name {
		first
		last
		full
		native
		alternative
	}
	image {
		large
		medium
	}
	descriptionMd: description(asHtml: false)
	descriptionHtml: description(asHtml: true)
	siteUrl
	media { 
		nodes {
			id
		}
	}
	favourites
	modNotes
}";

		private static readonly string StudioQueryReturn = @"{
	id
	name
	isAnimationStudio
	media{
		nodes{
			id
		}
	}
	siteUrl
	favourites
}";

		private static readonly string StaffQueryReturn = @"{
	id
	name {
		first
		last
		full
		native
	}
	language
	descriptionMd: description(asHtml: false)
	descriptionHtml: description(asHtml: true)
	siteUrl
	staffMedia {
		nodes {
			id
		}
	}
	characters {
		nodes {
			id
		}
	}
	characterMedia {
		nodes {
			id
		}
	}
	favourites
	modNotes
}";

		public async Task<User> GetUserByName(string username)
		{
			var query         = $"query ($username: String) {{ User (name: $username) {UserQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {username}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<UserResponse>(request);

			return response.Data.User;
		}

		public async Task<User> GetUserById(int id)
		{
			var query         = $"query ($id: Int) {{ User (id: $id) {UserQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<UserResponse>(request);

			return response.Data.User;
		}

		public async Task<Media> GetMediaById(int id)
		{
			var query         = $"query ($id: Int) {{ Media (id: $id) {MediaQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<MediaResponse>(request);

			return response.Data.Media;
		}

		public async Task<Media> GetMediaByMalId(int id)
		{
			var query         = $"query ($id: Int) {{ Media (idMal: $id) {MediaQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<MediaResponse>(request);

			return response.Data.Media;
		}
		
		public async Task<Media> GetMediaBySearch(string search)
		{
			var query         = $"query ($search: String) {{ Media (search: $search) {MediaQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {search}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<MediaResponse>(request);

			return response.Data.Media;
		}
		
		public async Task<Media> GetMediaBySearch(string search, MediaTypes type)
		{
			var query         = $"query ($search: String $type: MediaType) {{ Media (search: $search type: $type) {MediaQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {search, type}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<MediaResponse>(request);

			return response.Data.Media;
		}

		public async Task<Review> GetReviewById(int id)
		{
			var query         = $"query ($id: Int) {{ Review (id: $id) {ReviewQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<ReviewResponse>(request);

			return response.Data.Review;
		}

		public async Task<AiringSchedule> GetAiringScheduleById(int id)
		{
			var query         = $"query ($id: Int) {{ AiringSchedule (id: $id) {AiringScheduleQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<AiringScheduleResponse>(request);

			return response.Data.AiringSchedule;
		}

		public async Task<Recommendation> GetRecommendationById(int id)
		{
			var query         = $"query ($id: Int) {{ Recommendation (id: $id) {RecommendationQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<RecommendationResponse>(request);

			return response.Data.Recommendation;
		}

		public async Task<Character> GetCharacterById(int id)
		{
			var query         = $"query ($id: Int) {{ Character (id: $id) {CharacterQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<CharacterResponse>(request);

			return response.Data.Character;
		}

		public async Task<Character> GetCharacterBySearch(string search)
		{
			var query         = $"query ($search: String) {{ Character (search: $search) {CharacterQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {search}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<CharacterResponse>(request);

			return response.Data.Character;
		}

		public async Task<Studio> GetStudioById(int id)
		{
			var query         = $"query ($id: Int) {{ Studio (id: $id) {StudioQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<StudioResponse>(request);

			return response.Data.Studio;
		}

		public async Task<Studio> GetStudioBySearch(string search)
		{
			var query         = $"query ($search: String) {{ Studio (search: $search) {StudioQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {search}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<StudioResponse>(request);

			return response.Data.Studio;
		}

		public async Task<Staff> GetStaffById(int id)
		{
			var query         = $"query ($id: Int) {{ Staff (id: $id) {StaffQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<StaffResponse>(request);

			return response.Data.Staff;
		}

		public async Task<Staff> GetStaffBySearch(string search)
		{
			var query         = $"query ($search: String) {{ Staff (search: $search) {StaffQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {search}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());
			var response      = await graphQlClient.SendQueryAsync<StaffResponse>(request);

			return response.Data.Staff;
		}
	}
}