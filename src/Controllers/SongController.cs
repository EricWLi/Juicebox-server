using Microsoft.AspNetCore.Mvc;
using JuiceboxServer.Data;
using JuiceboxServer.Services;

namespace JuiceboxServer.Controllers
{
    [Route("api/songs")]
    public class SongController : ControllerBase
    {
        private readonly ILogger<SongController> _logger;
        private readonly JuiceboxContext _context;
        private readonly IApiClient _apiClient;

        public SongController(
            ILogger<SongController> logger,
            JuiceboxContext context,
            IApiClient apiClient
        )
        {
            _logger = logger;
            _context = context;
            _apiClient = apiClient;
        }

        // TODO: GET /api/songs/search
        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            throw new NotImplementedException();
        }
    }
}