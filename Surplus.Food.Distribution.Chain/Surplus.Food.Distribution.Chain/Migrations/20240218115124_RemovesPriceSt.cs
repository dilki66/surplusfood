using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surplus.Food.Distribution.Chain.Migrations
{
    /// <inheritdoc />
    public partial class RemovesPriceSt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_RefPriceStatuses_PriceStatusId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PriceStatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PriceStatusId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceStatusId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PriceStatusId",
                table: "Orders",
                column: "PriceStatusId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_RefPriceStatuses_PriceStatusId",
                table: "Orders",
                column: "PriceStatusId",
                principalTable: "RefPriceStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
