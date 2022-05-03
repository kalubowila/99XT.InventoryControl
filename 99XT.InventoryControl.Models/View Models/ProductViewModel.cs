using System;
using System.Collections.Generic;
using System.Text;

namespace _99XT.InventoryControl.Models.View_Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Supplier> AllSuppliers { get; set; }
    }
}
