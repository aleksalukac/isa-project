using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class ISADB34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppUsers_AppUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppUsers_AppUserId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_TimeSpan_DurationId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "TimeSpan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DurationId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DurationId",
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

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "AbsenceRequests",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "tbAppointments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "tbAppointments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TimeSpan",
                table: "AbsenceRequests");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "tbAppointments");

            migrationBuilder.DropColumn(
                name: "TimeSpan",
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

            migrationBuilder.AddColumn<long>(
                name: "DurationId",
                table: "Appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

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
                        name: "FK_TimeSpan_AbsenceRequests_Id",
                        column: x => x.Id,
                        principalTable: "AbsenceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSpan_Appointments_Id",
                        column: x => x.Id,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DurationId",
                table: "Appointments",
                column: "DurationId");

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
                name: "FK_Appointments_TimeSpan_DurationId",
                table: "Appointments",
                column: "DurationId",
                principalTable: "TimeSpan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
