using SuperShop.Data.Entities;
using System.Linq;

namespace SuperShop.Data
{
	//**********************
	//INTERFACE
	//**********************
	public interface IProductRepository :IGenericRepository<Product>
	{
		public IQueryable GetAllWithUsers(); //METODO NOVO

	}
}
