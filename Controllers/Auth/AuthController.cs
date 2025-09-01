using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs.Auth;
using TaskManagement.Models;
using TaskManagement.Services.Interfaces;
using TaskManagement.Utilities.EmailServices;
using TaskManagement.Models.ViewModels;

namespace TaskManagement.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IMapper mapper, IEmailService emailService)
        {
            _authService = authService;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var loginDto = _mapper.Map<LoginDTO>(viewModel);
            var user = await _authService.LoginAsync(loginDto);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(viewModel);
            }

            await SignInUser(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var registerDto = _mapper.Map<RegisterDTO>(viewModel);
            var result = await _authService.RegisterAsync(registerDto);

            if (!result)
            {
                ModelState.AddModelError("", "Email already exists");
                return View(viewModel);
            }

            TempData["SuccessMessage"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var token = await _authService.GeneratePasswordResetTokenAsync(email);
            if (token == null)
            {
                // Don't reveal that the email doesn't exist
                TempData["Message"] = "If the email exists, a reset link has been sent.";
                return RedirectToAction("Login");
            }

            var resetLink = Url.Action("ResetPassword", "Auth", new { email, token }, Request.Scheme);
            await _emailService.SendPasswordResetEmailAsync(email, resetLink);

            TempData["Message"] = "Password reset link has been sent to your email.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            var viewModel = new ChangePasswordVM { Email = email, Token = token };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ChangePasswordVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var isValid = await _authService.ValidateResetTokenAsync(viewModel.Email, viewModel.Token);
            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid or expired reset token");
                return View(viewModel);
            }

            var success = await _authService.ResetPasswordAsync(viewModel.Email, viewModel.Token, viewModel.NewPassword);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to reset password");
                return View(viewModel);
            }

            TempData["SuccessMessage"] = "Password reset successfully! Please login.";
            return RedirectToAction("Login");
        }

        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.Username}"),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = true
            });
        }
    }
}