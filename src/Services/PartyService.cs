using JuiceboxServer.Data;
using JuiceboxServer.Models;

namespace JuiceboxServer.Services
{
    public class PartyService : IPartyService
    {
        private readonly JuiceboxContext _context;
        private readonly IUserService _userService;

        public PartyService(JuiceboxContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        /// <summary>
        /// Create a new party with the specified user as the host.
        /// </summary>
        public async Task<Party> CreateAsync(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            var party = new Party
            {
                HostId = userId,
                Name = $"{user.FirstName}'s Party",
                Queue = new List<QueueItem>()
            };

            await _context.Parties.AddAsync(party);
            await _context.SaveChangesAsync();
            return party;
        }
    }
}