using System.Text.Json.Serialization;
using JuiceboxServer.Models.Spotify;

namespace JuiceboxServer.Models.Responses
{
    public class SpotifyErrorResponse
    {
        [JsonPropertyName("error")]
        public SpotifyError Error { get; set; } = null!;
    }
}