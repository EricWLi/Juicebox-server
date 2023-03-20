using Microsoft.AspNetCore.Mvc;
using JuiceboxServer.Models;
using JuiceboxServer.Models.Requests;
using JuiceboxServer.Services;
using JuiceboxServer.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(new UserResponseModel(user));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppUser user = model.ToUser();
            var result = await _userService.RegisterUserAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var response = new UserResponseModel(user);
            
            return Ok(response);
        }
        

        // TODO: POST /api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginResult = await _userService.SignInAsync(model.Username, model.Password);

            if (!loginResult.Succeeded)
            {
                return Forbid();
            }

            return Ok();
        }

        // TODO: GET /api/users/me
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            string id = User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;
            AppUser user = await _userService.GetUserByIdAsync(id);
            var response = new UserResponseModel(user);

            return Ok(response);
        }

        // TODO: PUT /api/users/me
    }
}