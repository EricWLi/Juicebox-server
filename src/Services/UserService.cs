using JuiceboxServer.Models;
using Microsoft.AspNetCore.Identity;

namespace JuiceboxServer.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(
            ILogger<UserService> logger,
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <param name="user">The new user to be added to the database. Must have a unique username.</param>
        /// <param name="password">The password for the new user</param>
        /// <returns>An IdentityResult indicating if registration was successful.</returns>
        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            _logger.LogInformation($"Registering user {user.UserName}");
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User {user.UserName} registered");
            }
            else
            {
                _logger.LogInformation($"User {user.UserName} registration failed");
            }

            return result;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}