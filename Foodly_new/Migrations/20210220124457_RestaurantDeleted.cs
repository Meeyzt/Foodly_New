using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodly_new.Migrations
{
    public partial class RestaurantDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Restaurants",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Restaurants");
        }
    }
}
