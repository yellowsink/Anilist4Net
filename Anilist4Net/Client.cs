using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace Anilist4Net
{
	public class Client
	{
		public async Task<User> GetUserByName(string username)
		{
			var query = @"query ($username: String) { User (name: $username) {
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
} }";
			var request       = new GraphQLRequest {Query = query, Variables = new {username}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());

			var response = await graphQlClient.SendQueryAsync<UserResponse>(request);

			return response.Data.User;
		}

		public async Task<User> GetUserById(int id)
		{
			var query = @"query ($id: Int) { User (id: $id) {
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
} }";
			var request       = new GraphQLRequest {Query = query, Variables = new {id}};
			var graphQlClient = new GraphQLHttpClient("https://graphql.anilist.co", new SystemTextJsonSerializer());

			var response = await graphQlClient.SendQueryAsync<UserResponse>(request);

			return response.Data.User;
		}
	}
}