using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodly_new.Migrations
{
    public partial class Restaurants2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByID",
                table: "Restaurants",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Restaurants",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByID",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Restaurants");
        }
    }
}
