using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelExp.Web.Migrations
{
    public partial class addTotalAmounttoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Trips",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Trips");
        }
    }
}
