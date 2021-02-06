using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class IsaDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugAndQuantity");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateTime",
                table: "AbsenceRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "tbDrugAndQuantities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugId = table.Column<long>(type: "bigint", nullable: true),
                    PharmacyId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbDrugAndQuantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbDrugAndQuantities_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbDrugAndQuantities_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbDrugAndQuantities_DrugId",
                table: "tbDrugAndQuantities",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_tbDrugAndQuantities_OrderId",
                table: "tbDrugAndQuantities",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbDrugAndQuantities");

            migrationBuilder.DropColumn(
                name: "StartDateTime",
                table: "AbsenceRequests");

            migrationBuilder.CreateTable(
                name: "DrugAndQuantity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugId = table.Column<long>(type: "bigint", nullable: true),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    SupplyOrderId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugAndQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugAndQuantity_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrugAndQuantity_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrugAndQuantity_SupplyOrders_SupplyOrderId",
                        column: x => x.SupplyOrderId,
                        principalTable: "SupplyOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugAndQuantity_DrugId",
                table: "DrugAndQuantity",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugAndQuantity_OrderId",
                table: "DrugAndQuantity",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugAndQuantity_SupplyOrderId",
                table: "DrugAndQuantity",
                column: "SupplyOrderId");
        }
    }
}
