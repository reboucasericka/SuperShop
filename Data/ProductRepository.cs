using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using System.Linq;

namespace SuperShop.Data
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly DataContext _context;

		public ProductRepository(DataContext context) : base(context) //context vai entrar no construtor da classe base GenericRepository
		{
			_context = context;
		}


		public IQueryable GetAllWithUsers() // Method to get all products with their associated users
		{
			return _context.Products.Include(p => p.User); //INCLUIR 
		}
		
	}
}
