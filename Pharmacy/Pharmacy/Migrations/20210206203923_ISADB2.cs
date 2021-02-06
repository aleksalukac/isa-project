using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class ISADB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbDrugAndQuantities_Orders_OrderId",
                table: "tbDrugAndQuantities");

            migrationBuilder.DropIndex(
                name: "IX_tbDrugAndQuantities_OrderId",
                table: "tbDrugAndQuantities");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "tbDrugAndQuantities");

            migrationBuilder.DropColumn(
                name: "ReSupply",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Reserved",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Sold",
                table: "Orders",
                newName: "TransactionComplete");

            migrationBuilder.AddColumn<long>(
                name: "DrugAndQuantitiesId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DrugAndQuantitiesId",
                table: "Orders",
                column: "DrugAndQuantitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_tbDrugAndQuantities_DrugAndQuantitiesId",
                table: "Orders",
                column: "DrugAndQuantitiesId",
                principalTable: "tbDrugAndQuantities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_tbDrugAndQuantities_DrugAndQuantitiesId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DrugAndQuantitiesId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DrugAndQuantitiesId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TransactionComplete",
                table: "Orders",
                newName: "Sold");

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "tbDrugAndQuantities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReSupply",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reserved",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_tbDrugAndQuantities_OrderId",
                table: "tbDrugAndQuantities",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbDrugAndQuantities_Orders_OrderId",
                table: "tbDrugAndQuantities",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
