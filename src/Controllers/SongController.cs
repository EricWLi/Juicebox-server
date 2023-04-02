using Microsoft.AspNetCore.Mvc;
using JuiceboxServer.Data;
using JuiceboxServer.Services;

namespace JuiceboxServer.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public class SongController : ControllerBase
    {
        private readonly ILogger<SongController> _logger;
        private readonly JuiceboxContext _context;

        public SongController(
            ILogger<SongController> logger,
            JuiceboxContext context
        )
        {
            _logger = logger;
            _context = context;
        }

        // TODO: GET /api/songs/search
        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            throw new NotImplementedException();
        }
    }
}