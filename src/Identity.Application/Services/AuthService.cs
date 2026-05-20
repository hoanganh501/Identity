using Application.Interface;
using AutoMapper;
using Domain.Entity;
using Domain.Interface;
using Identity.Contracts.Request;
using Identity.Contracts.Respsone;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthService(UserManager<ApplicationUser> userManager, 
            IConfiguration configuration,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<AuthResponse> LoginAsync(AuthRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.username) ?? throw new Exception("user not found");

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.password)) throw new UnauthorizedAccessException("Invalid username or password.");

            var roles = await _userManager.GetRolesAsync(user);

            var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

            var credentials = new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),           
            ];

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var result = _mapper.Map<AuthResponse>(user);
            result.AccessToken = tokenString;
            return result;
        }

        public async Task<Guid> RegisterAsync(RegistersRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            if (!IsValidPassword(request.Password))
            {
                throw new Exception("Password must contain uppercase, lowercase, number and special character");
            }

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpperInvariant(),
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ",result.Errors.Select(x => x.Description));
                throw new Exception(errors);
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            return user.Id;
        }

        private static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password)
                   && password.Length >= 8
                   && password.Any(char.IsUpper)
                   && password.Any(char.IsLower)
                   && password.Any(char.IsDigit)
                   && password.Any(x => !char.IsLetterOrDigit(x));
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new Exception("User not found");
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(x => x.Description));
                throw new Exception(errors);
            }
        }
    }
}
