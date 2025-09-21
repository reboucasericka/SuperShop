using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Entities;
using SuperShop.Models;
using System.Threading.Tasks;

namespace SuperShop.Helpers
{
	public class UserHelper : IUserHelper
	{
		private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
            _signInManager = signInManager;
        }

		public async Task<IdentityResult> AddUserAsync(User user, string Password)
		{
			return await _userManager.CreateAsync(user, Password); //bypassing the password to create the user
		}

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<User> GetUserByEmailAsync(string email)
		{
			return await _userManager.FindByEmailAsync(email); //bypassing the email to get the user
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
