using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chinese_Sale.Migrations
{
    public partial class addIsDraftToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Order");
        }
    }
}
