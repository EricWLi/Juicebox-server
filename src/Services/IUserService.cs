using JuiceboxServer.Models;
using Microsoft.AspNetCore.Identity;

namespace JuiceboxServer.Services
{
    public interface IUserService
    {
        Task<AppUser> GetUserByIdAsync(string id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<IdentityResult> RegisterUserAsync(AppUser user, string password);
        Task<bool> DeleteUserAsync(string username);
        Task<IdentityResult> UpdateUserAsync(AppUser user);
        Task<SignInResult> SignInAsync(string username, string password);
        Task<bool> UsernameExistsAsync(string username);
    }
}