using Microsoft.AspNetCore.Identity;

namespace JuiceboxServer.Models
{
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// The first name of the registered Juicebox user.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// The last name of the registered Juicebox user.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// The date the user registered.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The date the user was last active.
        /// </summary>
        public DateTime DateUpdated { get; set; }

        /**
         * Navigation Properties
         */

        /// <summary>
        /// The list of parties that the user owns.
        /// </summary>
        public ICollection<Party> HostedParties { get; set; } = null!;

        /// <summary>
        /// The list of parties the user is a member of.
        /// </summary>
        public ICollection<Party> JoinedParties { get; set; } = null!;
    }
}