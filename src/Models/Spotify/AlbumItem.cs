using System.Text.Json.Serialization;
using JuiceboxServer.Enums;

namespace JuiceboxServer.Models.Spotify
{
    public class AlbumItem
    {
        [JsonPropertyName("album_type")]
        public AlbumType AlbumType { get; set; }

        [JsonPropertyName("total_tracks")]
        public int TotalTracks { get; set; }

        [JsonPropertyName("available_markets")]
        public ICollection<string> AvailableMarkets { get; set; } = null!;

        [JsonPropertyName("external_urls")]
        public SpotifyUrl ExternalUrls { get; set; } = null!;

        [JsonPropertyName("href")]
        public string Href { get; set; } = null!;

        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("images")]
        public ICollection<Image> Images { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = null!;

        [JsonPropertyName("release_date_precision")]
        public DateType ReleaseDatePrecision { get; set; }

        [JsonPropertyName("restrictions")]
        public Restrictions Restrictions { get; set; } = null!;

        [JsonPropertyName("type")]
        public ItemType Type { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; } = null!;
    }
}