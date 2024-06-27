using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;
using Application.Interfaces;
using Infrastructure.Data;
using Domain.Entities;
using Application.DTOs;

namespace MVCPract2Kurs.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [RedirectIfAuthenticated]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectIfAuthenticated]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.RegisterUserAsync(model))
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email уже существует.");
                }
            }
            return View(model);
        }

        [HttpGet]
        [RedirectIfAuthenticated]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectIfAuthenticated]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.LoginAsync(model))
                {
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверная попытка входа.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            var model = new ApplicationUser
            {
                FullName = user.FullName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                ResetPasswordQuestion = user.ResetPasswordQuestion,
                ResetPasswordAnswer = user.ResetPasswordAnswer
            };

            return View(model);
        }

        [HttpGet]
        [RedirectIfAuthenticated]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectIfAuthenticated]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Пользователь с таким email не найден.");
                    return View(model);
                }

                return RedirectToAction(nameof(VerifyResetCode), new { email = user.Email });
            }

            return View(model);
        }

        [HttpGet]
        [RedirectIfAuthenticated]
        public async Task<IActionResult> VerifyResetCode(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ResetPasswordViewModel
            {
                Email = email,
                ResetPasswordQuestion = user.ResetPasswordQuestion
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RedirectIfAuthenticated]
        public async Task<IActionResult> VerifyResetCode(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound();
                }

                if (user.ResetPasswordAnswer != model.ResetPasswordAnswer)
                {
                    ModelState.AddModelError(string.Empty, "Неверный ответ на вопрос восстановления.");
                    return View(model);
                }

                await _userService.ChangePasswordAsync(model.Email, model.NewPassword);

                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }
    }
}
