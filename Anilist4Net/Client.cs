using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

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
      }
    }
    recommendations {
      nodes {
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

		public async Task<User> GetUserByName(string username)
		{
			var query         = $"query ($username: String) {{ User (name: $username) {UserQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {username}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());

			var response = await graphQlClient.SendQueryAsync<UserResponse>(request);

			return response.Data.User;
		}

		public async Task<User> GetUserById(int id)
		{
			var query         = $"query ($id: Int) {{ User (id: $id) {UserQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());

			var response = await graphQlClient.SendQueryAsync<UserResponse>(request);

			return response.Data.User;
		}

		public async Task GetMediaById(int id)
		{
			var query         = $"query ($id: int) {{ Media (id: $id) {MediaQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());

			var response = await graphQlClient.SendQueryAsync<MediaResponse>(request);
		}

		public async Task GetMediaByMalId(int id)
		{
			var query         = $"query ($id: int) {{ Media (idMal: $id) {MediaQueryReturn} }}";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());

			var response = await graphQlClient.SendQueryAsync<MediaResponse>(request);
		}
	}
}