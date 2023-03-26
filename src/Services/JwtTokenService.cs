using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JuiceboxServer.Models;
using Microsoft.IdentityModel.Tokens;

namespace JuiceboxServer.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public JwtTokenService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public string CreateToken(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
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