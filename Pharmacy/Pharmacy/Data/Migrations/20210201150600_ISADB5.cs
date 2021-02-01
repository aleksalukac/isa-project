using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class ISADB5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "AppUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "AdminUserID",
                table: "Pharmacys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "MedicalExpertId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TimeSpan",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSpan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSpan_Appointments_Id",
                        column: x => x.Id,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppUserId",
                table: "Appointments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppUserId1",
                table: "Appointments",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicalExpertId",
                table: "Appointments",
                column: "MedicalExpertId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppUsers_MedicalExpertId",
                table: "Appointments",
                column: "MedicalExpertId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppUsers_MedicalExpertId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "TimeSpan");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppUserId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppUserId1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_MedicalExpertId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AdminUserID",
                table: "Pharmacys");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MedicalExpertId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AppUsers",
                newName: "Password");
        }
    }
}
