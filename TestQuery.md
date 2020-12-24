# Test Query
Use this test query [here](https://anilist.co/graphiql) to get an example API response with all query types.

```graphql
{
  User (name: "Yellowsink") { # my profile!
  id
  name
  aboutPlain: about(asHtml: false)
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
	}
  Media (id: 1) { # Cowboy Bebop. Chosen purely for being id 1.
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
  }
  Review (id: 760){ # a review of Cowboy Bebop. chosen because it was convenient.
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
  AiringSchedule (id: 293185) { # an episode of HYPNOSISMIC cause I needed something currently airing
    id
    airingAt
    timeUntilAiring
    episode
    mediaId
  }
  Recommendation (id: 250){ # some random reccomendation, probably for Cowboy Bebop (again, convenience)
    id
    rating
    userRating
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
```
