using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodly_new.Migrations
{
    public partial class Restaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RestaurantID",
                table: "Reviews",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantID",
                table: "Reviews");
        }
    }
}
