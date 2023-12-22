using System.Linq;
using GraphQL;
using GraphQL.Client.Http;

namespace Anilist4Net;

/// <summary>
/// Contains extension methods
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Get the remaining rate limit value from the <see cref="GraphQLResponse{T}"/>
    /// </summary>
    /// <typeparam name="T">The class for the response type</typeparam>
    /// <param name="graphQlResponse">The response to pull the limit from</param>
    /// <returns>The remaining rate limit value</returns>
    public static int GetRemainingLimit<T>(this GraphQLResponse<T> graphQlResponse) where T : class
    {
        if (graphQlResponse is GraphQLHttpResponse<T> httpResponse)
        {
            var limit = httpResponse.ResponseHeaders.FirstOrDefault(x => x.Key == "X-RateLimit-Remaining");
            if (int.TryParse(limit.Value?.FirstOrDefault(), out var remaining))
            {
                return remaining;
            }
        }

        return 0;
    }

    /// <summary>
    /// Get the remaining rate limit value from a <see cref="GraphQLHttpRequestException"/>
    /// </summary>
    /// <param name="exception">Exception to pull the limit from</param>
    /// <returns>The remaining rate limit value</returns>
    public static int GetRemainingLimit(this GraphQLHttpRequestException exception)
    {
        var limit = exception.ResponseHeaders.FirstOrDefault(x => x.Key == "X-RateLimit-Remaining");
        if (int.TryParse(limit.Value?.FirstOrDefault(), out var remaining))
        {
            return remaining;
        }

        return 0;
    }
}