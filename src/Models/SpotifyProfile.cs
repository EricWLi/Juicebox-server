using System.Text.Json.Serialization;

namespace JuiceboxServer.Models
{
    public class SpotifyProfile : ISpotifyContent
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("country")]
        public string? Country { get; set;}

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; } = null!;

        [JsonPropertyName("href")]
        public string? Href { get; set; }

        [JsonPropertyName("product")]
        public string? Product { get; set; }
        
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("uri")]
        public string? Uri { get; set; }
    }
}