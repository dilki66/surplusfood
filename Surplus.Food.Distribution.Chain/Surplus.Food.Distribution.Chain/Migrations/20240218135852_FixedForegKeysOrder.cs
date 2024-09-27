using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surplus.Food.Distribution.Chain.Migrations
{
    /// <inheritdoc />
    public partial class FixedForegKeysOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceStatusId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PriceStatusId",
                table: "Orders",
                column: "PriceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServiceTypeId",
                table: "Orders",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_RefPriceStatuses_PriceStatusId",
                table: "Orders",
                column: "PriceStatusId",
                principalTable: "RefPriceStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_RefServiceTypes_ServiceTypeId",
                table: "Orders",
                column: "ServiceTypeId",
                principalTable: "RefServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_RefPriceStatuses_PriceStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_RefServiceTypes_ServiceTypeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PriceStatusId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ServiceTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PriceStatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "Orders");
        }
    }
}
