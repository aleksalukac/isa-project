using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class isadb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppUsers_AppUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppUsers_AppUserId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppUserId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppUserId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppUserId",
                table: "Appointments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppUserId1",
                table: "Appointments",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppUsers_AppUserId",
                table: "Appointments",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppUsers_AppUserId1",
                table: "Appointments",
                column: "AppUserId1",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
