using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Plugins.EFCoreSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class deleteInvId_PT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTransactions_Inventories_InventoryId",
                table: "ProductTransactions");

            migrationBuilder.DropIndex(
                name: "IX_ProductTransactions_InventoryId",
                table: "ProductTransactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductTransactions_InventoryId",
                table: "ProductTransactions",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTransactions_Inventories_InventoryId",
                table: "ProductTransactions",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
