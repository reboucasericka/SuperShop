using Microsoft.AspNetCore.Mvc.Rendering;
using SuperShop.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SuperShop.Data
{
	//**********************
	//INTERFACE
	//**********************
	public interface IProductRepository :IGenericRepository<Product>
	{
		public IQueryable GetAllWithUsers(); //METODO NOVO

		IEnumerable<SelectListItem> GetComboProducts();

	}
}
