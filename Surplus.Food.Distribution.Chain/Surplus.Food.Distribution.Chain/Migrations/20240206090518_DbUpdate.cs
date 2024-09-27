using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surplus.Food.Distribution.Chain.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodSupplierReviews_Customers_CustomerId",
                table: "FoodSupplierReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodSupplierReviews_FoodSuppliers_SupplierId",
                table: "FoodSupplierReviews");

            migrationBuilder.DropIndex(
                name: "IX_FoodSupplierReviews_CustomerId",
                table: "FoodSupplierReviews");

            migrationBuilder.DropIndex(
                name: "IX_FoodSupplierReviews_SupplierId",
                table: "FoodSupplierReviews");

            migrationBuilder.AddColumn<bool>(
                name: "DeletedFlag",
                table: "FoodItems",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedFlag",
                table: "FoodItems");

            migrationBuilder.CreateIndex(
                name: "IX_FoodSupplierReviews_CustomerId",
                table: "FoodSupplierReviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodSupplierReviews_SupplierId",
                table: "FoodSupplierReviews",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodSupplierReviews_Customers_CustomerId",
                table: "FoodSupplierReviews",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodSupplierReviews_FoodSuppliers_SupplierId",
                table: "FoodSupplierReviews",
                column: "SupplierId",
                principalTable: "FoodSuppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
