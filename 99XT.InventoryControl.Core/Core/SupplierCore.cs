using _99XT.InventoryControl.Core.ICore;
using _99XT.InventoryControl.DataAccess.IDataAccess;
using _99XT.InventoryControl.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _99XT.InventoryControl.Core.Core
{
    public class SupplierCore : ISupplierCore
    {
        private readonly ISupplierDataManager _supplierDataManager;
        public SupplierCore(ISupplierDataManager _dataManager)
        {
            _supplierDataManager = _dataManager;
        }

        public async Task<List<Supplier>> GetAllSuppliers()
        {
            return await _supplierDataManager.GetAllSuppliers();
        }

        public async Task<Supplier> GetSupplierById(long id)
        {
            return await _supplierDataManager.GetSupplierById(id);
        }

        public async Task<Supplier> AddSupplier(Supplier supplier)
        {
            supplier.DateModified = DateTime.Now.Date;
            return await _supplierDataManager.AddSupplier(supplier);
        }

        public async Task<Supplier> UpdateSupplier(Supplier supplier)
        {
            supplier.DateModified = DateTime.Now.Date;
            return await _supplierDataManager.UpdateSupplier(supplier);
        }

        public async Task<bool> DeleteSupplier(long id)
        {
            return await _supplierDataManager.DeleteSupplier(id);
        }

        public bool DoesSupplierExist(long id)
        {
            return _supplierDataManager.DoesSupplierExist(id);
        }
    }
}
