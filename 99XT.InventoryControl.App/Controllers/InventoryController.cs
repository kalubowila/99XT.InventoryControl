using _99XT.InventoryControl.Core.ICore;
using _99XT.InventoryControl.LoggerService.ILoggerCore;
using _99XT.InventoryControl.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace _99XT.InventoryControl.App.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IInventoryCore _inventoryCore;
        private readonly ISupplierCore _supplierCore;
        private ILoggerManager _logger;

        public InventoryController(IInventoryCore inventoryCore, ISupplierCore supplierCore, ILoggerManager logger)
        {
            _inventoryCore = inventoryCore;
            _supplierCore = supplierCore;
            _logger = logger;
        }

        // GET: Inventory
        public async Task<IActionResult> Index()
        {
            return View(await _inventoryCore.GetAllProducts());
        }

        // GET: Inventory/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var product = await _inventoryCore.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Inventory/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ProductViewModel viewModel = new ProductViewModel
            {
                AllSuppliers = await _supplierCore.GetAllSuppliers()
            };
            return View(viewModel);
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var savedProduct = await _inventoryCore.AddProduct(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel.Product);
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            ProductViewModel viewModel = new ProductViewModel
            {
                Product = await _inventoryCore.GetProductById(id),
                AllSuppliers = await _supplierCore.GetAllSuppliers()
            };
            if (viewModel.Product == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _inventoryCore.UpdateProduct(productViewModel.Product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(productViewModel.Product.ProductId))
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
            return View(productViewModel.Product);
        }

        // GET: Inventory/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await _inventoryCore.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await _inventoryCore.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception e)
            {

                throw;
            }

        }

        private bool ProductExists(long id)
        {
            return _inventoryCore.DoesProductExist(id);
        }

    }
}
