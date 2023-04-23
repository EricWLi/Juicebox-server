using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace JuiceboxServer.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected string GetCurrentUserId() => User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;
    }
}