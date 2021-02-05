using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class ISADB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppUsers_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Pharmacys_PharmacyId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AppUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AppUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AppUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_PharmacyId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "UserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "UserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Roles");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "AppUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "PatientID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "UserLogins",
                newName: "IX_UserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "UserClaims",
                newName: "IX_UserClaims_UserId");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Reports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PharmacyId",
                table: "Reports",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminUserID",
                table: "Pharmacys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AvrageScore",
                table: "Pharmacys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pharmacys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Reserved",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "AvrageScore",
                table: "Drugs",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<long>(
                name: "PharmacyId",
                table: "AppUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AvrageScore",
                table: "AppUsers",
                type: "real",
                nullable: false,
                defaultValue: 0f);

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

            migrationBuilder.AddColumn<int>(
                name: "Penalty",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PatientID",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
                name: "MedicalExpertID",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogins",
                table: "UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
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
                name: "IX_Reports_EmployeeId",
                table: "Reports",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PharmacyId",
                table: "Reports",
                column: "PharmacyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_Roles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AppUsers_EmployeeId",
                table: "Reports",
                column: "EmployeeId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Pharmacys_PharmacyId",
                table: "Reports",
                column: "PharmacyId",
                principalTable: "Pharmacys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_AppUsers_UserId",
                table: "UserClaims",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_AppUsers_UserId",
                table: "UserLogins",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_AppUsers_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                name: "FK_AspNetRoleClaims_Roles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AppUsers_EmployeeId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Pharmacys_PharmacyId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_AppUsers_UserId",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_AppUsers_UserId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_AppUsers_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropTable(
                name: "TimeSpan");

            migrationBuilder.DropIndex(
                name: "IX_Reports_EmployeeId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_PharmacyId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppUserId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppUserId1",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogins",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "AdminUserID",
                table: "Pharmacys");

            migrationBuilder.DropColumn(
                name: "AvrageScore",
                table: "Pharmacys");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pharmacys");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Reserved",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AvrageScore",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AvrageScore",
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
                name: "Penalty",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MedicalExpertID",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AppUsers",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Appointments",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.AlterColumn<long>(
                name: "PharmacyId",
                table: "AppUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_PharmacyId",
                table: "AppUsers",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppUsers_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Pharmacys_PharmacyId",
                table: "AppUsers",
                column: "PharmacyId",
                principalTable: "Pharmacys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AppUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AppUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AppUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
