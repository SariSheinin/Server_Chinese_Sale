using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chinese_Sale.Migrations
{
    public partial class isPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PresentsOrder_Order_OrderId",
                table: "PresentsOrder");

            migrationBuilder.DropIndex(
                name: "IX_PresentsOrder_OrderId",
                table: "PresentsOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PresentsOrder_OrderId",
                table: "PresentsOrder",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresentsOrder_Order_OrderId",
                table: "PresentsOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
