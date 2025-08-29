using AutoMapper;
using TaskManagement.Configuration.Auth;
using TaskManagement.DTOs;
using TaskManagement.DTOs.Auth;
using TaskManagement.Repositories;
using TaskManagement.Repositories.Interfaces;
using TaskManagement.Services.Helpers;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher passwordHasher,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterDTO registerDTO)
        {

            var exists = await _userRepository.AnyAsync(u => u.Username == registerDTO.Username || u.Email == registerDTO.Email);

            if (exists)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Message = "Username or Email already exists"
                };
            }

            var defaultRole = await _roleRepository.FindAsync(r => r.RoleName == "User");

            var user = new User
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                PasswordHash = _passwordHasher.HashPassword(registerDTO.Password),
                RoleId = defaultRole.First().Id,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _userRepository.AddAsync(user);

            return new AuthenticationResult
            {
                Success = true,
                Message = "Registration Complete",
                User = _mapper.Map<UserDTO>(user)
            };

        }

    }
}