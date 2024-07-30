using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chinese_Sale.Migrations
{
    public partial class addQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PresentsOrder",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PresentsOrder_OrderId",
                table: "PresentsOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PresentsOrder_PresentId",
                table: "PresentsOrder",
                column: "PresentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresentsOrder_Order_OrderId",
                table: "PresentsOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PresentsOrder_Present_PresentId",
                table: "PresentsOrder",
                column: "PresentId",
                principalTable: "Present",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PresentsOrder_Order_OrderId",
                table: "PresentsOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PresentsOrder_Present_PresentId",
                table: "PresentsOrder");

            migrationBuilder.DropIndex(
                name: "IX_PresentsOrder_OrderId",
                table: "PresentsOrder");

            migrationBuilder.DropIndex(
                name: "IX_PresentsOrder_PresentId",
                table: "PresentsOrder");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PresentsOrder");
        }
    }
}
