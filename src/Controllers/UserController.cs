using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JuiceboxServer.Data;
using JuiceboxServer.Models;

namespace JuiceboxServer.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly JuiceboxContext _context;

        public UserController(ILogger<UserController> logger, JuiceboxContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        // TODO: POST /api/users/register

        // TODO: POST /api/users/login

        // TODO: GET /api/users/me

        // TODO: PUT /api/users/me
    }
}