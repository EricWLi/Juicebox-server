using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JuiceboxServer.Config;
using JuiceboxServer.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JuiceboxServer.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _settings;

        public JwtTokenService(IUserService userService, IOptions<JwtSettings> settings)
        {
            _userService = userService;
            _settings = settings.Value;
        }

        public string CreateToken(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_settings.Secret);
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