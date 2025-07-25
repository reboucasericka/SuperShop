using SuperShop.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperShop.Data
{
	public class MockRepository : IRepository
	{
		public void AddProduct(Product product)
		{
			throw new System.NotImplementedException();
		}

		public Product GetProduct(int id)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<Product> GetProducts()
		{
			var products = new List<Product>();
			products.Add(new Product { Id = 1, Name = "um", Price = 10 });
			products.Add(new Product { Id = 2, Name = "dois", Price = 20 });
			products.Add(new Product { Id = 3, Name = "tres", Price = 30 });
			products.Add(new Product { Id = 4, Name = "quatro", Price = 40 });
			products.Add(new Product { Id = 5, Name = "cinco", Price = 50 });
			products.Add(new Product { Id = 6, Name = "seis", Price = 60 });
			products.Add(new Product { Id = 7, Name = "sete", Price = 70 });
			products.Add(new Product { Id = 8, Name = "oito", Price = 80 });
			products.Add(new Product { Id = 9, Name = "nove", Price = 90 });
			products.Add(new Product { Id = 10, Name = "dez", Price = 100 });
			return products;
		}

		public bool ProductExists(int id)
		{
			throw new System.NotImplementedException();
		}

		public void RemoveProduct(Product product)
		{
			throw new System.NotImplementedException();
		}

		public Task<bool> SaveAllAsync()
		{
			throw new System.NotImplementedException();
		}

		public void UpdateProduct(Product product)
		{
			throw new System.NotImplementedException();
		}
	}
}
