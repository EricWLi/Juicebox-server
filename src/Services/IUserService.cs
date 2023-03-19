using JuiceboxServer.Models;
using Microsoft.AspNetCore.Identity;

namespace JuiceboxServer.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<IdentityResult> RegisterUserAsync(User user, string password);
        Task<bool> DeleteUserAsync(int id);
        Task UpdateUserAsync(User user);
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task<bool> UserExistsAsync(int id);
        Task<bool> UserExistsAsync(string username);
    }
}