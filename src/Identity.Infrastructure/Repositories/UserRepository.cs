using Domain.Entity;
using Domain.Interface;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
