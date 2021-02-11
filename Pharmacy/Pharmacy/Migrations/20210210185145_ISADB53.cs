using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class ISADB53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrugId",
                table: "SupplyOrders");

            migrationBuilder.DropColumn(
                name: "ExtraQuantity",
                table: "SupplyOrders");

            migrationBuilder.CreateTable(
                name: "SupplyItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugId = table.Column<long>(type: "bigint", nullable: false),
                    SupplyOrderId = table.Column<long>(type: "bigint", nullable: false),
                    ExtraQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplyItems");

            migrationBuilder.AddColumn<long>(
                name: "DrugId",
                table: "SupplyOrders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "ExtraQuantity",
                table: "SupplyOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
