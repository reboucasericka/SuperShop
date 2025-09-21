using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data;
using SuperShop.Helpers;
using SuperShop.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{

    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
		private readonly IUserHelper _userHelper;
		//private readonly IImageHelper _imageHelper;
        private readonly IBlobHelper _blobHelper;
		private readonly IConverterHelper _converterHelper;

		public ProductsController(
            IProductRepository productRepository, 
            IUserHelper userHelper, 
            //IImageHelper imageHelper,
            IBlobHelper blobHelper,
			IConverterHelper converterHelper)
        {
            _productRepository = productRepository;
			_userHelper = userHelper;
			//_imageHelper = ImageHelper;
            _blobHelper = blobHelper;
			_converterHelper = converterHelper;
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
                Guid imageId = Guid.Empty; // Initialize imageId to an empty GUID
               
                if (model.ImageFile != null && model.ImageFile.Length > 0) // Check if the ImageFile is not null
                {
					imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
				}
                var product = _converterHelper.ToProduct(model, imageId, true);


				
				//antes de gravar o produto, vamos associar o usuario que esta logado
				product.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name); // Get the user by email from the UserHelper
				await _productRepository.CreateAsync(product);          // CreateAsync is an async method that adds the product to the database
                return RedirectToAction(nameof(Index));
            }
            return View(model);
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
            
            var model = _converterHelper.ToProductViewModel(product);   
			return View(model);
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
                    //var path = model.ImageUrl; // Initialize the path variable to the current image URL
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0) // Check if the ImageFile is not null
                    {						
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
                    }

                    var product = _converterHelper.ToProduct(model, imageId, false);


					
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