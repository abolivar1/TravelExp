using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExp.Common.Enums;
using TravelExp.Web.Data;
using TravelExp.Web.Data.Entities;
using TravelExp.Web.Models;

namespace TravelExp.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly DataContext _context;

        public UserHelper(
            UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<Employee> signInManager,
            DataContext context)

        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Employee> AddUserAsync(AddUserViewModel model, string path, UserType userType)
        {
            Employee userEntity = new Employee
            {
                Address = model.Address,
                Document = model.Document,
                Email = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PicturePath = path,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username,
                UserType = userType
            };

            IdentityResult result = await _userManager.CreateAsync(userEntity, model.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            Employee newUser = await GetUserByEmailAsync(model.Username);
            await AddUserToRoleAsync(newUser, userEntity.UserType.ToString());
            return newUser;
        }


        public async Task<IdentityResult> AddUserAsync(Employee user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(Employee user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<Employee> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .Include(u => u.Trips)
                .ThenInclude(t => t.TripDetails)
                .ThenInclude(td => td.ExpenseType)
                .Include(u => u.Trips)
                .ThenInclude(t => t.City)
                .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(u => u.Email.Equals(email));

            return user;
        }

        public async Task<bool> IsUserInRoleAsync(Employee user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null)
            {
                return true;
            }

            var response = await _userManager.DeleteAsync(user);
            return response.Succeeded;
        }
        public async Task<IdentityResult> UpdateUserAsync(Employee user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(Employee user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);
        }

        public async Task<IdentityResult> ChangePasswordAsync(Employee user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(Employee user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Employee user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<Employee> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Trips)
                .ThenInclude(t => t.TripDetails)
                .ThenInclude(td => td.ExpenseType)
                .Include(u => u.Trips)
                .ThenInclude(t => t.City)
                .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(u => u.Id.Equals(userId));

            return user;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Employee user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(Employee user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);

        }

    }
}
