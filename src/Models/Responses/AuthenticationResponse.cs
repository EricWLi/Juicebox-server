namespace JuiceboxServer.Models.Responses
{
    public class AuthenticationResponse
    {
        /// <summary>
        /// Whether or not the authentication was successful.
        /// </summary>
        public bool Result { get; set; } = true;

        /// <summary>
        /// The JWT access token.
        /// </summary>
        public string AccessToken { get; set; } = null!;

        /// <summary>
        /// The user information for the authenticated user.
        /// </summary>
        public string Username { get; set; } = null!;
    }
}