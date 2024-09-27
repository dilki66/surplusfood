using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surplus.Food.Distribution.Chain.Migrations
{
    /// <inheritdoc />
    public partial class AddedPriceSt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceStatusId",
                table: "OrderItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceStatusId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
