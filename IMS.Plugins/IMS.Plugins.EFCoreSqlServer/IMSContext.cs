using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class IMSContext : DbContext
    {
        public IMSContext(DbContextOptions<IMSContext> dbContextOptions) : base(dbContextOptions)
        {
                
        }


        public DbSet<Inventory>? Inventories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductInventory>? ProductInventories { get; set; }
        public DbSet<InventoryTransaction>? InventoryTransactions { get; set; }
        public DbSet<ProductTransaction>? ProductTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInventory>()
                .HasKey(x => new { x.ProductId, x.InventoryId });

            modelBuilder.Entity<ProductInventory>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductInventories)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<ProductInventory>()
                .HasOne(x => x.Inventory)
                .WithMany(x => x.ProductInventories)
                .HasForeignKey(x => x.InventoryId);

            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { InventoryId = 1, InventoryName = "Bike Seat", InventoryQuantity = 10, InventoryPrice = 2 },
                new Inventory { InventoryId = 2, InventoryName = "Bike Body", InventoryQuantity = 10, InventoryPrice = 15 },
                new Inventory { InventoryId = 3, InventoryName = "Bike Wheel", InventoryQuantity = 20, InventoryPrice = 8 },
                new Inventory { InventoryId = 4, InventoryName = "Bike Pedal", InventoryQuantity = 20, InventoryPrice = 1 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductId = 1, ProductName = "Bike", ProductQuantity = 10, ProductPrice = 150 },
                new Product() { ProductId = 2, ProductName = "Car", ProductQuantity = 5, ProductPrice = 25000 }
            );

            modelBuilder.Entity<ProductInventory>().HasData(
                new ProductInventory { ProductId = 1, InventoryId = 1, InventoryQuantity = 1 }, 
                new ProductInventory { ProductId = 1, InventoryId = 2, InventoryQuantity = 1 }, 
                new ProductInventory { ProductId = 1, InventoryId = 3, InventoryQuantity = 2 }, 
                new ProductInventory { ProductId = 1, InventoryId = 4, InventoryQuantity = 2 } 
            );
        }
    }
}
