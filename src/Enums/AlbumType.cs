using System.Text.Json.Serialization;

namespace JuiceboxServer.Enums
{
    public enum AlbumType
    {
        [JsonPropertyName("album")]
        Album,

        [JsonPropertyName("single")]
        Single,

        [JsonPropertyName("compilation")]
        Compilation
    }
}