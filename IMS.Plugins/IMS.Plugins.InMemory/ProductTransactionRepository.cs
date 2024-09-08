using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.InMemory
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private List<ProductTransaction> _productTransactions = new List<ProductTransaction>();

        private readonly IProductRepository _productRepository;
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public ProductTransactionRepository(IProductRepository productRepository,
            IInventoryTransactionRepository inventoryTransactionRepository,
            IInventoryRepository inventoryRepository)
        {
            this._productRepository = productRepository;
            this._inventoryTransactionRepository = inventoryTransactionRepository;
            this._inventoryRepository = inventoryRepository;
        }

        public async Task ProduceAsync(string productionNumber, Product product,int quantity, string doneBy)
        {
            var prod = _productRepository.GetProductByIdAsync(product.ProductId).Result;

            if(prod is not null)
            {
                foreach(var item in prod.ProductInventories)
                {

                    if(item.Inventory is not null)
                    {

                        _inventoryTransactionRepository.ProduceAsync(productionNumber, item.Inventory, item.InventoryQuantity * quantity, doneBy, -1);

                    }
                    var inv = await _inventoryRepository.GetInventoryByIdAsync(item.InventoryId);
                    inv.InventoryQuantity -= item.InventoryQuantity * quantity;
                    await _inventoryRepository.UpdateInventoryAsync(inv);

                }
            }

            _productTransactions.Add(new ProductTransaction
            {
                ProductionNumber = productionNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.ProductQuantity,
                ActivityType = ProductTransactionType.ProduceProduct,
                QuantityAfter = product.ProductQuantity + quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy
               
            });
        }

        public Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string doneby)
        {
            _productTransactions.Add(new ProductTransaction
            {
                SONumber = salesOrderNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.ProductQuantity,
                ActivityType = ProductTransactionType.SellProduct,
                QuantityAfter = product.ProductQuantity - quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneby,
                UnitPrice = unitPrice
            });
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
        {
            var products = (await _productRepository.GetProductsByNameAsync(string.Empty)).ToList();

            var query = from it in this._productTransactions
                        join inv in products on it.ProductId equals inv.ProductId
                        where
                            (string.IsNullOrWhiteSpace(productName) || inv.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0)
                            &&
                            (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date) &&
                            (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) &&
                            (!transactionType.HasValue || it.ActivityType == transactionType)
                        select new ProductTransaction
                        {
                            Product = inv,
                            ProductTransactionId = it.ProductTransactionId,
                            SONumber = it.SONumber,
                            ProductionNumber = it.ProductionNumber,
                            ProductId = it.ProductId,
                            QuantityBefore = it.QuantityBefore,
                            ActivityType = it.ActivityType,
                            QuantityAfter = it.QuantityAfter,
                            TransactionDate = it.TransactionDate,
                            DoneBy = it.DoneBy,
                            UnitPrice = it.UnitPrice
                        };

            return query;
        }
    }
}
