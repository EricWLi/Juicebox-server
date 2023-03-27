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
        private readonly ISpotifyClient _client;

        public SpotifyController(JuiceboxContext context, ISpotifyClient client)
        {
            _context = context;
            _client = client;
        }

        [HttpPost("authorize")]
        [Authorize]
        public async Task<IActionResult> Authorize()
        {
            string state = _client.CreateState();
            HttpContext.Session.SetString("state", state);

            var response = new RedirectUrlResponse
            {
                RedirectUrl = _client.GetAuthorizationUrl(state)
            };
            
            return Ok(response);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state, [FromQuery] string? error)
        {
            if (HttpContext.Session.GetString("state") != state)
            {
                return BadRequest();
            }

            return Redirect("/");
        
        }
    }
}