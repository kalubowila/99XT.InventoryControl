using _99XT.InventoryControl.DataAccess.IDataAccess;
using _99XT.InventoryControl.Models;
using _99XT.InventoryControl.Models.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _99XT.InventoryControl.DataAccess.DataAccess
{
    public class SupplierDataManager : ISupplierDataManager
    {
        private readonly InventoryContext _inventoryContext;
        public SupplierDataManager(InventoryContext context)
        {
            _inventoryContext = context;
        }

        public async Task<List<Supplier>> GetAllSuppliers()
        {
            return await _inventoryContext.Suppliers.Include(o => o.SupplyProducts).ToListAsync();
        }
        public async Task<Supplier> GetSupplierById(long id)
        {
            return await _inventoryContext.Suppliers.Include(o => o.SupplyProducts).FirstOrDefaultAsync(m => m.SupplierId == id);
        }
        public async Task<Supplier> AddSupplier(Supplier supplier)
        {
            _inventoryContext.Add(supplier);
            await _inventoryContext.SaveChangesAsync();
            return supplier;
        }
        public async Task<Supplier> UpdateSupplier(Supplier supplier)
        {
            _inventoryContext.Update(supplier);
            await _inventoryContext.SaveChangesAsync();
            return supplier;
        }
        public async Task<bool> DeleteSupplier(long id)
        {
            var supplier = await _inventoryContext.Suppliers.FindAsync(id);
            _inventoryContext.Suppliers.Remove(supplier);
            await _inventoryContext.SaveChangesAsync();
            return true;
        }
        public bool DoesSupplierExist(long id)
        {
            return _inventoryContext.Suppliers.Any(e => e.SupplierId == id);
        }
    }
}
