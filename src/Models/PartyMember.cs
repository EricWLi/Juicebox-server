using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuiceboxServer.Models
{
    [Table("PartyMember")]
    public class PartyMember
    {
        /// <summary>
        /// The unique identifier of a person who joined the party.
        /// This ID is not the same as the user ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The party that the member is a part of.
        /// </summary>
        [ForeignKey("Id")]
        public Party Party { get; set; } = null!;

        /// <summary>
        /// The user that is a part of the party.
        /// </summary>
        [ForeignKey("Id")]
        public User User { get; set; } = null!;
    }
}