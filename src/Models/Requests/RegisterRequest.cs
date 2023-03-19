namespace JuiceboxServer.Models.Requests
{
    public class RegisterRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public User ToUser()
        {
            return new User
            {
                UserName = Username,
                FirstName = FirstName,
                LastName = LastName,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };
        }
    }
}
    
