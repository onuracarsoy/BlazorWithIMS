using IMS.WebApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModelsValidations
{
    public class Produce_EnsureEnoughInventoryQuantity : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var produceViewModel = validationContext.ObjectInstance as ProduceViewModel;
            if (produceViewModel != null)
            {
                if (produceViewModel.Product is not null && produceViewModel.Product.ProductInventories is not null)
                {
                    foreach (var item in produceViewModel.Product.ProductInventories)
                    {
                        if (item.Inventory is not null &&
                            item.InventoryQuantity * produceViewModel.QuantityToProduce > item.Inventory.InventoryQuantity)
                        {
                            return new ValidationResult($"Seçtiğiniz stok/stoklar {item.Inventory.InventoryName} üretmeniz için yeterli sayıda değil!",
                                new List<string>
                                {
                                    validationContext.MemberName
                                });
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
