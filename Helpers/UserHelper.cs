﻿using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Entities;
using SuperShop.Models;
using System.Threading.Tasks;

namespace SuperShop.Helpers
{
	public class UserHelper : IUserHelper
	{
		private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; //gestao de login
        private readonly RoleManager<IdentityRole> _roleManager; //gestao de roles

        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

		public async Task<IdentityResult> AddUserAsync(User user, string Password)
		{
			return await _userManager.CreateAsync(user, Password); //bypassing the password to create the user
		}

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
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

        public async Task<User> GetUserByEmailAsync(string email)
		{
			return await _userManager.FindByEmailAsync(email); //bypassing the email to get the user
		}

        public async Task<bool> IsUserInRoleAsync(User user, string roleName) //verifica se o user esta na role sim ou nao
        {
            return await _userManager.IsInRoleAsync(user, roleName);//verifica se o user esta na role retorna um boolean
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateUserAsync(User user) //update do user
        {
            return await _userManager.UpdateAsync(user);//faz o update do user
        }
    }
}
