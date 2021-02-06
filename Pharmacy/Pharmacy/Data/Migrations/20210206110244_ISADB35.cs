using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class ISADB35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbAppointments_AppUsers_AppUserId",
                table: "tbAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_tbAppointments_AppUsers_AppUserId1",
                table: "tbAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbAppointments",
                table: "tbAppointments");

            migrationBuilder.RenameTable(
                name: "tbAppointments",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_tbAppointments_AppUserId1",
                table: "Appointments",
                newName: "IX_Appointments_AppUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_tbAppointments_AppUserId",
                table: "Appointments",
                newName: "IX_Appointments_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppUsers_AppUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppUsers_AppUserId1",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "tbAppointments");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_AppUserId1",
                table: "tbAppointments",
                newName: "IX_tbAppointments_AppUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_AppUserId",
                table: "tbAppointments",
                newName: "IX_tbAppointments_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbAppointments",
                table: "tbAppointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbAppointments_AppUsers_AppUserId",
                table: "tbAppointments",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbAppointments_AppUsers_AppUserId1",
                table: "tbAppointments",
                column: "AppUserId1",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
