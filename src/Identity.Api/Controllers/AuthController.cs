using Application.Interface;
using Identity.Contracts.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class AuthController: BaseApi
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(AuthRequest request)
        {
            return Ok(await _authService.LoginAsync(request));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegistersRequest request)
        {
            return Ok(await _authService.RegisterAsync(request));
        }

        [HttpDelete("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(Guid userId)
        {
            await _authService.DeleteAsync(userId);
            return Ok();
        }
    }
}
