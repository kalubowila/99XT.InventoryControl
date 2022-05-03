using _99XT.InventoryControl.Core.ICore;
using _99XT.InventoryControl.DataAccess.IDataAccess;
using _99XT.InventoryControl.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace _99XT.InventoryControl.Core.Core
{
    public class InventoryCore : IInventoryCore
    {
        private readonly IInventoryDataManager _inventoryDataManager;
        public InventoryCore(IInventoryDataManager _dataManager)
        {
            _inventoryDataManager = _dataManager;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _inventoryDataManager.GetAllProducts();
        }

        public async Task<Product> GetProductById(long id)
        {
            return await _inventoryDataManager.GetProductById(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            product.DateModified = DateTime.Now.Date;
            return await _inventoryDataManager.AddProduct(product);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            product.DateModified = DateTime.Now.Date;
            return await _inventoryDataManager.UpdateProduct(product);
        }

        public async Task<bool> DeleteProduct(long id)
        {
            return await _inventoryDataManager.DeleteProduct(id);
        }

        public bool DoesProductExist(long id)
        {
            return _inventoryDataManager.DoesProductExist(id);
        }
    }
}
