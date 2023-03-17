using Microsoft.AspNetCore.Mvc;
using JuiceboxServer.Data;

namespace JuiceboxServer.Controllers
{
    public class SongController : ControllerBase
    {
        private readonly ILogger<SongController> _logger;
        private readonly JuiceboxContext _context;

        public SongController(ILogger<SongController> logger, JuiceboxContext context)
        {
            _logger = logger;
            _context = context;
        }

        // TODO: GET /api/songs/search
    }
}