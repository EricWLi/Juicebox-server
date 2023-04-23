using System.Text.Json.Serialization;

namespace JuiceboxServer.Models.Spotify
{
    public class Image
    {
        /// <summary>
        /// The source URL of the image.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;

        /// <summary>
        /// The image height in pixels.
        /// </summary>
        [JsonPropertyName("height")]
        public int? Height { get; set; }

        /// <summary>
        /// The image width in pixels.
        /// </summary>
        [JsonPropertyName("width")]
        public int? Width { get; set;}
    }
}