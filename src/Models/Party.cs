using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JuiceboxServer.Models
{
    public class Party
    {
        /// <summary>
        /// The id of the party.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The id of the person hosting the party.
        /// </summary>
        public string HostId { get; set; } = null!;

        /// <summary>
        /// The name of the room/party. (Optional)
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The date the party was created.
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date the party was last updated.
        /// </summary>
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;


        /**
         * Navigation Properties
         */

        /// <summary>
        /// The navigation property to the user that created the party.
        /// </summary>
        [JsonIgnore]
        public AppUser Host { get; } = null!;

        /// <summary>
        /// The party members.
        /// </summary>
        public ICollection<AppUser> Members { get; set; } = null!;

        /// <summary>
        /// The party's song queue.
        /// </summary>
        public ICollection<QueueItem> Queue { get; set; } = null!;

    }
}