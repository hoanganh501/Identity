using Identity.Contracts.Request;
using Identity.Contracts.Respsone;

namespace Application.Interface
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(AuthRequest request);
        Task<Guid> RegisterAsync(RegistersRequest request);
        Task DeleteAsync(Guid userId);
    }
}
