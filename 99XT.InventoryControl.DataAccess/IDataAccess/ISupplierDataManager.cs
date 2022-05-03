using _99XT.InventoryControl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _99XT.InventoryControl.DataAccess.IDataAccess
{
    public interface ISupplierDataManager
    {
        Task<List<Supplier>> GetAllSuppliers();
        Task<Supplier> GetSupplierById(long id);
        Task<Supplier> AddSupplier(Supplier supplier);
        Task<Supplier> UpdateSupplier(Supplier supplier);
        Task<bool> DeleteSupplier(long id);
        bool DoesSupplierExist(long id);
    }
}
