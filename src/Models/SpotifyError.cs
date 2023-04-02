using System.Text.Json.Serialization;

namespace JuiceboxServer.Models
{
    public class SpotifyError
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

    }
}