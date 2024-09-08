using IMS.CoreBusiness;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Activities
{
    public class SellProductUseCase : ISellProductUseCase
    {
        private readonly IProductTransactionRepository _productTransactionRepository;
        private readonly IProductRepository _productRepository;

        public SellProductUseCase(IProductTransactionRepository productTransactionRepository,
           IProductRepository productRepository)
        {
            this._productTransactionRepository = productTransactionRepository;
            this._productRepository = productRepository;
        }


        public async Task ExecuteAsync(string salesOrderNumber, Product product, int quantity,double unitPrice, string doneby)
        {
            await _productTransactionRepository.SellProductAsync(salesOrderNumber, product, quantity, unitPrice, doneby);

            product.ProductQuantity -= quantity;
            await _productRepository.UpdateProductAsync(product);
        }
    }
}
