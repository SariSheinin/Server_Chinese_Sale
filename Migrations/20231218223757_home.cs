using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chinese_Sale.Migrations
{
    public partial class home : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfDonation",
                table: "Donor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeOfDonation",
                table: "Donor",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
