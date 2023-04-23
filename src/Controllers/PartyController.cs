using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JuiceboxServer.Services;

namespace JuiceboxServer.Controllers
{
    [ApiController]
    [Route("/api/parties")]
    public class PartyController : BaseController
    {
        private readonly ILogger<PartyController> _logger;
        private readonly IPartyService _partyService;
        private readonly IUserService _userService;

        public PartyController(ILogger<PartyController> logger, IPartyService partyService, IUserService userService)
        {
            _logger = logger;
            _partyService = partyService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateParty()
        {
            string userId = GetCurrentUserId();
            var party = await _partyService.CreateAsync(userId);
            return Ok(party);
        }

        // TODO: GET /api/parties/{id}

        // TODO: PUT /api/parties/{id}

        // TODO: DELETE /api/parties/{id}

        // TODO: GET /api/parties/{id}/queue

        // TODO: POST /api/parties/{id}/queue

        // TODO: DELETE /api/parties/{id}/queue

        // TODO: POST /api/parties/{id}/skip
    }
}