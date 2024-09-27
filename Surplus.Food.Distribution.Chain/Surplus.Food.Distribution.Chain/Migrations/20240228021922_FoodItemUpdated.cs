using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surplus.Food.Distribution.Chain.Migrations
{
    /// <inheritdoc />
    public partial class FoodItemUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "FoodItems",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PickupTime",
                table: "FoodItems",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "PickupTime",
                table: "FoodItems");
        }
    }
}
