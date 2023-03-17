using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JuiceboxServer.Data;
using JuiceboxServer.Models;

namespace JuiceboxServer.Controllers
{
    [ApiController]
    [Route("/api/parties")]
    public class PartyController : ControllerBase
    {
        private readonly ILogger<PartyController> _logger;
        private readonly JuiceboxContext _context;

        public PartyController(ILogger<PartyController> logger, JuiceboxContext context)
        {
            _logger = logger;
            _context = context;
        }

        // TODO: POST /api/parties

        // TODO: GET /api/parties/{id}

        // TODO: PUT /api/parties/{id}

        // TODO: DELETE /api/parties/{id}

        // TODO: GET /api/parties/{id}/queue

        // TODO: POST /api/parties/{id}/queue

        // TODO: DELETE /api/parties/{id}/queue

        // TODO: POST /api/parties/{id}/skip
    }
}