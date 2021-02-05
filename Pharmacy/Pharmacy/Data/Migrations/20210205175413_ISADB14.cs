using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class ISADB14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Pharmacys_PharmacyId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_PharmacyId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "AppUsers");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeIdPharmacy",
                table: "AppUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeIdPharmacy",
                table: "AppUsers");

            migrationBuilder.AddColumn<long>(
                name: "PharmacyId",
                table: "AppUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_PharmacyId",
                table: "AppUsers",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Pharmacys_PharmacyId",
                table: "AppUsers",
                column: "PharmacyId",
                principalTable: "Pharmacys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
