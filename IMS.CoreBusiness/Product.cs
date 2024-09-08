using IMS.CoreBusiness.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Ad alanını doldurunuz.")]
        [StringLength(150, ErrorMessage = "Ad uzunluğu en fazla 150 olabilir.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Miktar alanını doldurunuz.")]
        [Range(0, int.MaxValue, ErrorMessage = "Miktar en az 0 olabilir.")]
        public int ProductQuantity { get; set; }

        [Required(ErrorMessage = "Fiyat alanını doldurunuz.")]
        [Range(0, int.MaxValue, ErrorMessage = "Fiyat en az 0 olabilir.")]
        [Product_EnsurePriceIsGreaterThanInventoriesCost]
        public double ProductPrice { get; set; }

        
        public List<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

        public void AddInventory(Inventory inventory)
        {
            if (!ProductInventories.Any(x => x.Inventory is not null && x.Inventory.InventoryName.Equals(inventory.InventoryName)))
            {
                ProductInventories.Add(new ProductInventory
                {
                    InventoryId = inventory.InventoryId,
                    Inventory = inventory,
                    InventoryQuantity = 1,
                    ProductId = ProductId,
                    Product = this
                });

            }

        }

        public void RemoveInventory(ProductInventory productInventory)
        {
            var result  = ProductInventories.FirstOrDefault(x => x.Inventory is not null && x.Inventory.InventoryName.Equals(productInventory.Inventory.InventoryName));
            if (result is not null)
            {
                ProductInventories.Remove(productInventory);
            }
        }
    }
}
