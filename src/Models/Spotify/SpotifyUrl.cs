using System.Text.Json.Serialization;

namespace JuiceboxServer.Models.Spotify
{
    public class SpotifyUrl
    {
        [JsonPropertyName("spotify")]
        public string? Spotify { get; set; }
    }
}