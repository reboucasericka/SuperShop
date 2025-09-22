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
        Task CheckRoleAsync(string roleName);//veriifica se a role existe se nao existir cria a role
        Task AddUserToRoleAsync(User user, string roleName);//adiciona o user a role
        Task<bool> IsUserInRoleAsync(User user, string roleName);//verifica se o user esta na role
    }
}
