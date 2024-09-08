using IMS.WebApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModelsValidations
{
    public class Sell_EnsureEnoughProductQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var sellViewModel = validationContext.ObjectInstance as SellViewModel;
            if (sellViewModel != null)
            {
                if (sellViewModel.Product is not null)
                {
                    if (sellViewModel.QuantityToSell > sellViewModel.Product.ProductQuantity)
                    {
                        return new ValidationResult($"Seçtiğiniz ürün/ürünler satış yapmnaız için yeterli sayıda değil! Maksimum {sellViewModel.Product.ProductName} için {sellViewModel.Product.ProductQuantity} ürün satabilirsniz.",
                                   new[] { validationContext.MemberName });
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
