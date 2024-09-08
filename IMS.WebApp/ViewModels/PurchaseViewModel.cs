using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class PurchaseViewModel
    {
        [Required(ErrorMessage="Satın Alım No alanını doldurunuz.")]
        public string PONumber { get; set; } = string.Empty;

        [Range(minimum:1, maximum:int.MaxValue, ErrorMessage ="Malzeme seçiniz.")]
        public int InventoryId { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Miktar en az 1 olabilir.")]
        public int QuantityToPurchase { get; set; }

     
        public double InventoryPrice { get; set; }
    }
}
