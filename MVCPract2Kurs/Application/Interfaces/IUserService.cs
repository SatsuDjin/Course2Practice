using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<bool> ChangePasswordAsync(string email, string newPassword);
    }
}
