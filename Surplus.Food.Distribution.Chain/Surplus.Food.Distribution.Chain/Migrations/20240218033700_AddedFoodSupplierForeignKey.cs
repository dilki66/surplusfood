using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surplus.Food.Distribution.Chain.Migrations
{
    /// <inheritdoc />
    public partial class AddedFoodSupplierForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FoodSupplierId",
                table: "FoodItems",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_FoodSupplierId",
                table: "FoodItems",
                column: "FoodSupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_FoodSuppliers_FoodSupplierId",
                table: "FoodItems",
                column: "FoodSupplierId",
                principalTable: "FoodSuppliers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_FoodSuppliers_FoodSupplierId",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_FoodSupplierId",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "FoodSupplierId",
                table: "FoodItems");
        }
    }
}
