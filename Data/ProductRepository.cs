using SuperShop.Data.Entities;
using System.Linq;

namespace SuperShop.Data
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(DataContext context) : base(context) //context vai entrar no construtor da classe base GenericRepository
		{
			//public IQueryable<Product> GetAllOrderedByName() // para ordenar os produtos por nome
			//{
			//	return base.GetAll().OrderBy(p => p.Name); // ou Price, etc.
			//}
		}
	}
}
