using JuiceboxServer.Config;
using Microsoft.Extensions.Options;

namespace JuiceboxServer.Services
{
    public class SpotifyClient : ISpotifyClient
    {
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

        public string GetAuthorizationUrl()
        {
            throw new NotImplementedException();
        }
    }
}