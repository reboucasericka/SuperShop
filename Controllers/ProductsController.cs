using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data;
using SuperShop.Data.Entities;
using SuperShop.Helpers;

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
        public IActionResult Create()
        {
            return View();
        }
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //TODO: MODIFICAR PARA O USER QUE ESTIVER LOGADO
				//antes de gravar o produto, vamos associar o usuario que esta logado
                product.User = await _userHelper.GetUserByEmailAsync(User.Identity.Name); // Get the user by email from the UserHelper
				await _productRepository.CreateAsync(product);          // CreateAsync is an async method that adds the product to the database
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        // GET: Products/Edit/5
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
            return View(product);
        }
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product) // Edit is an async method that updates the product in the database
		{
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
					//antes de gravar o produto, vamos associar o usuario que esta logado
					product.User = await _userHelper.GetUserByEmailAsync(User.Identity.Name); // Get the user by email from the UserHelper
					await _productRepository.UpdateAsync(product); // UpdateAsync is an async method that updates the product in the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _productRepository.ExistAsync(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
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