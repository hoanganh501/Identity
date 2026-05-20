using Application.Interface;
using Domain.Interface;

namespace Application.Services
{
    public class UserService: IUserService
    {
        public readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
