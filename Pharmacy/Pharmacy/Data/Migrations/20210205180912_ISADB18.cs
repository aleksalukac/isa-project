using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class ISADB18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Pharmacys_PharmacyId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_PharmacyId",
                table: "AppUsers");

            migrationBuilder.AlterColumn<long>(
                name: "PharmacyId",
                table: "AppUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PharmacyId",
                table: "AppUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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
