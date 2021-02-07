using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class ISADB40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbsenceRequests_AppUsers_PharmacyAdministratorId",
                table: "AbsenceRequests");

            migrationBuilder.DropIndex(
                name: "IX_AbsenceRequests_PharmacyAdministratorId",
                table: "AbsenceRequests");

            migrationBuilder.DropColumn(
                name: "TimeSpan",
                table: "AbsenceRequests");

            migrationBuilder.AlterColumn<string>(
                name: "PharmacyAdministratorId",
                table: "AbsenceRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "AbsenceRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "AbsenceRequests");

            migrationBuilder.AlterColumn<string>(
                name: "PharmacyAdministratorId",
                table: "AbsenceRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "AbsenceRequests",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceRequests_PharmacyAdministratorId",
                table: "AbsenceRequests",
                column: "PharmacyAdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbsenceRequests_AppUsers_PharmacyAdministratorId",
                table: "AbsenceRequests",
                column: "PharmacyAdministratorId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
