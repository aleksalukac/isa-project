using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class ISADB4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Drugs");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "tbDrugAndQuantities",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "PhrmacyId",
                table: "Appointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "tbDrugAndQuantities");

            migrationBuilder.DropColumn(
                name: "PhrmacyId",
                table: "Appointments");

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Drugs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
