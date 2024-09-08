using IMS.CoreBusiness;
using IMS.WebApp.ViewModelsValidations;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class SellViewModel
    {
        [Required(ErrorMessage = "Satış Yapma No alanını doldurunuz.")]
        public string SalesOrderNumber { get; set; } = string.Empty;

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Ürün seçiniz.")]
        public int ProductId { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Miktar en az 1 olabilir.")]
        [Sell_EnsureEnoughProductQuantity]
        public int QuantityToSell { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Fiyat en az 0 olabilir.")]
        public double UnitPrice { get; set; }

        public Product? Product { get; set; }
    }
}
