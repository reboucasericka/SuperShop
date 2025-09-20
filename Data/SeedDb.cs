using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
	public class SeedDb
	{
		private readonly DataContext _context;
		private readonly IUserHelper _userHelper;
		//private readonly UserManager<User> _userManager;
		private Random _random;

		public SeedDb(DataContext context, IUserHelper userHelper)
		{
			_context = context;
			_userHelper = userHelper;
			//_userManager = userManager;
			_random = new Random();
		}
		public async Task SeedAsync()
		{
			//verifica se existe a bd se nao existir cria a bd
			await _context.Database.EnsureCreatedAsync(); // Ensure the database is created
			//verifica se esse usuario existe se nao existir cria o usuario
			var user = await _userHelper.GetUserByEmailAsync("reboucasericka@gmail.com");
			if (user == null) // se o usuario nao existir ele cria o usuario novo
			{
				user = new User
				{
					FirstName = "ericka",
					LastName = "reboucas",
					Email = "reboucasericka@gmail.com",
					UserName = "reboucasericka",
					PhoneNumber = "000000000"
				};
				var result = await _userHelper.AddUserAsync(user, "123456"); // Create the user with a password
				if (result != IdentityResult.Success) // Check if the user was created successfully
				{
					throw new InvalidOperationException("Could not create the user in seed");
				}
			}
			if (!_context.Products.Any()) // Check if there are no products in the database
		    {
				AddProduct(" iPhone X", user);
				AddProduct(" iPhone X", user);
				AddProduct("Magic Mouse", user);
				AddProduct("Magic Keyboard", user);
				AddProduct("MacBook Pro 16", user);
				AddProduct("MacBook Pro 14", user);
				AddProduct("MacBook Air", user);
				AddProduct("iPad Pro 12.9", user);
				AddProduct("iPad Air", user);
				AddProduct("iPad Mini", user);
				await _context.SaveChangesAsync(); // Save changes to the database
			}
		}
			private void AddProduct(string name, User user)
			{
				_context.Products.Add(new Product
				{
					Name = name,
					Price = _random.Next(1000),
					IsAvailable = true,
					Stock = _random.Next(1, 100), // Random stock between 1 and 100
					User = user// Associate the product with the user
				});
			}
	}
}
