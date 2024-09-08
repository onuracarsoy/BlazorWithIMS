using IMS.CoreBusiness;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IInventoryTransactionRepository
    {
       Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, double price);
       Task ProduceAsync(string productionNumber, Inventory inventory, int quantityConsume, string doneBy, double price);
        Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType);
    }
}