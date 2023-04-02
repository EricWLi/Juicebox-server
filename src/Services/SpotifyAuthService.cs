using System.Text;
using JuiceboxServer.Config;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.WebUtilities;
using JuiceboxServer.Models.Responses;
using JuiceboxServer.Models.Tokens;

namespace JuiceboxServer.Services
{
    public class SpotifyAuthService
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
        private readonly ILogger<SpotifyAuthService> _logger;
        private readonly ITokenStore<SpotifyToken> _tokenStore;

        private readonly string basicAuth;

        public SpotifyAuthService(
            IOptions<SpotifySettings> settings,
            ILogger<SpotifyAuthService> logger,
            ITokenStore<SpotifyToken> tokenStore
        )
        {
            _settings = settings.Value;
            _logger = logger;
            _tokenStore = tokenStore;

            basicAuth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.ClientId}:{_settings.ClientSecret}"));
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

            var queryParams = new Dictionary<string, string?>
            {
                { "response_type", "code" },
                { "client_id", _settings.ClientId },
                { "redirect_uri", _settings.RedirectUri },
                { "scope", scope },
                { "state", state }
            };

            return QueryHelpers.AddQueryString("https://accounts.spotify.com/authorize", queryParams);
        }

        public async Task AuthorizeAsync(string userId, string authorizationCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            request.Headers.Add("Authorization", $"Basic {basicAuth}");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", authorizationCode },
                { "redirect_uri", _settings.RedirectUri! }
            });
            
            var response = await _httpClient.SendAsync(request);
            var tokenResponse = await response.Content.ReadFromJsonAsync<SpotifyTokenResponse>();
            
            await _tokenStore.Store(new SpotifyToken
            {
                UserId = userId,
                AccessToken = tokenResponse!.AccessToken,
                RefreshToken = tokenResponse!.RefreshToken,
                Expires = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
            });

            _logger.LogInformation("User {UserId} authorized with Spotify.", userId);
        }

        private async Task<SpotifyToken> RefreshTokenAsync(string userId, string refreshToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

            request.Headers.Add("Authorization", $"Basic {basicAuth}");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            });
            
            var response = await _httpClient.SendAsync(request);
            var tokenResponse = await response.Content.ReadFromJsonAsync<SpotifyTokenResponse>();

            if (tokenResponse == null)
            {
                throw new Exception("Token refresh failed.");
            }

            if (tokenResponse.Error == "invalid_grant")
            {
                // TODO: Create BadRefreshTokenException
                throw new Exception("Invalid refresh token");
            }

            var token = new SpotifyToken
            {
                UserId = userId,
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken,
                Expires = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
            };

            _logger.LogInformation("Refreshed tokens for User {UserId}", userId);

            return await _tokenStore.Store(token);
        }

        public async Task<string> GetAccessTokenAsync(string userId)
        {
            var tokenPair = await _tokenStore.Retrieve(userId);

            if (tokenPair == null || tokenPair.RefreshToken == null)
            {
                // TODO: SpotifyUnauthorizedException
                throw new Exception("Authorization with Spotify required.");
            }

            if (tokenPair.Expires <= DateTime.UtcNow)
            {
                tokenPair = await RefreshTokenAsync(userId, tokenPair.RefreshToken);
            }

            return tokenPair.AccessToken!;
        }
    }
}