using System.Text.Json.Serialization;
using JuiceboxServer.Models.Spotify;

namespace JuiceboxServer.Models.Responses
{
    public class SpotifySearchResponse : ISpotifyContent
    {
        [JsonPropertyName("tracks")]
        public SearchObject<TrackItem> Tracks { get; set; } = null!;

        [JsonPropertyName("artists")]
        public SearchObject<ArtistItem> Artists { get; set; } = null!;

        [JsonPropertyName("albums")]
        public SearchObject<AlbumItem> Albums { get; set; } = null!;

        [JsonPropertyName("playlists")]
        public SearchObject<PlaylistItem> Playlists { get; set; } = null!;

        [JsonPropertyName("shows")]
        public SearchObject<ShowItem> Shows { get; set; } = null!;

        [JsonPropertyName("episodes")]
        public SearchObject<EpisodeItem> Episodes { get; set; } = null!;

        [JsonPropertyName("audiobooks")]
        public SearchObject<AudiobookItem> Audiobooks { get; set; } = null!;
    }
}