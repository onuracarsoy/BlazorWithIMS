using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryRepository : IInventoryRepository
    {

        private List<Inventory> _inventories;

        public InventoryRepository()
        {
            _inventories = new List<Inventory>
            {
                new Inventory { InventoryId = 1, InventoryName = "Inventory 1", InventoryPrice = 10.0, InventoryQuantity = 10 },
                new Inventory { InventoryId = 2, InventoryName = "Inventory 2", InventoryPrice = 20.0, InventoryQuantity = 20 },
                new Inventory { InventoryId = 3, InventoryName = "Inventory 3", InventoryPrice = 30.0, InventoryQuantity = 30 },
                new Inventory { InventoryId = 4, InventoryName = "Inventory 4", InventoryPrice = 40.0, InventoryQuantity = 40 },
                new Inventory { InventoryId = 5, InventoryName = "Inventory 5", InventoryPrice = 50.0, InventoryQuantity = 50 }


            };
        }

        public Task AddInventoryAsync(Inventory inventory)
        {
            if (_inventories.Any(x => x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
            { return Task.CompletedTask; }

            var maxId = _inventories.Max(x => x.InventoryId);
            inventory.InventoryId = maxId + 1;

            _inventories.Add(inventory);

            return Task.CompletedTask;
        }

        public Task UpdateInventoryAsync(Inventory inventory)
        {
            if (_inventories.Any(x => x.InventoryId != inventory.InventoryId && x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;

            var incTopUpdate = _inventories.FirstOrDefault(x => x.InventoryId == inventory.InventoryId);
            if (incTopUpdate != null)
            {
                incTopUpdate.InventoryName = inventory.InventoryName;
                incTopUpdate.InventoryPrice = inventory.InventoryPrice;
                incTopUpdate.InventoryQuantity = inventory.InventoryQuantity;
            }

            return Task.CompletedTask;

        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return await Task.FromResult(_inventories);
            }

            return _inventories.Where(x => x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase));


        }

        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            return await Task.FromResult(_inventories.First(x => x.InventoryId == inventoryId));
        }


        public  Task DeleteInventoryByIdAsync(int inventoryId)
        {
            var inventory = _inventories.FirstOrDefault(x => x.InventoryId == inventoryId);
            if (inventory is not null)
            {
                _inventories.Remove(inventory);
            }

            return Task.CompletedTask;
        }
    }
}
