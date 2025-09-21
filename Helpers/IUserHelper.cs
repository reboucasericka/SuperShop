using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Entities;
using SuperShop.Models;
using System.Threading.Tasks;

namespace SuperShop.Helpers
{
	public interface IUserHelper
	{
		Task<User> GetUserByEmailAsync(string email);

		Task<IdentityResult> AddUserAsync(User user, string Password);

		Task<SignInResult> LoginAsync(LoginViewModel model);

		Task LogoutAsync();

        //2 metodos para mudar a password


		Task<IdentityResult> UpdateUserAsync(User user);



        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);
    }
}
