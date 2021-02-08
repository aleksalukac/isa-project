using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class ISADB33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbSaleItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugAndQuantitiesId = table.Column<long>(type: "bigint", nullable: true),
                    BeforePrice = table.Column<double>(type: "float", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSaleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbSaleItems_tbDrugAndQuantities_DrugAndQuantitiesId",
                        column: x => x.DrugAndQuantitiesId,
                        principalTable: "tbDrugAndQuantities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbSaleItems_DrugAndQuantitiesId",
                table: "tbSaleItems",
                column: "DrugAndQuantitiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbSaleItems");
        }
    }
}
