using JuiceboxServer.Models;

namespace JuiceboxServer.Services
{
    /// <summary>
    /// This interface is used to create a token for a user for authentication purposes.
    /// </summary>
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(string username);
    }
}