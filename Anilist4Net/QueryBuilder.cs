namespace Anilist4Net
{
    /// <summary>
    /// Build queries used to retrieve data from AniList API
    /// <see cref="https://anilist.co/graphiql?"/> for assistance with building queries
    /// </summary>
    public static class QueryBuilder
    {
        #region Queries

        /// <summary>
        /// Retrieve query that can be used to retrieve characters for a <see cref="Media"/> entry.
        /// This query is useful for retrieving characters when a <see cref="Media"/> entry has more than 25 characters.
        /// </summary>
        /// <param name="page">The page of characters to retrieve. Page contains 25 entries</param>
        /// <returns>Query to retrieve characters for Media</returns>
        public static string GetCharactersForMediaQuery(int page)
        {
            return @"{
                        " + CharacterQuery(page) + @"
                    }";
        }

        /// <summary>
        /// Retrieve query that can be used to retrieve media for a <see cref="Character"/> entry.
        /// This query is useful for retrieving media when a <see cref="Character"/> entry has more than 25 media entries.
        /// </summary>
        /// <param name="page">The page of media entries to retrieve. Page contains 25 entries</param>
        /// <returns>Query to retrieve media for a character</returns>
        public static string GetCharacterMediaQuery(int page)
        {
            return @"{
                        " + CharacterMediaQuery(page) + @"
                    }";
        }

        /// <summary>
        /// Retrieve query that can be used to retrieve <see cref="CharacterConnection"/> for a <see cref="Staff"/> entry
        /// </summary>
        /// <param name="page">The page of <see cref="CharacterConnection"/> entries to retrive. Page contains 25 entries</param>
        /// <returns>Query to retrieve characters for a staff entry</returns>
        public static string GetStaffCharactersQuery(int page)
        {
            return @"{
                        " + CharactersForStaff(page) + @"
                    }";
        }

        /// <summary>
        /// Retrieve query that can be used to retrieve a complete <see cref="Media"/> entry.
        /// </summary>
        /// <returns>Query to retrieve <see cref="Media"/></returns>
        public static string GetMediaQuery()
        {
            return @"{
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
                              title {
                                romaji
                                english
                                native
                              }
                              type
                            }
                            relationType
                          }
                        }"
                        + CharacterQuery(1) +
                        @"staff {
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
        }

        /// <summary>
        /// Retrieve query that can be used to retrieve a <see cref="Studio"/>
        /// </summary>
        /// <returns>Query for retrieving a studio from AniList</returns>
        public static string GetStudioQuery()
        {
            return @"{
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
        }

        /// <summary>
        /// Retrieve query that can be used to retrieve a <see cref="Recommendation"/>
        /// </summary>
        /// <returns>Query for retrieving a recommendation from AniList</returns>
        public static string GetRecommendationQuery()
        {
            return @"{
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
        }

        /// <summary>
        /// Retrieve query that can be used to retrieve a <see cref="Review"/>
        /// </summary>
        /// <returns>Query for retrieving a review from AniList</returns>
        public static string GetReviewQuery()
        {
            return @"{
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
        }

        /// <summary>
        /// Retrieve query that can be used to retrieve a <see cref="User"/>
        /// </summary>
        /// <returns>Query for retrieving a user from AniList</returns>
        public static string GetUserQuery()
        {
            return @"{
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
        }

        /// <summary>
        /// Retrieve query that can be used to retrieve a <see cref="AiringSchedule"/>
        /// </summary>
        /// <returns>Query for retrieving an AiringSchedule from AniList</returns>
        public static string GetAiringScheduleQuery()
        {
            return @"{
	                    id
	                    airingAt
	                    timeUntilAiring
	                    episode
	                    mediaId
                    }";
        }

        /// <summary>
        /// Retrieve query that can be used to retrieve a <see cref="Character"/>
        /// </summary>
        /// <returns>Query for retrieving a character from AniList</returns>
        public static string GetCharacterQuery()
        {
            return @"{
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
                        "
	                    + CharacterMediaQuery(1) +
	                    @"
                        favourites
	                    modNotes
                    }";
        }

        /// <summary>
        /// Retrieve query that can be used to retrieve a <see cref="Staff"/> entry
        /// </summary>
        /// <returns>Query for retrieving <see cref="Staff"/> entry from AniList</returns>
        public static string GetStaffQuery()
        {
            return @"{
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
	                    "
                        + CharactersForStaff(1) + 
                        @"
	                    characterMedia {
		                    nodes {
			                    id
		                    }
	                    }
	                    image {
		                    large
		                    medium
	                    }
	                    favourites
	                    modNotes
                    }";
        }

        #endregion

        #region Query Parts

        /// <summary>
        /// Get query part that can be used to retrieve <see cref="CharacterConnection"/> for <see cref="Staff"/>
        /// </summary>
        /// <param name="page">The page to retrievve</param>
        /// <returns>Query to retrieve staff characters</returns>
        private static string CharactersForStaff(int page)
        {
            return @"characters (page: " + page + @") {
                            edges{
                                node{
                                    id,
                                    name {
                                        first
                                        last
                                }
                                media {
                                    nodes{
                                        id
                                        type              
                                    }
                                }
                            }
                        }
	                }";
        }

        /// <summary>
        /// Get query part that can be used to retrieve <see cref="Character"/> media entries
        /// </summary>
        /// <param name="page">The page to retrieve</param>
        /// <returns>Query to retrieve character media</returns>
        private static string CharacterMediaQuery(int page)
        {
            return @"media (page: " + page + @") { 
		                    nodes {
			                    id
                                type
		                    }
                            edges {
                                node {
                                    id
                                }
                                characterRole
                            }
	                    }";
        }

        /// <summary>
        /// Retrieve the query for characters that form part of the <see cref="Media"/> query.
        /// This can also be used to retrieve characters for shows that have more than the page limit of 25
        /// </summary>
        /// <param name="page">The page to retrieve</param>
        /// <returns>Query to retrieve characters</returns>
        private static string CharacterQuery(int page)
        {
            return @"characters (page: " + page + @") {
                      edges {
                        node {
                          id
                        }
                        role
                        voiceActors {
                          id
                        }
                      }
                    }";
        }

        #endregion
    }
}
