using System.ComponentModel.DataAnnotations;

namespace JuiceboxServer.Models.Tokens
{
    public abstract class TokenPair
    {

        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? Expires { get; set; }
    }
}