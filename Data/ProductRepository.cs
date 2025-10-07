using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using System.Collections.Generic;
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

        public IEnumerable<SelectListItem> GetComboProducts() //for item  uma lista com nome e id  construir a lista
        {
			var list = _context.Products.Select(p => new SelectListItem
			{
				Text = p.Name,
				Value = p.Id.ToString(),
			}).ToList();

			list.Insert(0, new SelectListItem
			{
				Text = "(Select a product...)",
				Value = "0"
			});
			return list;


        }
    }
}
