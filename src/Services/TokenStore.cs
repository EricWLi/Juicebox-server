using JuiceboxServer.Data;
using JuiceboxServer.Models.Tokens;
using Microsoft.EntityFrameworkCore;

namespace JuiceboxServer.Services
{
    /// <summary>
    /// Service for storing access and refresh token pairs for external web services.
    /// </summary>
    public class TokenStore<T> : ITokenStore<T> where T : TokenPair
    {
        private readonly ILogger<TokenStore<T>> _logger;
        private readonly JuiceboxContext _context;

        public TokenStore(
            ILogger<TokenStore<T>> logger,
            JuiceboxContext context
        )
        {
            _logger = logger;
            _context = context;
        }

        public async Task<T> Store(T pair) {
            var token = await _context.Tokens.OfType<T>().FirstOrDefaultAsync(t => 
                t.UserId == pair.UserId);

            if (token != null)
            {
                token.AccessToken = pair.AccessToken;
                token.Expires = pair.Expires;
                
                if (pair.RefreshToken != null)
                {
                    token.RefreshToken = pair.RefreshToken;
                }
            }
            else
            {
                _context.Tokens.Add(pair);
                token = pair;
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Stored token pair for user {userId}", pair.UserId);

            return token;
        }

        public async Task<T?> Retrieve(string userId)
        {
            var token = await _context.Tokens.OfType<T>().FirstOrDefaultAsync(t => 
                t.UserId == userId);
            
            return token;
        }
    }
}