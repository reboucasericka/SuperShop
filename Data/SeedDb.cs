using SuperShop.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
	public class SeedDb
	{
		private readonly DataContext _context;
		private Random  _random;

		public SeedDb(DataContext context)
		{
			_context = context;
			_random = new Random();
		}
		public async Task SeedAsync()
		{
			await _context.Database.EnsureCreatedAsync(); // Ensure the database is created

			if (!_context.Products.Any()) // Check if there are no products in the database
			{
				AddProduct(" iPhone X");
				AddProduct(" iPhone X");
				AddProduct("Magic Mouse");	
				AddProduct("Magic Keyboard");
				AddProduct("MacBook Pro 16");
				AddProduct("MacBook Pro 14");
				AddProduct("MacBook Air");
				AddProduct("iPad Pro 12.9");
				AddProduct("iPad Air");
				AddProduct("iPad Mini");
				await _context.SaveChangesAsync(); // Save changes to the database
			}			
		}

		private async Task AddProduct(string name)
		{
			_context.Products.Add(new Product
			{
				Name = name,
				Price = _random.Next(1000),
				IsAvailable = true, 
				Stock = _random.Next(1, 100), // Random stock between 1 and 100
			});
		}
	}
}
