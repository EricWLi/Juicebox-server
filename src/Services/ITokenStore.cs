using JuiceboxServer.Models.Tokens;

namespace JuiceboxServer.Services
{
    public interface ITokenStore<T> where T : TokenPair
    {
        /// <summary>
        /// Stores the given token pair.
        /// </summary>
        /// <param name="pair">The token pair to store.</param>
        Task<T> Store(T pair);

        /// <summary>
        /// Retrieves the token pair for the given user ID and provider.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The token pair, or null if none exists.</returns>
        Task<T?> Retrieve(string userId);
    }
}