using System.Security.Claims;
using JuiceboxServer.Data;
using JuiceboxServer.Models.Responses;
using JuiceboxServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuiceboxServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotifyController : ControllerBase
    {
        private readonly JuiceboxContext _context;
        private readonly SpotifyAuthService _authService;
        private readonly SpotifyRemoteService _remoteService;

        private string GetCurrentUserId() => User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;

        public SpotifyController(
            JuiceboxContext context,
            SpotifyAuthService authService,
            SpotifyRemoteService profileService
        )
        {
            _context = context;
            _authService = authService;
            _remoteService = profileService;
        }

        [HttpPost("authorize")]
        [Authorize]
        public IActionResult Authorize()
        {
            string state = _authService.CreateState();
            HttpContext.Session.SetString("state", state);

            var response = new RedirectUrlResponse
            {
                RedirectUrl = _authService.GetAuthorizationUrl(state)
            };
            
            return Ok(response);
        }

        [HttpGet("callback")]
        [Authorize]
        public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state, [FromQuery] string? error)
        {
            if (HttpContext.Session.GetString("state") != state || error != null)
            {
                return BadRequest();
            }

            string userId = GetCurrentUserId();
            await _authService.AuthorizeAsync(userId, code);
            
            return Redirect("/");
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            string userId = GetCurrentUserId();
            var profile = await _remoteService.GetProfile(userId);
            return Ok(profile);
        }

        [HttpGet("devices")]
        [Authorize]
        public async Task<IActionResult> Devices()
        {
            string userId = GetCurrentUserId();
            var devices = await _remoteService.GetDevices(userId);
            return Ok(devices);
        }

        [HttpPost("play")]
        public async Task<IActionResult> Play([FromQuery] string uri, [FromQuery] string? device)
        {
            string userId = GetCurrentUserId();
            var result = await _remoteService.Play(userId, uri, device);
            return result.IsSuccessful ? Ok(result.Content) : StatusCode(result.StatusCode, result.Error);
        }

        [HttpPost("queue")]
        public async Task<IActionResult> Queue([FromQuery] string uri, [FromQuery] string? device)
        {
            string userId = GetCurrentUserId();
            var result = await _remoteService.AddToQueue(userId, uri, device);
            return result.IsSuccessful ? Ok(result.Content) : StatusCode(result.StatusCode, result.Error);
        }
    }
}