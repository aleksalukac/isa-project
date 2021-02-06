using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class ISADB33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "Appointments");

            migrationBuilder.AddColumn<long>(
                name: "DurationId",
                table: "Appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DurationId",
                table: "Appointments",
                column: "DurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_TimeSpan_DurationId",
                table: "Appointments",
                column: "DurationId",
                principalTable: "TimeSpan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_TimeSpan_DurationId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DurationId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DurationId",
                table: "Appointments");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
