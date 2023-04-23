using System.Text.Json.Serialization;

namespace JuiceboxServer.Models.Spotify
{
    public class Restrictions
    {
        [JsonPropertyName("reason")]
        public RestrictionReasonType Reason { get; set; }
    }
}