using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuiceboxServer.Models
{
    [Table("Party")]
    public class Party
    {
        /// <summary>
        /// The id of the party.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the room/party. (Optional)
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The user who created the party.
        /// </summary>
        [ForeignKey("Id")]
        public User Host { get; set; } = null!;
    }
}