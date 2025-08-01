using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShop.Data;

namespace SuperShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : Controller
	{
		private readonly IProductRepository _productRepository;

		//constructor to inject the product repository
		public ProductsController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		// GET: api/products
		[HttpGet]
		public IActionResult GetAllProducts()
		{
			return Ok(_productRepository.GetAllWithUsers());//vai buscar todos os produtos atraves do repoditorio e o ok embrulha tudo dentro do json e manda e testar no post 
			//PRIMEIRO NO INTERFACE DEPOIS NA CLASSE E NO CONTROLADOR E SE FOR PRECISO A VIEWS
		}
	}
}
