using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surplus.Food.Distribution.Chain.Migrations
{
    /// <inheritdoc />
    public partial class FoodItemOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Offers",
                table: "FoodItems",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Offers",
                table: "FoodItems");
        }
    }
}
