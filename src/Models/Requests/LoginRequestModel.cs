namespace JuiceboxServer.Models.Requests
{
    public class LoginRequestModel
    {
        /// <summary>
        /// The username of the user logging in.
        /// </summary>
        public string Username { get; set; } = null!;

        /// <summary>
        /// The password of the user logging in.
        /// </summary>
        public string Password { get; set; } = null!;
    }
}