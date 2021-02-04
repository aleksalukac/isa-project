using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Data.Migrations
{
    public partial class ISADB8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppUsers_MedicalExpertId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppUsers_PatientId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_MedicalExpertId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "MedicalExpertId",
                table: "Appointments",
                newName: "MedicalExpertID");

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

            migrationBuilder.AddColumn<float>(
                name: "AvrageScore",
                table: "Pharmacys",
                type: "real",
                nullable: false,
                defaultValue: 0f);

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

            migrationBuilder.AddColumn<float>(
                name: "AvrageScore",
                table: "AppUsers",
                type: "real",
                nullable: false,
                defaultValue: 0f);

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

            migrationBuilder.AlterColumn<string>(
                name: "MedicalExpertID",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_EmployeeId",
                table: "Reports",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PharmacyId",
                table: "Reports",
                column: "PharmacyId");

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
                name: "FK_TimeSpan_AbsenceRequests_Id",
                table: "TimeSpan",
                column: "Id",
                principalTable: "AbsenceRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AppUsers_EmployeeId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Pharmacys_PharmacyId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSpan_AbsenceRequests_Id",
                table: "TimeSpan");

            migrationBuilder.DropIndex(
                name: "IX_Reports_EmployeeId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_PharmacyId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "AvrageScore",
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
                name: "AvrageScore",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Penalty",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Appointments",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "MedicalExpertID",
                table: "Appointments",
                newName: "MedicalExpertId");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicalExpertId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicalExpertId",
                table: "Appointments",
                column: "MedicalExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppUsers_MedicalExpertId",
                table: "Appointments",
                column: "MedicalExpertId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppUsers_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
