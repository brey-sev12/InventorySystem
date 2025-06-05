using InventorySystem.Models;
using InventorySystem.ViewModels; // For ProductFormViewModel
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace InventorySystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ISDBContext _context;

        public ProductController(ISDBContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Supplier) // eager load Supplier
                .ToListAsync();

            return View(products);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new ProductFormViewModel
            {
                Suppliers = await GetSuppliersSelectListAsync()
            };

            return View(viewModel);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    ProductCode = model.ProductCode,
                    ProductName = model.ProductName,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    SupplierID = model.SupplierID
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // If we reach this point, something failed; reload suppliers for the dropdown
            model.Suppliers = await GetSuppliersSelectListAsync();
            return View(model);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var viewModel = new ProductFormViewModel
            {
                ProductID = product.ProductID,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Quantity = product.Quantity,
                Price = product.Price,
                SupplierID = product.SupplierID,
                Suppliers = await GetSuppliersSelectListAsync()
            };

            return View(viewModel);
        }

        // POST: Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Suppliers = await GetSuppliersSelectListAsync();
                return View(model);
            }

            var product = await _context.Products.FindAsync(model.ProductID);
            if (product == null) return NotFound();

            product.ProductCode = model.ProductCode;
            product.ProductName = model.ProductName;
            product.Quantity = model.Quantity;
            product.Price = model.Price;
            product.SupplierID = model.SupplierID;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }

        // Helper method to get suppliers select list
        private async Task<List<SelectListItem>> GetSuppliersSelectListAsync()
        {
            return await _context.Suppliers
                .Select(s => new SelectListItem
                {
                    Value = s.SupplierID.ToString(),
                    Text = s.SupplierName
                }).ToListAsync();
        }
    }
}
