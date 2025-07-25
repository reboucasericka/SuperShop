using SuperShop.Data.Entities;

namespace SuperShop.Data
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(DataContext context) : base(context) //context vai entrar no construtor da classe base GenericRepository
		{
			// Constructor that passes the DataContext to the base class
			// This allows the ProductRepository to use the methods defined in GenericRepository
		}
	}
}
