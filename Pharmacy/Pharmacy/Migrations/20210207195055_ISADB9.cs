using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class ISADB9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "WorkHoursEnd",
                table: "AppUsers",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WorkHoursStart",
                table: "AppUsers",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "AppUserDrug",
                columns: table => new
                {
                    AllergicDrugsId = table.Column<long>(type: "bigint", nullable: false),
                    AllergicUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserDrug", x => new { x.AllergicDrugsId, x.AllergicUsersId });
                    table.ForeignKey(
                        name: "FK_AppUserDrug_AppUsers_AllergicUsersId",
                        column: x => x.AllergicUsersId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserDrug_Drugs_AllergicDrugsId",
                        column: x => x.AllergicDrugsId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserDrug_AllergicUsersId",
                table: "AppUserDrug",
                column: "AllergicUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserDrug");

            migrationBuilder.DropColumn(
                name: "WorkHoursEnd",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "WorkHoursStart",
                table: "AppUsers");
        }
    }
}
