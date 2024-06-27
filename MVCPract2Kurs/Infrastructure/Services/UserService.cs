using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;
using Application.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;


namespace Infrastructure.Services
{
    
        public class UserService : IUserService
        {
        private readonly ClinicContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ClinicContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterUserAsync(RegisterViewModel model)
            {
                if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                {
                    return false;
                }

                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FullName = model.FullName,
                    Email = model.Email,
                    PasswordHash = HashPassword(model.Password),
                    IsAdmin = false,
                    ResetPasswordQuestion = model.ResetPasswordQuestion,
                    ResetPasswordAnswer = model.ResetPasswordAnswer
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return true;
            }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !VerifyPassword(user.PasswordHash, model.Password))
            {
                return false;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return true;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
            {
                return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            }

            public async Task<bool> ChangePasswordAsync(string email, string newPassword)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return false;
                }

                user.PasswordHash = HashPassword(newPassword);
                _context.Update(user);
                await _context.SaveChangesAsync();

                return true;
            }

            private string HashPassword(string password)
            {
                byte[] salt = new byte[128 / 8];
                using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                return $"{Convert.ToBase64String(salt)}.{hashed}";
            }

            private bool VerifyPassword(string hashedPassword, string providedPassword)
            {
                string[] parts = hashedPassword.Split('.');
                if (parts.Length != 2)
                {
                    throw new FormatException("Неверный формат хешированного пароля.");
                }

                byte[] salt = Convert.FromBase64String(parts[0]);
                string hash = parts[1];

                string providedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: providedPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                return hash == providedHash;
                
            }
        }
    

}
