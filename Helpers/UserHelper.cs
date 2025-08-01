using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Entities;
using System.Threading.Tasks;

namespace SuperShop.Helpers
{
	public class UserHelper : IUserHelper
	{
		private readonly UserManager<User> _userManager;

		public UserHelper(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IdentityResult> AddUserAsync(User user, string Password)
		{
			return await _userManager.CreateAsync(user, Password); //bypassing the password to create the user
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await _userManager.FindByEmailAsync(email); //bypassing the email to get the user
		}
	}
}
