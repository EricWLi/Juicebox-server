using System.ComponentModel.DataAnnotations;

namespace JuiceboxServer.Models
{
    public class SpotifyToken
    {
        [Key]
        public int Id { get; set; }
        public AppUser User { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTimeOffset ExpiresAt { get; set; }
    }
}