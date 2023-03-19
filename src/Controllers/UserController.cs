using Microsoft.AspNetCore.Mvc;
using JuiceboxServer.Models;
using JuiceboxServer.Models.Requests;
using JuiceboxServer.Services;

namespace JuiceboxServer.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<User> GetUser(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostRegisterUser([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = model.ToUser();
            var result = await _userService.RegisterUserAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            
            return Ok();
        }
        

        // TODO: POST /api/users/login

        // TODO: GET /api/users/me

        // TODO: PUT /api/users/me
    }
}