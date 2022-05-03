using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace _99XT.InventoryControl.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("supplier_id")]
        public long SupplierId { get; set; }

        [Required]
        [Column("name")]
        [Display(Name = "Supplier Name")]
        public string Name { get; set; }

        [Required]
        [Column("address_line_1")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Column("address_line_2")]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Column("telephone")]
        [MaxLength(15)]
        public string Telephone { get; set; }

        [Column("date_added", TypeName = "date")]
        [Display(Name = "Date Added")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateAdded { get; set; }

        [Column("date_modified", TypeName = "date")]
        [Display(Name = "Date Modified")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateModified { get; set; }

        [ForeignKey("SupplierId")]
        [Display(Name = "Supply Products")]
        public List<Product> SupplyProducts { get; set; }
    }
}
