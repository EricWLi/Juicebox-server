using System.Text.Json.Serialization;

namespace JuiceboxServer.Models.Responses
{
    public class SpotifyErrorResponse
    {
        [JsonPropertyName("error")]
        public SpotifyError Error { get; set; } = null!;
    }
}