using System.Text;
using JuiceboxServer.Config;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.WebUtilities;
using JuiceboxServer.Models;
using JuiceboxServer.Models.Responses;

namespace JuiceboxServer.Services
{
    public class SpotifyClient : ISpotifyClient
    {
        /// <summary>
        /// The scopes that the client will be requesting.
        /// See https://developer.spotify.com/documentation/general/guides/authorization/scopes/
        /// </summary>
        private static readonly String[] AuthorizationScope = {
            "user-read-private",
            "user-read-email",
            "user-read-playback-state",
            "user-modify-playback-state",
            "user-read-currently-playing"
        };

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly SpotifySettings _settings;
        private readonly ILogger<SpotifyClient> _logger;

        public SpotifyClient(
            IOptions<SpotifySettings> settings,
            ILogger<SpotifyClient> logger
        )
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public string CreateState(int length = 16)
        {
            var sb = new StringBuilder();
            var rand = new Random();
            string possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            for (int i = 0; i < length; i++)
            {
                sb.Append(possible[rand.Next(possible.Length)]);
            }

            return sb.ToString();
        }

        public string GetAuthorizationUrl(string? state = null)
        {
            string scope = String.Join(" ", AuthorizationScope);
            string baseAuthUrl = "https://accounts.spotify.com/authorize";

            var queryParams = new Dictionary<string, string?>
            {
                { "response_type", "code" },
                { "client_id", _settings.ClientId },
                { "redirect_uri", _settings.RedirectUri },
                { "scope", scope }
            };

            if (state != null)
            {
                queryParams.Add("state", state);
            }

            return QueryHelpers.AddQueryString(baseAuthUrl, queryParams);
        }

        public async Task<SpotifyToken> GetTokens(string code)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            string basicAuth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.ClientId}:{_settings.ClientSecret}"));

            request.Headers.Add("Authorization", $"Basic {basicAuth}");
            request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", _settings.RedirectUri }
            });
            
            var response = await _httpClient.SendAsync(request);
            var tokenResponse = await response.Content.ReadFromJsonAsync<SpotifyTokenResponse>();

            return new SpotifyToken
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken
            };
        }
    }
}