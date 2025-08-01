using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using SuperShop.Models;

namespace SuperShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
		private readonly IUserHelper _userHelper;

		public ProductsController(IProductRepository productRepository, IUserHelper userHelper)
        {
            _productRepository = productRepository;
			_userHelper = userHelper;
		}
        // GET: Products
        public IActionResult Index()
        {
			//var produtos = _productRepository.GetAllOrderedByName(); //para ordenar os produtos por nome
			//return View(produtos);
			return View(_productRepository.GetAll().OrderBy(p => p.Name));

        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
		// GET: Products/Create
		//CREATE é um método que retorna uma view para criar um novo produto
		//
		public IActionResult Create()
        {
            return View();
        }
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty; // Initialize the path variable to an empty string
                if (model.ImageFile != null && model.ImageFile.Length > 0) // Check if the ImageFile is not null
                {
                    var guid = Guid.NewGuid().ToString(); // Generate a new GUID for the image file name
                    var file = $"{guid}.jpg"; // Create a new file name with the GUID and .jpg extension

					path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", file); // Combine the current directory with the images folder and the file name
                    using (var stream = new FileStream(path, FileMode.Create)) // Create a new file stream to write the image file
                    {
                        await model.ImageFile.CopyToAsync(stream); // Copy the image file to the stream asynchronously
					}
                    path = $"~/images/products/{file}"; // Set the path to the image file in the wwwroot folder
				}
                var product = this.ToProduct(model, path); // Convert the ProductViewModel to a Product entity using the ToProduct extension method

				//TODO: MODIFICAR PARA O USER QUE ESTIVER LOGADO
				//antes de gravar o produto, vamos associar o usuario que esta logado
				product.User = await _userHelper.GetUserByEmailAsync(User.Identity.Name); // Get the user by email from the UserHelper
				await _productRepository.CreateAsync(product);          // CreateAsync is an async method that adds the product to the database
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

		private Product ToProduct(ProductViewModel model, string path)
		{
			return new Product
            {
                Id = model.Id,
				ImageUrl = path, // Set the ImageUrl to the path of the uploaded image
				IsAvailable = model.IsAvailable,
				LastPurchase = model.LastPurchase,
				LastSale = model.LastSale,
				Name = model.Name,
                Price = model.Price,      
                Stock = model.Stock,
                User = model.User // Assuming the User property is set in the ProductViewModel
			};
		}
        //******************************************************************************
		// GET: Products/Edit/5
		//EDIT é um método que retorna uma view para editar um produto existente
		//******************************************************************************
		public async Task<IActionResult> Edit(int? id)  // Edit is an async method that retrieves the product by id
		{
            if (id == null)
            {
                return NotFound();
            }
            var product = await _productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
			// ANTES DE RETORNAR A VIEW, PRECISAMOS CONVERTER O PRODUCT PARA O PRODUCTVIEWMODEL
            var model= this.ToProductViewModel(product); // Convert the Product entity to a ProductViewModel using the ToProductViewModel extension method
			return View(model);
        }

		private ProductViewModel ToProductViewModel(Product product)
		{
			return new ProductViewModel
            {
                Id = product.Id,
				IsAvailable = product.IsAvailable,
				LastPurchase = product.LastPurchase,
				LastSale = product.LastSale,
				ImageUrl = product.ImageUrl, // Set the ImageUrl to the path of the uploaded image                    
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = product.User // Assuming the User property is set in the Product entity
            };
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//igual a view do create, mas com o produto já preenchido
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model ) // Edit is an async method that updates the product in the database
		{
            
            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl; // Initialize the path variable to the current image URL
                    if (model.ImageFile != null && model.ImageFile.Length > 0) // Check if the ImageFile is not null
                    {
						var guid = Guid.NewGuid().ToString(); // Generate a new GUID for the image file name
						var file = $"{guid}.jpg"; // Create a new file name with the GUID and .jpg exten

						path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", file); // Combine the current directory with the images folder and the file name
                        using (var stream = new FileStream(path, FileMode.Create)) // Create a new file stream to write the image file
                        {
                            await model.ImageFile.CopyToAsync(stream); // Copy the image file to the stream asynchronously
                        }
                        path = $"~/images/products/{file}"; // Set the path to the image file in the wwwroot folder
                    }
                    var product = this.ToProduct(model, path); // Convert the ProductViewModel to a Product entity using the ToProduct extension method
					//antes de gravar o produto, vamos associar o usuario que esta logado
					product.User = await _userHelper.GetUserByEmailAsync(User.Identity.Name); // Get the user by email from the UserHelper
					await _productRepository.UpdateAsync(product); // UpdateAsync is an async method that updates the product in the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _productRepository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model );
        }
		//******************************************************************************
		// GET: Products/Delete/5
		//DELETE é um método que retorna uma view para confirmar a exclusão de um produto
		//******************************************************************************
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}