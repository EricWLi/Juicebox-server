using JuiceboxServer.Models;
using JuiceboxServer.Models.Responses;

namespace JuiceboxServer.Services
{
    public class SpotifyRemoteService
    {
        private readonly SpotifyAuthService _authService;
        private readonly HttpClient _httpClient = new HttpClient();

        public SpotifyRemoteService(SpotifyAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Retrieve the Spotify profile linked to the currently logged in user.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <returns>The Spotify profile</returns>
        public async Task<SpotifyProfile?> GetProfile(string userId)
        {
            string token = await _authService.GetAccessTokenAsync(userId);
            
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<SpotifyProfile>();
        }
        
        /// <summary>
        /// Retrieve the Spotify player state.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <returns>The Spotify player state</returns>
        public async Task<SpotifyDevice> GetPlayerState(string userId)
        {
            string token = await _authService.GetAccessTokenAsync(userId);
            
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/player");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);
            var device = await response.Content.ReadFromJsonAsync<SpotifyDevice>();
            return device!;
        }

        /// <summary>
        /// Retrieve the devices that the user is currently using.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <returns>The collection of devices.</returns>
        public async Task<IEnumerable<SpotifyDevice>> GetDevices(string userId)
        {
            string token = await _authService.GetAccessTokenAsync(userId);
            
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me/player/devices");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);
            var deviceResponse = await response.Content.ReadFromJsonAsync<SpotifyDeviceResponse>();
            return deviceResponse!.Devices;
        }

        /// <summary>
        /// Activate a device.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <param name="device">The device to activate</param>
        /// <returns>Whether the device was activated successfully</returns>
        public async Task<bool> TransferPlayback(string userId, string device)
        {
            string token = await _authService.GetAccessTokenAsync(userId);

            var request = new HttpRequestMessage(HttpMethod.Put, "https://api.spotify.com/v1/me/player");
            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = JsonContent.Create(new {
                device_ids = new[] { device }
            });

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Play a song.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <param name="device">The device to play on</param>
        /// <returns>Whether the song was played successfully</returns>
        public async Task<SpotifyResult> Play(string userId, string itemUri, string? device)
        {
            string token = await _authService.GetAccessTokenAsync(userId);
            
            var request = new HttpRequestMessage(HttpMethod.Put, $"https://api.spotify.com/v1/me/player/play?device={device}");
            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content = JsonContent.Create(new { uris = new[] { itemUri } });

            var response = await _httpClient.SendAsync(request);
            var result = await SpotifyResultFactory.CreateResultFromResponse(response);
            return result;
        }

        /// <summary>
        /// Add an item to the user's queue.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <param name="itemUri">The URI of the item to add</param>
        public async Task<bool> AddToQueue(string userId, string itemUri, string? device)
        {
            string token = await _authService.GetAccessTokenAsync(userId);
            
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.spotify.com/v1/me/player/queue?uri={itemUri}&device={device}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);

            // TODO: Handle 400, 404 errors

            return response.IsSuccessStatusCode;
        }
    }
}