using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surplus.Food.Distribution.Chain.Migrations
{
    /// <inheritdoc />
    public partial class AddedFItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Image = table.Column<byte[]>(type: "longblob", nullable: false),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    FoodSupplierId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceStatusId = table.Column<int>(type: "int", nullable: false),
                    DeletedFlag = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodItems_FoodSuppliers_FoodSupplierId",
                        column: x => x.FoodSupplierId,
                        principalTable: "FoodSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItems_RefPriceStatuses_PriceStatusId",
                        column: x => x.PriceStatusId,
                        principalTable: "RefPriceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItems_RefServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "RefServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_FoodItemId",
                table: "OrderItems",
                column: "FoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_FoodSupplierId",
                table: "FoodItems",
                column: "FoodSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_PriceStatusId",
                table: "FoodItems",
                column: "PriceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_ServiceTypeId",
                table: "FoodItems",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_FoodItems_FoodItemId",
                table: "OrderItems",
                column: "FoodItemId",
                principalTable: "FoodItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_FoodItems_FoodItemId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_FoodItemId",
                table: "OrderItems");
        }
    }
}
