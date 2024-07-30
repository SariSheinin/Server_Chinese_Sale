using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chinese_Sale.Migrations
{
    public partial class ChangePropOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PresentsOrder_Order_OrderId",
                table: "PresentsOrder");

            migrationBuilder.DropColumn(
                name: "OrederId",
                table: "PresentsOrder");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "PresentsOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PresentsOrder_Order_OrderId",
                table: "PresentsOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PresentsOrder_Order_OrderId",
                table: "PresentsOrder");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "PresentsOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrederId",
                table: "PresentsOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PresentsOrder_Order_OrderId",
                table: "PresentsOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
