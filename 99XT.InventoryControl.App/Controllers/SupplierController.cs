using _99XT.InventoryControl.Core.ICore;
using _99XT.InventoryControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace _99XT.InventoryControl.App.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly ISupplierCore _supplierCore;

        public SupplierController(ISupplierCore _core)
        {
            _supplierCore = _core;
        }

        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            return View(await _supplierCore.GetAllSuppliers());
        }

        // GET: Supplier/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var supplier = await _supplierCore.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Supplier/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("SupplierId,Name,AddressLine1,AddressLine2,Telephone,DateAdded,DateModified")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierCore.AddSupplier(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var supplier = await _supplierCore.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SupplierId,Name,AddressLine1,AddressLine2,Telephone,DateAdded,DateModified")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _supplierCore.UpdateSupplier(supplier);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.SupplierId))
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
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            var supplier = await _supplierCore.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _supplierCore.DeleteSupplier(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(long id)
        {
            return _supplierCore.DoesSupplierExist(id);
        }
    }
}
