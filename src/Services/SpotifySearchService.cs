using JuiceboxServer.Models.QueryParameters;
using JuiceboxServer.Models.Responses;
using JuiceboxServer.Models.Spotify;
using Microsoft.AspNetCore.WebUtilities;

namespace JuiceboxServer.Services
{
    public class SpotifySearchService
    {
        private readonly SpotifyAuthService _authService;
        private readonly HttpClient _httpClient = new HttpClient();

        public SpotifySearchService(SpotifyAuthService authService)
        {
            _authService = authService;
        }

        public async Task<SpotifyResult> Search(string userId, SearchQueryParameter searchParams)
        {
            string token = await _authService.GetAccessTokenAsync(userId);

            var queryParams = new Dictionary<string, string?>
            {
                { "q", searchParams.Query },
                { "type", searchParams.Type },
                { "market", searchParams.Market },
                { "limit", searchParams.Limit?.ToString() },
                { "offset", searchParams.Offset?.ToString() },
                { "include_external", searchParams.IncludeExternal }
            };

            string url = QueryHelpers.AddQueryString("https://api.spotify.com/v1/search", queryParams);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);
            return await SpotifyResultFactory.CreateResultFromResponse<SpotifySearchResponse>(response);
        }
    }
}