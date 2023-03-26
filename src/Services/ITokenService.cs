using JuiceboxServer.Models;

namespace JuiceboxServer.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}