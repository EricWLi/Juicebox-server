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
        private readonly ITokenService _tokenService;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            ITokenService tokenService
        )
        {
            _logger = logger;
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(new UserResponse(user));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
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

            var response = new UserResponse(user);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
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
            
            AppUser user = await _userService.GetUserByUsernameAsync(model.Username);
            string accessToken = _tokenService.CreateToken(user);

            var response = new AuthenticationResponse {
                Username = model.Username,
                AccessToken = accessToken
            };

            return Ok(response);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            string userId = User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;
            AppUser user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var response = new UserResponse(user);
            return Ok(response);
        }
    }
}