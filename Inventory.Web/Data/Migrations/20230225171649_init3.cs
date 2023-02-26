using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Web.Data.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Customers_BuyerId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Employees_BorrowerID",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "BuyerId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerID",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Customers_BuyerId",
                table: "Items",
                column: "BuyerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Employees_BorrowerID",
                table: "Items",
                column: "BorrowerID",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Customers_BuyerId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Employees_BorrowerID",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "BuyerId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerID",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Customers_BuyerId",
                table: "Items",
                column: "BuyerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Employees_BorrowerID",
                table: "Items",
                column: "BorrowerID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
