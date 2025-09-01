using AutoMapper;
using TaskManagement.Configuration.Auth;
using TaskManagement.DTOs;
using TaskManagement.DTOs.Auth;
using TaskManagement.Models;
using TaskManagement.Repositories;
using TaskManagement.Repositories.Interfaces;
using TaskManagement.Services.Interfaces;
using TaskManagement.Utilities.EmailServices;

namespace TaskManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher passwordHasher,
            IAuthRepository authRepository,
            IEmailService emailService,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _authRepository = authRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerDTO)
        {
            var exists = await _authRepository.GetByEmailAsync(registerDTO.Email);

            if (exists != null)
            {
                return false;
            }

            var user = _mapper.Map<User>(registerDTO);
            user.PasswordHash = _passwordHasher.HashPassword(registerDTO.Password);
            user.RoleId = 2;
            user.CreatedAt = DateTime.UtcNow;
            await _authRepository.AddAsync(user);

            _ = SendWelcomeEmail(user);
            return true;
        }

        public async Task<User> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _authRepository.GetByEmailAsync(loginDTO.Email);

            var isValidPassword = _passwordHasher.VerifyPassword(loginDTO.Password, user.PasswordHash);

            if (user == null || !isValidPassword)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            await _authRepository.UpdateLastLoginAsync(user.Id);
            return user;

        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _authRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            user.ResetTokenExpires = DateTime.UtcNow.AddMinutes(5);

            await _authRepository.UpdateAsync(user);
            return token;
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _authRepository.GetByEmailAsync(email);

            if (user == null || user.PasswordResetToken != token || user.ResetTokenExpires < DateTime.UtcNow)
            {
                return false;
            }

            user.ResetTokenExpires = null;
            user.PasswordResetToken = null;
            user.PasswordHash = _passwordHasher.HashPassword(newPassword);
            await _authRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> ValidateResetTokenAsync(string email, string token)
        {
            var user = await _authRepository.GetByEmailAsync(email);
            return user != null && user.PasswordResetToken == token && user.ResetTokenExpires > DateTime.UtcNow;
        }

        private async Task SendWelcomeEmail(User user)
        {
            try
            {
                await _emailService.SendWelcomeEmailAsync(user.Email);
            }
            catch (Exception ex)
            {
                // Log email sending failure but don't fail registration
                Console.WriteLine($"Failed to send welcome email: {ex.Message}");
            }
        }

    }
}