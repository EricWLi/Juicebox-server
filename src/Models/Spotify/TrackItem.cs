using System.Text.Json.Serialization;

namespace JuiceboxServer.Models.Spotify
{
    public class TrackItem
    {
        [JsonPropertyName("album")]
        public AlbumItem Album { get; set; }

        [JsonPropertyName("artists")]
        public ICollection<ArtistItem> Artists { get; set; }
        
    }
}