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
    public class InventoryDataManager : IInventoryDataManager
    {
        private readonly InventoryContext _inventoryContext;
        public InventoryDataManager(InventoryContext context)
        {
            _inventoryContext = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _inventoryContext.Products.Include(o => o.Supplier).ToListAsync();
        }

        public async Task<Product> GetProductById(long id)
        {
            return await _inventoryContext.Products.Include(o => o.Supplier).FirstOrDefaultAsync(m => m.ProductId == id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            _inventoryContext.Add(product);
            await _inventoryContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _inventoryContext.Update(product);
            await _inventoryContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(long id)
        {
            var product = await _inventoryContext.Products.FindAsync(id);
            _inventoryContext.Products.Remove(product);
            await _inventoryContext.SaveChangesAsync();
            return true;
        }

        public bool DoesProductExist(long id)
        {
            return _inventoryContext.Products.Any(e => e.ProductId == id);
        }
    }
}
