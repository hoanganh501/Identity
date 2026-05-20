using Identity.Contracts.Request;
using Refit;

namespace Identity.Client.Interfaces
{
    public interface IIdentityClient
    {
        [Post("/api/auth/register")]
        Task<Guid> RegisterAsync(RegistersRequest request);

        [Delete("/api/auth/{userId}")]
        Task DeleteAsync(Guid userId);
    }
}
