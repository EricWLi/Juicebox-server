using System.Text.Json.Serialization;

namespace JuiceboxServer.Models.Responses
{
    public class SpotifyDeviceResponse
    {
        [JsonPropertyName("devices")]
        public IEnumerable<SpotifyDevice> Devices { get; set; } = null!;
    }
}