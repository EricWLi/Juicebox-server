using JuiceboxServer.Models;

namespace JuiceboxServer.Services
{
    public interface ISpotifyClient
    {
        string CreateState(int length = 16);
        string GetAuthorizationUrl(string? state = null);
        Task<SpotifyToken> GetTokens(string code);
    }
}