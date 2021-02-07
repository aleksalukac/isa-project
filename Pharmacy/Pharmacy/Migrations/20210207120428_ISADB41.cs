using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class ISADB41 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceRequests_AppUsers_EmployeeId",
                table: "AbsenceRequests");

            migrationBuilder.DropIndex(
                name: "IX_AbsenceRequests_EmployeeId",
                table: "AbsenceRequests");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "AbsenceRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "AbsenceRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceRequests_AppUserId",
                table: "AbsenceRequests",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceRequests_AppUsers_AppUserId",
                table: "AbsenceRequests",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceRequests_AppUsers_AppUserId",
                table: "AbsenceRequests");

            migrationBuilder.DropIndex(
                name: "IX_AbsenceRequests_AppUserId",
                table: "AbsenceRequests");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AbsenceRequests");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "AbsenceRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceRequests_EmployeeId",
                table: "AbsenceRequests",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceRequests_AppUsers_EmployeeId",
                table: "AbsenceRequests",
                column: "EmployeeId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
