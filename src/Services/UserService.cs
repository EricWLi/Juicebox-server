using JuiceboxServer.Models;
using Microsoft.AspNetCore.Identity;

namespace JuiceboxServer.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserService(
            ILogger<UserService> logger,
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager)
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
        public async Task<IdentityResult> RegisterUserAsync(AppUser user, string password)
        {
            _logger.LogInformation($"Registering user {user.UserName}");
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        /// <summary>
        /// Deletes a user from the database.
        /// <summary>
        /// <param name="username">The username of the user to be deleted.</param>
        /// <returns>A boolean indicating if the deletion was successful.</returns>
        public async Task<bool> DeleteUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return false;
            }

            _logger.LogInformation($"Deleting user {user.UserName}");
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        /// <summary>
        /// Gets a user by the user ID.
        /// </summary>
        /// <param name="id">The ID of the user to be retrieved.</param>
        /// <returns>The user with the given ID.</returns>
        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        /// <summary>
        /// Gets a user by the username.
        /// </summary>
        /// <param name="username">The username of the user to be retrieved.</param>
        /// <returns>The user with the given username.</returns>
        public Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return _userManager.FindByNameAsync(username);
        }

        /// <summary>
        /// Updates a user object.
        /// </summary>
        /// <param name="user">The user to be updated.</param>
        /// <returns>An IdentityResult indicating if the update was successful.</returns>
        public async Task<IdentityResult> UpdateUserAsync(AppUser user)
        {
            _logger.LogInformation($"Updating user {user.UserName}");
            return await _userManager.UpdateAsync(user);
        }

        /// <summary>
        /// Checks if a username already exists in the database.
        /// </summary>
        /// <param name="username">The username to be checked.</param>
        /// <returns>A boolean indicating if the username already exists.</returns>
        public async Task<bool> UsernameExistsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }

        /// <summary>
        /// Checks if the user credentials are valid.
        /// </summary>
        /// <param name="username">The username to be checked.</param>
        /// <param name="password">The password to be checked.</param>
        /// <returns>A boolean indicating if the credentials are valid.</returns>
        public Task<SignInResult> SignInAsync(string username, string password)
        {
            return _signInManager.PasswordSignInAsync(username, password, true, false);
        }
    }
}