using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class ISADB52 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplyOrders_Pharmacys_PharmacyId",
                table: "SupplyOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_tbSaleItems_tbDrugAndQuantities_DrugAndQuantitiesId",
                table: "tbSaleItems");

            migrationBuilder.DropIndex(
                name: "IX_tbSaleItems_DrugAndQuantitiesId",
                table: "tbSaleItems");

            migrationBuilder.AlterColumn<long>(
                name: "DrugAndQuantitiesId",
                table: "tbSaleItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PharmacyId",
                table: "SupplyOrders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleveryDate",
                table: "SupplyOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "DrugId",
                table: "SupplyOrders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "ExtraQuantity",
                table: "SupplyOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyOrders_Pharmacys_PharmacyId",
                table: "SupplyOrders",
                column: "PharmacyId",
                principalTable: "Pharmacys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplyOrders_Pharmacys_PharmacyId",
                table: "SupplyOrders");

            migrationBuilder.DropColumn(
                name: "DeleveryDate",
                table: "SupplyOrders");

            migrationBuilder.DropColumn(
                name: "DrugId",
                table: "SupplyOrders");

            migrationBuilder.DropColumn(
                name: "ExtraQuantity",
                table: "SupplyOrders");

            migrationBuilder.AlterColumn<long>(
                name: "DrugAndQuantitiesId",
                table: "tbSaleItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PharmacyId",
                table: "SupplyOrders",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_tbSaleItems_DrugAndQuantitiesId",
                table: "tbSaleItems",
                column: "DrugAndQuantitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyOrders_Pharmacys_PharmacyId",
                table: "SupplyOrders",
                column: "PharmacyId",
                principalTable: "Pharmacys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbSaleItems_tbDrugAndQuantities_DrugAndQuantitiesId",
                table: "tbSaleItems",
                column: "DrugAndQuantitiesId",
                principalTable: "tbDrugAndQuantities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
