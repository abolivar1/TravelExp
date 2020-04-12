using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExp.Common.Enums;
using TravelExp.Web.Data.Entities;
using TravelExp.Web.Models;

namespace TravelExp.Web.Helpers
{
    public interface IUserHelper
    {
        Task<Employee> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(Employee user, string password);

        Task<Employee> AddUserAsync(AddUserViewModel model, string path, UserType userType);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(Employee user, string roleName);

        Task<bool> IsUserInRoleAsync(Employee user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<bool> DeleteUserAsync(string email);

        Task<IdentityResult> UpdateUserAsync(Employee user);

        Task<SignInResult> ValidatePasswordAsync(Employee user, string password);

        Task<IdentityResult> ChangePasswordAsync(Employee user, string oldPassword, string newPassword);

        Task<string> GenerateEmailConfirmationTokenAsync(Employee user);

        Task<IdentityResult> ConfirmEmailAsync(Employee user, string token);

        Task<Employee> GetUserByIdAsync(string userId);

        Task<string> GeneratePasswordResetTokenAsync(Employee user);

        Task<IdentityResult> ResetPasswordAsync(Employee user, string token, string password);

    }
}
