namespace JuiceboxServer.Models.Requests
{
    public class RegisterRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public AppUser ToUser()
        {
            return new AppUser
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
    
