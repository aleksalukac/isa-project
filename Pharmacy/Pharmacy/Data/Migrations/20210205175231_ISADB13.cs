using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class ISADB13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Pharmacys_PharmacyId1",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_PharmacyId1",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "PharmacyId1",
                table: "AppUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PharmacyId1",
                table: "AppUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_PharmacyId1",
                table: "AppUsers",
                column: "PharmacyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Pharmacys_PharmacyId1",
                table: "AppUsers",
                column: "PharmacyId1",
                principalTable: "Pharmacys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
