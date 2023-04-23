using System.Text.Json.Serialization;

namespace JuiceboxServer.Enums
{
    public enum ItemType
    {
        [JsonPropertyName("album")]
        Album,

        [JsonPropertyName("artist")]
        Artist,

        [JsonPropertyName("playlist")]
        Playlist,

        [JsonPropertyName("track")]
        Track,

        [JsonPropertyName("show")]
        Show,

        [JsonPropertyName("episode")]
        Episode,

        [JsonPropertyName("audiobook")]
        Audiobook
    }
}