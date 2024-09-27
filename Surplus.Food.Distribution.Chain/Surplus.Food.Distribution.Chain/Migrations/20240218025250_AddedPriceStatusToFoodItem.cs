using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surplus.Food.Distribution.Chain.Migrations
{
    /// <inheritdoc />
    public partial class AddedPriceStatusToFoodItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceStatus",
                table: "FoodItems",
                newName: "PriceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_PriceStatusId",
                table: "FoodItems",
                column: "PriceStatusId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_RefPriceStatuses_PriceStatusId",
                table: "FoodItems",
                column: "PriceStatusId",
                principalTable: "RefPriceStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_RefPriceStatuses_PriceStatusId",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_PriceStatusId",
                table: "FoodItems");

            migrationBuilder.RenameColumn(
                name: "PriceStatusId",
                table: "FoodItems",
                newName: "PriceStatus");
        }
    }
}
