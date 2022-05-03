using _99XT.InventoryControl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _99XT.InventoryControl.Core.ICore
{
    public interface IInventoryCore
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(long id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(long id);
        bool DoesProductExist(long id);
    }
}
