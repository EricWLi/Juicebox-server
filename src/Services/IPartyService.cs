using JuiceboxServer.Models;

namespace JuiceboxServer.Services
{
    public interface IPartyService
    {
        Task<Party> CreateAsync(string userId);
    }
}