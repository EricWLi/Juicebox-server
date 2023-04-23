using System.Text.Json.Serialization;

namespace JuiceboxServer.Models.Spotify
{
    public enum RestrictionReasonType
    {
        [JsonPropertyName("market")]
        Market,

        [JsonPropertyName("product")]
        Product,

        [JsonPropertyName("explicit")]
        Explicit
    }
}