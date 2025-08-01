using SuperShop.Data.Entities;
using System.Linq;

namespace SuperShop.Data
{
	public interface IProductRepository :IGenericRepository<Product>
	{
		//IQueryable<Product> GetAllOrderedByName(); // para ordenar os produtos por nome
	}
}
