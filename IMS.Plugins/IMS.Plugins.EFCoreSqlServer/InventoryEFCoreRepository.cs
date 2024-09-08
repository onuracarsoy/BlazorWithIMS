using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class InventoryEFCoreRepository : IInventoryRepository
    {
        private readonly IDbContextFactory<IMSContext> _dbContextFactory;

        public InventoryEFCoreRepository(IDbContextFactory<IMSContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;

        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            using var db = this._dbContextFactory.CreateDbContext();
            db.Inventories?.Add(inventory);
            await db.SaveChangesAsync();
        }

        public async Task DeleteInventoryByIdAsync(int inventoryId)
        {
            using var db = this._dbContextFactory.CreateDbContext();
            var inventory = db.Inventories?.Find(inventoryId);
            if (inventory is null) return;

            db.Inventories?.Remove(inventory);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            using var db = this._dbContextFactory.CreateDbContext();
            return await db.Inventories.Where(x => x.InventoryName.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            using var db = this._dbContextFactory.CreateDbContext();
            var inventory = await db.Inventories.FindAsync(inventoryId);
            if (inventory is not null) return inventory;

            return new Inventory();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            using var db = this._dbContextFactory.CreateDbContext();
            var inv = await db.Inventories.FindAsync(inventory.InventoryId);
            if (inv is not null)
            {
                inv.InventoryName = inventory.InventoryName;
                inv.InventoryPrice = inventory.InventoryPrice;
                inv.InventoryQuantity = inventory.InventoryQuantity;

                await db.SaveChangesAsync();
            }
        }
    }
}

