using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuiceboxServer.Models
{
    [Table("UserAccount")]
    public class User
    {
        /// <summary>
        /// The identifier of the registered Juicebox user.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The username of the registered Juicebox user.
        /// </summary>
        public string Username { get; set; } = null!;

        /// <summary>
        /// The Argon2 password hash of the registered Juicebox user.
        /// </summary>
        public string PasswordHash { get; set; } = null!;

        /// <summary>
        /// The date the user registered.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The date the user was last active.
        /// </summary>
        public DateTime LastActive { get; set;}
    }
}