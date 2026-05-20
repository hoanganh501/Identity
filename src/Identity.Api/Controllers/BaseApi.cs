using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class BaseApi : Controller
    {
        protected Guid GetCurrentUser()
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User?.FindFirst("sub")?.Value;

            if (Guid.TryParse(userId, out var guid))
                return guid;

            throw new UnauthorizedAccessException("Invalid user id in token");
        }
    }
}
