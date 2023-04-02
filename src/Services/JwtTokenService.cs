using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JuiceboxServer.Config;
using JuiceboxServer.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JuiceboxServer.Services
{
    /// <summary>
    /// Service to create a JSON Web Token for a user for authentication purposes.
    /// </summary>
    public class JwtTokenService : ITokenService
    {
        private readonly JwtSettings _settings;
        private readonly IUserService _userService;

        public JwtTokenService(IOptions<JwtSettings> settings, IUserService userService)
        {
            if (settings.Value.Secret == null)
            {
                throw new ApplicationException("JWT Secret is not configured.");
            }

            _settings = settings.Value;
            _userService = userService;
        }

        public async Task<string> CreateTokenAsync(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);

            if (user == null)
            {
                throw new Exception($"The username {username} was not found.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_settings.Secret!);
            var key = new SymmetricSecurityKey(secret);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
            );

            return tokenHandler.WriteToken(token);
        }
    }
}