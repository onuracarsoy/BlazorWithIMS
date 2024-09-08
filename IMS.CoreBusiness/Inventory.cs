using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace IMS.CoreBusiness
{
    public class Inventory
    {
        public int InventoryId { get; set; }

        [Required(ErrorMessage ="Ad alanını doldurunuz.")]
        [StringLength(150, ErrorMessage ="Ad uzunluğu en fazla 150 olabilir.")]
        public string InventoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Miktar alanını doldurunuz.")]
        [Range(0, int.MaxValue, ErrorMessage="Miktar en az 0 olabilir.")]
        public int InventoryQuantity { get; set; }

        [Required(ErrorMessage = "Fiyat alanını doldurunuz.")]
        [Range(0, int.MaxValue, ErrorMessage = "Fiyat en az 0 olabilir.")]
        public double InventoryPrice { get; set; }

        public List<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
    }
}
