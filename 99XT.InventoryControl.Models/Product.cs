using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _99XT.InventoryControl.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        public Int64 ProductId { get; set; }

        [Required]
        [Column("name")]
        [Display(Name ="Product Name")]
        public string Name { get; set; }

        [Column("available_units")]
        [Display(Name = "Available Units")]
        public int AvailableUnits { get; set; } = 0;

        [Column("reorder_level")]
        [Display(Name = "Reorder Level")]
        public int ReorderLevel { get; set; } = 10;

        [Required]
        [Column("unit_price")]
        [Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }

        [Column("date_added", TypeName = "date")]
        [Display(Name = "Date Added")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateAdded { get; set; }

        [Column("date_modified", TypeName = "date")]
        [Display(Name = "Date Modified")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateModified { get; set; }

        [Display(Name = "Supplier")]
        public long SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}
