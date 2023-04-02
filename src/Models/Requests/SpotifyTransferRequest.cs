using System.Text.Json.Serialization;

namespace JuiceboxServer.Models.Requests
{
    public class SpotifyTransferRequest
    {
        [JsonPropertyName("device")]
        public string Device { get; set; } = null!;
    }
}