using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.InMemory
{
    public class ProductRepository : IProductRepository
    {

        private List<Product> _product;

        public ProductRepository()
        {
            _product = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Product 1", ProductPrice = 10.0, ProductQuantity = 10 },
                new Product { ProductId = 2, ProductName = "Product 2", ProductPrice = 20.0, ProductQuantity = 20 },
                new Product { ProductId = 3, ProductName = "Product 3", ProductPrice = 30.0, ProductQuantity = 30 },
                new Product { ProductId = 4, ProductName = "Product 4", ProductPrice = 40.0, ProductQuantity = 40 },
                new Product { ProductId = 5, ProductName = "Product 5", ProductPrice = 50.0, ProductQuantity = 50 }


            };
        }

        public Task AddProductAsync(Product product)
        {
            if (_product.Any(x => x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
            { return Task.CompletedTask; }

            var maxId = _product.Max(x => x.ProductId);
            product.ProductId = maxId + 1;

            _product.Add(product);

            return Task.CompletedTask;
        }

        public Task UpdateProductAsync(Product product)
        {
            if (_product.Any(x => x.ProductId != product.ProductId && x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;

            var incTopUpdate = _product.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (incTopUpdate != null)
            {
                incTopUpdate.ProductName = product.ProductName;
                incTopUpdate.ProductPrice = product.ProductPrice;
                incTopUpdate.ProductQuantity = product.ProductQuantity;
                incTopUpdate.ProductInventories = product.ProductInventories;
            }

            return Task.CompletedTask;

        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return await Task.FromResult(_product);
            }

            return _product.Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));


        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            //return await Task.FromResult(_product.FirstOrDefault(x => x.ProductId == productId));
            var prod = _product.FirstOrDefault(x => x.ProductId == productId);
            var newProd = new Product();
            if (prod != null)
            {
                newProd.ProductId = prod.ProductId;
                newProd.ProductName = prod.ProductName;
                newProd.ProductPrice = prod.ProductPrice;
                newProd.ProductQuantity = prod.ProductQuantity;
                newProd.ProductInventories = new List<ProductInventory>();
                if (prod.ProductInventories != null && prod.ProductInventories.Count > 0)
                {
                    foreach (var item in prod.ProductInventories)
                    {
                        var newProdInv = new ProductInventory
                        {
                            InventoryId = item.InventoryId,
                            ProductId = item.ProductId,
                            Product = prod,
                            Inventory = new Inventory(),
                            InventoryQuantity = item.InventoryQuantity
                           
                        };
                        if (item.Inventory is not null)
                        {
                            newProdInv.Inventory.InventoryId = item.Inventory.InventoryId;
                            newProdInv.Inventory.InventoryName = item.Inventory.InventoryName;
                            newProdInv.Inventory.InventoryPrice = item.Inventory.InventoryPrice;
                            newProdInv.Inventory.InventoryQuantity = item.Inventory.InventoryQuantity;

                        }
                        newProd.ProductInventories.Add(newProdInv);
                    }
                }
            }
            return await Task.FromResult(newProd);
        }


        public Task DeleteProductByIdAsync(int productId)
        {
            var product = _product.FirstOrDefault(x => x.ProductId == productId);
            if (product is not null)
            {
                _product.Remove(product);
            }

            return Task.CompletedTask;
        }

        
    }
}
