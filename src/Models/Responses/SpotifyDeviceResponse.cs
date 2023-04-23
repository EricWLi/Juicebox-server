using System.Text.Json.Serialization;
using JuiceboxServer.Models.Spotify;

namespace JuiceboxServer.Models.Responses
{
    public class SpotifyDeviceResponse : ISpotifyContent
    {
        [JsonPropertyName("devices")]
        public ICollection<Device> Devices { get; set; } = null!;
    }
}